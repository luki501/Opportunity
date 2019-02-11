using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using log4net;
using Opportunity.Model;
using Opportunity.Model.Database;
using Opportunity.Model.Obiekty;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Opportunity.ViewModel.Obiekty
{
    public class TowarVM : ViewModelBase
    {
        #region fields
        private Towar towar;
        public Towar Towar { get { return towar; } }
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        private bool zaznaczony;
        private string pathPrzyjecie;
        private string pathWycofanie;
        private DialogService dialog;
        private ObservableCollection<WydanieViewVM> listaWydan;

        public int Id { get { return towar.Id; } }    
        public int IdTypu { get { return towar.IdTypu; } set { towar.IdTypu = value; } }
        public int? IdMarki { get { return towar.IdMarki; } set { towar.IdMarki = value; } }
        //public TypTowaru Typ { get { return towar.Typ; } set { towar.Typ = value; } }
        public string NazwaTypu { get { if (IdTypu != 0) return towar.NazwaTypu; else return ""; } set { GetTyp(value); } }
        public string NazwaMarki { get { if (IdMarki != 0) return towar.NazwaMarki; else return ""; } set { GetMarka(value); } }
        public string Nazwa { get { if (towar.Nazwa != null) return towar.Nazwa; else return ""; } set { towar.Nazwa = value; } }
        public string Marka { get { if (towar.Marka != null) return towar.Marka; else return ""; } set { towar.Marka = value; } }
        public string Model { get { if (towar.Model != null) return towar.Model; else return ""; } set { towar.Model = value; } }
        public string Ean { get { return towar.Ean; } set { towar.Ean = value; } }
        public string PathZdjecie { get { return towar.PathZdjecie; } set { towar.PathZdjecie = value; RaisePropertyChanged("PathZdjecie"); } }
        public string PathGwarancja { get { return towar.PathGwarancja; } set { towar.PathGwarancja = value; RaisePropertyChanged("PathGwarancja"); } }
        public string PathWycofanie { get { return pathWycofanie; } set { pathWycofanie = value; RaisePropertyChanged("PathWycofanie"); } }
        public string PathPrzyjecie { get { return pathPrzyjecie; } set { pathPrzyjecie = value; RaisePropertyChanged("PathPrzyjecie"); } }
        public bool Premium { get { return towar.Premium; } set { towar.Premium = value; } }
        public string Uwagi { get { return towar.Uwagi; } set { towar.Uwagi = value; } }        

        public DateTime? DataPrzyjecia { get { return towar.DataPrzyjecia; } }
        public decimal StanMagazynowy { get { return towar.StanMagazynowy; } }
        public decimal StanWydanych { get { return towar.StanWydanych; } }
        public string Brygadzista { get { return towar.Brygadzista; } }
        public int IdBrygadzisty { get { return towar.IdBrygadzisty; } }
        public ObservableCollection<WydanieViewVM> ListaWydan { get { return new ObservableCollection<WydanieViewVM>(listaWydan.OrderByDescending(d => d.DataWydania)); } set { listaWydan = value; } }
        public ObservableCollection<PrzyjecieVM> ListaPrzyjec { get; set; }
        public decimal IloscWycofanych { get { return towar.IloscWycofanych; } }
        public bool IsWydaniaBezPotwierdzenia { get { return ListaWydan.Any(w => w.DataPotwierdzeniaPrzyjecia == null); } }
        public bool IsWycofany { get { return IloscWycofanych > 0; } }
        public bool IsWMagazynie { get { if (StanMagazynowy > 0) return true; else return false; } }
        public bool IsWydany { get { if (StanWydanych > 0) return true; else return false; } }
        public bool IsTowarDoPrzyjecia
        {
            get { if (Premium && towar.IloscPrzyjetych > 0) return false; else return true; }
        }
        public bool IsTowarDoWydania
        {
            get { if (IsWMagazynie || IsWydany ) return true; else return false; }
        }
        public bool IsTowarDoPobrania
        {
            get { if (IsWydany) return true; else return false; }
        }
        public string UrlPathZdjecie
        {
            get
            {
                string s = string.Format("media/{0}", PathZdjecie);
                return s;
            }
        }

        public BitmapImage BitmapZdjecie
        {
            get
            {
                try
                {
                    BitmapImage bi = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + PathZdjecie.Replace("/", "\\")));
                    return bi;
                }
                catch { return null; }
            }
        }

        public string UrlPathGwarancja
        {
            get
            {
                string s = string.Format("media/{0}", PathGwarancja);
                return s;
            }
        }

        public BitmapImage BitmapGwarancja
        {
            get
            {
                try
                {
                    BitmapImage bi = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + PathGwarancja.Replace("/", "\\")));
                    return bi;
                }
                catch { return null; }
            }
        }

        public bool Zaznaczony { get { return zaznaczony; } set { zaznaczony = value; RaisePropertyChanged("Zaznaczony"); } }
        
        #endregion

        #region constructors
        public TowarVM(bool premium)
        {
            towar = new Towar();
            DanePoczatkowe();           
        }

        public TowarVM(Towar towar)
        {
            this.towar = towar;
            DanePoczatkowe();            
        }

        #endregion

        #region commands
        private RelayCommand zapiszDaneTowaruCommand;

        public RelayCommand ZapiszDaneTowaruCommand
        {
            get { if (zapiszDaneTowaruCommand == null) zapiszDaneTowaruCommand = new RelayCommand(ZapiszDaneTowaru); return zapiszDaneTowaruCommand; }
        }        
        #endregion

        #region methods
        private void DanePoczatkowe()
        {
            try
            {
                dialog = new DialogService();
                PobierzListeWydan();
                PobierzListePrzyjec();
            }
            catch (Exception ex) { dialog.ShowError(ex); log.Error(ex); throw ex; }
        }
        internal void Save()
        {
            try
            {
                towar.Save();
            }
            catch (Exception ex) { dialog.ShowError(ex); log.Error(ex); throw ex; }
        }

        internal void Remove()
        {
            try
            {
                towar.Remove();
            }
            catch (Exception ex) { dialog.ShowError(ex); log.Error(ex); throw ex; }
        }
        public void PobierzListeWydan()
        {
            try
            {
                List<WydanieView> lista = DbAdapterEF.GetListaWydan(Id);
                if (lista.Count > 0)
                    ListaWydan = new ObservableCollection<WydanieViewVM>(lista.Select(w => new WydanieViewVM(w)));
                else
                    ListaWydan = new ObservableCollection<WydanieViewVM>();
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        private void PobierzListePrzyjec()
        {
            try
            {
                List<Przyjecie> lista = DbAdapterEF.GetListaPrzyjec(Id);
                if (lista.Count > 0)
                    ListaPrzyjec = new ObservableCollection<PrzyjecieVM>(lista.Select(p => new PrzyjecieVM(p)));
                else
                    ListaPrzyjec = new ObservableCollection<PrzyjecieVM>();
            }
            catch (Exception ex) { dialog.ShowError(ex); log.Error(ex); throw ex; }
        }

        private void ZapiszDaneTowaru()
        {
            try
            {
                string walidacja = Towar.Validate();
                if (walidacja.Equals(""))
                {
                    Save();                    
                }
                else
                    dialog.ShowWarning(walidacja);
            }
            catch (Exception ex) { dialog.ShowError(ex); log.Error(ex); throw ex; }
        }

        public void OdswiezCechy()
        {
            RaisePropertyChanged("IsTowarDoPobrania");
            RaisePropertyChanged("IsTowarDoWydania");
            RaisePropertyChanged("IsTowarDoPrzyjecia");
            RaisePropertyChanged("ListaWydan");
            RaisePropertyChanged("ListaPrzyjec");            
            // TODO Delegate
        }
        public string Przekaz(int idPracownika)
        {
            try
            {
                WydanieVM wydanie = new WydanieVM();                
                if (IdBrygadzisty > 0)
                    wydanie.IdWydajacego = IdBrygadzisty;
                wydanie.IdPrzyjmujacego = idPracownika;
                wydanie.IdTowaru = Id;
                string walidacja = wydanie.Validate();
                if (walidacja.Equals(""))
                {
                    if (wydanie.Ilosc > StanMagazynowy)
                        return "brak w magazynie";
                    wydanie.Save();
                    PobierzListeWydan();
                    Zaznaczony = false;
                    OdswiezCechy();                    
                    return "";
                }
                else
                {                    
                    return walidacja;
                }
            }
            catch(Exception ex) { dialog.ShowError(ex); log.Error(ex); throw ex; }
        }
        private void GetTyp(string nazwa)
        {
            try
            {
                IdTypu = Towar.GetIdTypuTowaru(nazwa);
            }
            catch (Exception ex) { dialog.ShowError(ex); log.Error(ex); throw ex; }
        }
        private void GetMarka(string nazwa)
        {
            try
            {
                IdMarki = Towar.GetIdMarkiTowaru(nazwa);
            }
            catch (Exception ex) { dialog.ShowError(ex); log.Error(ex); throw ex; }
        }
        #endregion

        #region static methods
        public static List<TypTowaruVM> PobierzTypyTowarow()
        {
            try
            {
                //return DbAdapterEF.GetListaTypowTowarow().Select(t => new TypTowaruVM(t)).ToList();
                List<TypTowaruVM> lista = new List<TypTowaruVM>();
                foreach (TypTowaru typ in Towar.PobierzTypyTowarow())
                {
                    lista.Add(new TypTowaruVM(typ));
                }
                return lista;
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void UsunTypTowaru(TypTowaruVM typ)
        {
            try
            {
                Towar.UsunTypTowaru(typ.Typ);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        public static List<MarkaTowaruVM> PobierzMarkiTowarow()
        {
            try
            {
                //return DbAdapterEF.GetListaTypowTowarow().Select(t => new TypTowaruVM(t)).ToList();
                List<MarkaTowaruVM> lista = new List<MarkaTowaruVM>();
                foreach (MarkaTowaru marka in Towar.PobierzMarkiTowarow())
                {
                    lista.Add(new MarkaTowaruVM(marka));
                }
                return lista;
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal static void UsunMarkeTowaru(MarkaTowaruVM marka)
        {
            try
            {
                Towar.UsunMarkeTowaru(marka.Marka);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        #endregion
    }
}
