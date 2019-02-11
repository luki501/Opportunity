using log4net;
using Opportunity.Model.Database;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Opportunity.Model.Obiekty
{
    public class Pracownik 
    {
        #region fields
        private Pracownicy pracownikDb;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public int Id { get { return pracownikDb.id; } }
        public string Numer { get { return pracownikDb.numer; } set { pracownikDb.numer = value; } }
        public string Imie { get { return pracownikDb.imie; } set { pracownikDb.imie = value; } }
        public string Nazwisko { get { return pracownikDb.nazwisko; } set { pracownikDb.nazwisko = value; } }
        public bool Brygadzista { get { return pracownikDb.brygadzista; } set { pracownikDb.brygadzista = value; } }
        public bool Admin { get { return pracownikDb.admin; } set { pracownikDb.admin = value; } }
        public int? IdNadrzednego { get { return pracownikDb.id_nadrzednego; } set { pracownikDb.id_nadrzednego = value; } }
        public DateTime? DataUrodzenia { get { return pracownikDb.data_urodzenia; } set { pracownikDb.data_urodzenia = value; } }
        public string Uwagi { get { return pracownikDb.uwagi; } set { pracownikDb.uwagi = value; } }
        public string Login { get { return pracownikDb.login; } set { pracownikDb.login = value; } }
        public byte[] Password { get { return pracownikDb.passwordhash; } set { pracownikDb.passwordhash = value; } }
        public ObservableCollection<ADokument> ListaDokumentow { get; set; }
        public string Katalog { get { return string.Format("{0}{1}ID{2}", Nazwisko, Imie, Id).Replace(" ", ""); } }
        

        public Adres Adres { get; set; }
        public Paszport PaszportAktualny { get { return (Paszport)ListaDokumentow.Where(d => d is Paszport).OrderByDescending(p => p.DataWystawienia).FirstOrDefault(); } }
        public SzkolenieBHP BHPAktualne { get { return (SzkolenieBHP)ListaDokumentow.Where(d => d is SzkolenieBHP).OrderByDescending(p => p.DataWystawienia).FirstOrDefault(); } }
        public Wiza WizaAktualna { get { return (Wiza)ListaDokumentow.Where(d => d is Wiza).OrderByDescending(p => p.DataWystawienia).FirstOrDefault(); } }
        public Oswiadczenie OswiadczenieAktualne { get { return (Oswiadczenie)ListaDokumentow.Where(d => d is Oswiadczenie).OrderByDescending(p => p.DataWystawienia).FirstOrDefault(); } }
        public Zezwolenie ZezwolenieAktualne { get { return (Zezwolenie)ListaDokumentow.Where(d => d is Zezwolenie).OrderByDescending(p => p.DataWystawienia).FirstOrDefault(); } }
        public Zameldowanie ZameldowanieAktualne { get { return (Zameldowanie)ListaDokumentow.Where(d => d is Zameldowanie).OrderByDescending(p => p.DataWystawienia).FirstOrDefault(); } }
        public Badanie BadanieAktualne { get { return (Badanie)ListaDokumentow.Where(d => d is Badanie).OrderByDescending(p => p.DataWystawienia).FirstOrDefault(); } }
        public Zatrudnienie ZatrudnienieAktualne { get { return (Zatrudnienie)ListaDokumentow.Where(d => d is Zatrudnienie).OrderByDescending(p => p.DataWystawienia).FirstOrDefault(); } }

        public Pracownicy PracownikDb { get { return pracownikDb; } }

        public string DanePersonalne
        {
            get { string s = string.Format("{0} {1}", Nazwisko, Imie); if (s.Equals(" ")) return Numer; return s; }
        }
        public string Wiek
        {
            get
            {
                if (DataUrodzenia != null)
                    return (DateTime.Now.Year - ((DateTime)DataUrodzenia).Year).ToString();
                return "";
            }
        }
        #endregion

        #region constructors
        public Pracownik()
        {            
            pracownikDb = new Pracownicy();            
            PobierzListy();
        }

        public Pracownik(Pracownicy p)
        {
            pracownikDb = p;
            PobierzListy();            
        }
        #endregion

        #region methods
        internal void Save()
        {
            try
            {
                if (Id == 0)
                    DbAdapterEF.PracownikInsert(this);
                else
                    DbAdapterEF.PracownikUpdate(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal void Remove()
        {
            try
            {
                DbAdapterEF.PracownikRemove(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal void DodajUzytkownika(string haslo)
        {
            try
            {
                DbAdapterEF.DodajUzytkownika(pracownikDb.numer, haslo);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        private void PobierzListy()
        {
            try
            {
                ListaDokumentow = new ObservableCollection<ADokument>();
                foreach (Paszporty paszport in pracownikDb.Paszporty.Where(p => !p.nieaktywny))
                {
                    ListaDokumentow.Add(new Paszport(paszport));
                }
                foreach (Wizy wiza in pracownikDb.Wizy.Where(p => !p.nieaktywna))
                {
                    ListaDokumentow.Add(new Wiza(wiza));
                }
                foreach (Badania badanie in pracownikDb.Badania.Where(p => !p.nieaktywne))
                {
                    ListaDokumentow.Add(new Badanie(badanie));
                }
                foreach (Oswiadczenia oswiadczenie in pracownikDb.Oswiadczenia.Where(p => !p.nieaktywne))
                {
                    ListaDokumentow.Add(new Oswiadczenie(oswiadczenie));
                }
                foreach (SzkoleniaBHP szkolenie in pracownikDb.SzkoleniaBHP.Where(p => !p.nieaktywne))
                {
                    ListaDokumentow.Add(new SzkolenieBHP(szkolenie));
                }
                foreach (Zaproszenia zaproszenie in pracownikDb.Zaproszenia.Where(p => !p.nieaktywne))
                {
                    ListaDokumentow.Add(new Zameldowanie(zaproszenie));
                }
                foreach (Zezwolenia zezwolenie in pracownikDb.Zezwolenia.Where(p => !p.nieaktywne))
                {
                    ListaDokumentow.Add(new Zezwolenie(zezwolenie));
                }
                foreach (Zatrudnienia zatrudnienie in pracownikDb.Zatrudnienia.Where(p => !p.nieaktywne))
                {
                    ListaDokumentow.Add(new Zatrudnienie(zatrudnienie));
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        internal void DodajDokument(int typ, string numer, DateTime dataWystawienia, DateTime dataWaznosci, string uwagi) // TODO Czy używane?
        {
            try
            {                
                ADokument dokument;                
                switch (typ)
                {
                    case 0:
                        dokument = new Paszport(numer, dataWystawienia, dataWaznosci, uwagi);
                        dokument.Save(Id);
                        ListaDokumentow.Add(dokument as Paszport);
                        break;
                    case 1:
                        dokument = new Zezwolenie(numer, dataWystawienia, dataWaznosci, uwagi);
                        dokument.Save(Id);
                        ListaDokumentow.Add(dokument as Zezwolenie);
                        break;
                    case 2:
                        dokument = new Zameldowanie(numer, dataWystawienia, dataWaznosci, uwagi);
                        dokument.Save(Id);
                        ListaDokumentow.Add(dokument as Zameldowanie);
                        break;
                    case 3:
                        dokument = new Wiza(numer, dataWystawienia, dataWaznosci, uwagi);
                        dokument.Save(Id);
                        ListaDokumentow.Add(dokument as Wiza);
                        break;
                    case 4:
                        dokument = new SzkolenieBHP(numer, dataWystawienia, dataWaznosci, uwagi);
                        dokument.Save(Id);
                        ListaDokumentow.Add(dokument as SzkolenieBHP);
                        break;
                    case 5:
                        dokument = new Oswiadczenie(numer, dataWystawienia, dataWaznosci, uwagi);
                        dokument.Save(Id);
                        ListaDokumentow.Add(dokument as Oswiadczenie);
                        break;
                    case 6:
                        dokument = new Badanie(numer, dataWystawienia, dataWaznosci, uwagi);
                        dokument.Save(Id);
                        ListaDokumentow.Add(dokument as Badanie);
                        break;
                    default:
                        break;
                } 
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        
        internal void DodajDokument(ADokument nowyDokument)
        {
            nowyDokument.Save(Id);
            ListaDokumentow.Add(nowyDokument);
        }
        
        internal void UsunDokument(ADokument dokument)
        {
            dokument.Remove();
        }
        internal string Validate()
        {
            try
            {
                if (Numer == null || Numer.Equals(""))
                    return "Brak numeru";
                else
                    return "";
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
