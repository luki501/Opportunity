using log4net;
using Opportunity.Model.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Model.Obiekty
{
    public class Towar
    {
        #region fields
        private Towary towarDb;
        //private TypTowaru typ;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public int Id { get { return towarDb.id; } }
        //public TypTowaru Typ { get { return new TypTowaru(towarDb.Slownik_typy_towarow); } set { towarDb.Slownik_typy_towarow = DbAdapterEF.GetSlownikTypyTowarow(value); } }
        public int IdTypu { get { return towarDb.id_typu; } set { towarDb.id_typu = value; } }
        public int? IdMarki { get { return towarDb.id_marki; } set { towarDb.id_marki = value; } }
        public string NazwaTypu { get { return DbAdapterEF.GetNazwaGrupyTowarow(IdTypu); } }
        public string NazwaMarki { get { return DbAdapterEF.GetNazwaMarkiTowaru(IdMarki); } }
        public string Nazwa { get { return towarDb.nazwa; } set { towarDb.nazwa = value; } }
        public string Marka { get { return towarDb.marka; } set { towarDb.marka = value; } }
        public string Model { get { return towarDb.model; } set { towarDb.model = value; } }
        public string Ean { get { return towarDb.ean; } set { towarDb.ean = value; } }
        public string PathZdjecie { get { return towarDb.path_zdjecie; } set { towarDb.path_zdjecie = value; Save(); } }
        public string PathGwarancja { get { return towarDb.path_gwarancja; } set { towarDb.path_gwarancja = value; Save(); } }
        public bool Premium { get { return towarDb.premium; } set { towarDb.premium = value; } }        
        public string Uwagi { get { return towarDb.uwagi; } set { towarDb.uwagi = value; } }
        public Towary TowarDb { get { return towarDb; } }    
        public string Katalog { get { return string.Format("{0}{1}ID{2}", Premium ? Marka : Nazwa, Model, Id).Replace(" ", ""); } }

        public ObservableCollection<Przyjecie> Przyjecia { get; set; }
        public ObservableCollection<WydanieView> Wydania { get; set; }
        public ObservableCollection<Wycofanie> Wycofania { get; set; }

        public DateTime? DataPrzyjecia
        {
            get
            {
                if (Przyjecia.Count == 0)
                    return null;
                else
                    return Przyjecia.OrderByDescending(p => p.DataPrzyjecia).Select(p => p.DataPrzyjecia).First();
            }
        }
        public decimal StanWydanych
        {
            get
            {
                decimal iloscOut = 0;
                if (Wydania.Any(w => w.IdWydajacego == null))
                    iloscOut = Wydania.Where(w => w.IdWydajacego == null).Sum(w => w.Ilosc);
                decimal iloscIn = 0;
                if (Wydania.Any(w => w.IdWydajacego == null))
                    iloscIn = Wydania.Where(w => w.IdPrzyjmujacego == null).Sum(w => w.Ilosc);
                return iloscOut - iloscIn;
            }
        }        
        public decimal IloscPrzyjetych
        {
            get
            {
                if (Przyjecia.Count > 0)
                    return Przyjecia.Sum(w => w.Ilosc);
                else
                    return 0;

            }
        }
        public decimal IloscWycofanych
        {
            get
            {
                if (Wycofania.Count > 0)
                    return Wycofania.Sum(w => w.Ilosc);
                else
                    return 0;
            }
        }

        public string Brygadzista
        {
            get
            {
                if (Premium && StanWydanych == 1)
                {
                    Pracownik pracownik = DbAdapterEF.GetPracownikaByTowar(Id);
                    if (pracownik != null)
                        return pracownik.DanePersonalne;
                }
                else if (Premium && IloscWycofanych > 0)
                    return "Wycofany";
                else if (Premium && IloscPrzyjetych > 0)
                    return "W magazynie";
                return "";
            }
        }
        public int IdBrygadzisty
        {
            get
            {
                if (IloscPrzyjetych == 1 && StanWydanych == 1)
                {
                    Pracownik pracownik = DbAdapterEF.GetPracownikaByTowar(Id);
                    if (pracownik != null)
                        return pracownik.Id;
                }
                return 0;
            }
        }
        public decimal StanMagazynowy { get { return IloscPrzyjetych - StanWydanych - IloscWycofanych; } }
        public DateTime? DataOstatniegoWydania
        {
            get
            {
                if (StanWydanych > 0)
                {
                    return Wydania.Max(w => w.DataWydania);
                }
                else
                    return null;
            }
        }
        
        #endregion

        #region constructors
        public Towar()
        {
            towarDb = new Towary();
            DanePoczatkowe();
            Przyjecia = new ObservableCollection<Przyjecie>();
            Wycofania = new ObservableCollection<Wycofanie>();
            Wydania = new ObservableCollection<WydanieView>();               
        }

        public Towar(Towary towar)
        {
            towarDb = towar;
            Przyjecia = new ObservableCollection<Przyjecie>(DbAdapterEF.GetListaPrzyjec(towar.id));
            Wycofania = new ObservableCollection<Wycofanie>(DbAdapterEF.GetListaWycofan(towar.id));
            Wydania = new ObservableCollection<WydanieView>(DbAdapterEF.GetListaWydan(towar.id));            
        }
        #endregion

        #region methods
        public void Save()
        {
            try
            {
                if (Id == 0)
                {
                    DbAdapterEF.TowarInsert(this);
                }
                else
                    DbAdapterEF.TowarUpdate(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        public void Remove()
        {
            try
            {
                DbAdapterEF.TowarRemove(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        private void DanePoczatkowe()
        {
            try
            {
                Nazwa = "";
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal string Validate()
        {
            try
            {
                if (IdTypu == 0)
                    return "Wybierz typ towaru";
                else if ((Nazwa == null || Nazwa.Equals("")) && (IdMarki == null || Model.Equals("")))
                    return "produkt musi posiadać cechy";
                return "";
            }
            catch (Exception ex) { ; log.Error(ex); throw ex; }
        }
        #endregion

        #region static method
        public static void ZapiszNowyTypTowaru(string nazwa)
        {
            try
            {
                if (PobierzTypyTowarow().Any(t => t.Nazwa.Equals(nazwa)))
                    throw new Exception("Ten typ towaru już istnieje w bazie");
                if (nazwa.Equals(""))
                    throw new Exception("Brak nazwy");
                DbAdapterEF.TypTowaruInsert(nazwa);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        public static List<TypTowaru> PobierzTypyTowarow()
        {
            try
            {
                return DbAdapterEF.GetListaTypowTowarow();
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        internal static void UsunTypTowaru(TypTowaru typ)
        {
            try
            {                
                DbAdapterEF.TypTowarRemove(typ.typTowaruDb);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        public static int GetIdTypuTowaru(string nazwa)
        {
            try
            {
                return DbAdapterEF.GetIdGrupyTowarow(nazwa);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        public static void ZapiszNowaMarkeTowaru(string nazwa)
        {
            try
            {
                if (PobierzMarkiTowarow().Any(t => t.Nazwa.Equals(nazwa)))
                    throw new Exception("Ta marka towaru już istnieje w bazie");
                if (nazwa.Equals(""))
                    throw new Exception("Brak nazwy");
                DbAdapterEF.MarkaTowaruInsert(nazwa);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        public static List<MarkaTowaru> PobierzMarkiTowarow()
        {
            try
            {
                return DbAdapterEF.GetListaMarekTowarow();
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        internal static void UsunMarkeTowaru(MarkaTowaru marka)
        {
            try
            {
                DbAdapterEF.MarkaTowarRemove(marka.markaDb);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        public static int GetIdMarkiTowaru(string nazwa)
        {
            try
            {
                return DbAdapterEF.GetIdMarkiTowarow(nazwa);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
