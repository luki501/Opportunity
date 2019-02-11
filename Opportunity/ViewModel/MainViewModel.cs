using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using log4net;
using Opportunity.Model;
using Opportunity.Model.Database;
using Opportunity.Model.Obiekty;
using Opportunity.ViewModel.Obiekty;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace Opportunity.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region fields          
        private DialogService dialog;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        private ObservableCollection<PracownikVM> listaPracownikow;
        private ObservableCollection<TowarVM> listaTowarow;        
        private ObservableCollection<Filtr> listaFiltrow;
        private ObservableCollection<WydanieViewVM> listaWydanDoPotwierdzenia;
        private TowarVM towarWybrany;
        private TowarVM magazynWybrany;
        private PracownikVM pracownikWybrany;        
        
        public ObservableCollection<PracownikVM> ListaPracownikow { get { return listaPracownikow; } set { listaPracownikow = value; RaisePropertyChanged("ListaPracownikow"); } }
        public ObservableCollection<Filtr> ListaFiltrow { get { return listaFiltrow; } set { listaFiltrow = value; RaisePropertyChanged("ListaFiltrow"); } }
        public ObservableCollection<TowarVM> ListaTowarow { get { return listaTowarow; } set { listaTowarow = value; RaisePropertyChanged("ListaTowarow"); } }        
        public ObservableCollection<PracownikVM> ListaBrygadzistow { get { if (listaPracownikow != null) return new ObservableCollection<PracownikVM>(listaPracownikow.Where(p => p.Brygadzista)); else return new ObservableCollection<PracownikVM>(); } }

        public PracownikVM PracownikWybrany { get { return pracownikWybrany; } set { pracownikWybrany = value; RaisePropertyChanged("PracownikWybrany"); } }
        public TowarVM TowarWybrany { get { return towarWybrany; } set { towarWybrany = value; RaisePropertyChanged("TowarWybrany"); RaisePropertyChanged("IsTowarWybrany"); } }
        public TowarVM MagazynWybrany { get { return magazynWybrany; } set { magazynWybrany = value; RaisePropertyChanged("MagazynWybrany"); RaisePropertyChanged("IsMagazynWybrany"); RaisePropertyChanged("ListaPracownikowDlaMagazynu"); } }
        public List<PracownikVM> ListaPracownikowDlaMagazynu
        {
            get { if (listaPracownikow != null && listaPracownikow.Any(p => IsTowarUPracownika(p))) return listaPracownikow.Where(p => IsTowarUPracownika(p)).ToList(); else return new List<PracownikVM>(); }
        }

        private bool IsTowarUPracownika(PracownikVM pr)
        {
            decimal iloscPrzyjec = magazynWybrany.ListaWydan.Where(p => p.IdPrzyjmujacego == pr.Id).Sum(p => p.Ilosc);
            decimal iloscWydan = magazynWybrany.ListaWydan.Where(p => p.IdWydajacego == pr.Id).Sum(p => p.Ilosc);
            return iloscPrzyjec > iloscWydan;
        }

        #region filtry
        private string szukanaFrazaTowar;
        private string szukanaFrazaMagazyn;
        private string szukanaFrazaPracownik;
        private bool towaryFiltrWMagazynie;
        private bool towaryFiltrWydane;
        private bool towaryFiltrWycofane;
        private bool towaryFiltrBezPotwierdzenia;
        private bool magazynFiltrWMagazynie;
        private bool magazynFiltrWydane;
        private bool magazynFiltrWycofane;
        private bool magazynFiltrBezPotwierdzenia;
        private bool filtrDokumentyWystawioneDzisiaj;

        public bool FiltrDokumentyWystawioneDzisiaj { get { return filtrDokumentyWystawioneDzisiaj; } set { filtrDokumentyWystawioneDzisiaj = value; RaisePropertyChanged("PracownicyFiltr"); } }
        public int TowaryFiltrIdPracownika { get; set; }
        public bool TowaryFiltrWMagazynie { get { return towaryFiltrWMagazynie; } set { towaryFiltrWMagazynie = value; RaisePropertyChanged("TowaryFiltr"); } }
        public bool TowaryFiltrWydane { get { return towaryFiltrWydane; } set { towaryFiltrWydane = value; RaisePropertyChanged("TowaryFiltr"); } }
        public bool TowaryFiltrWycofane { get { return towaryFiltrWycofane; } set { towaryFiltrWycofane = value; RaisePropertyChanged("TowaryFiltr"); } }
        public bool TowaryFiltrBezPotwierdzenia { get { return towaryFiltrBezPotwierdzenia; } set { towaryFiltrBezPotwierdzenia = value; RaisePropertyChanged("TowaryFiltr"); } }
        public bool MagazynFiltrWMagazynie { get { return magazynFiltrWMagazynie; } set { magazynFiltrWMagazynie = value; RaisePropertyChanged("MagazynFiltr"); } }
        public bool MagazynFiltrWydane { get { return magazynFiltrWydane; } set { magazynFiltrWydane = value; RaisePropertyChanged("MagazynFiltr"); } }
        public bool MagazynFiltrWycofane { get { return magazynFiltrWycofane; } set { magazynFiltrWycofane = value; RaisePropertyChanged("MagazynFiltr"); } }
        public bool MagazynFiltrBezPotwierdzenia { get { return magazynFiltrBezPotwierdzenia; } set { magazynFiltrBezPotwierdzenia = value; RaisePropertyChanged("MagazynFiltr"); } }
        public string SzukanaFrazaTowar { get { return szukanaFrazaTowar; } set { szukanaFrazaTowar = value; RaisePropertyChanged("SzukanaFrazaTowar"); RaisePropertyChanged("TowaryFiltr"); } }
        public string SzukanaFrazaMagazyn { get { return szukanaFrazaMagazyn; } set { szukanaFrazaMagazyn = value; RaisePropertyChanged("SzukanaFrazaMagazyn"); RaisePropertyChanged("MagazynFiltr"); } }
        public string SzukanaFrazaPracownik { get { return szukanaFrazaPracownik; } set { szukanaFrazaPracownik = value; RaisePropertyChanged("SzukanaFrazaPracownik"); RaisePropertyChanged("PracownicyFiltr"); } }
        public ObservableCollection<PracownikVM> PracownicyFiltr
        {
            get
            {
                if (IsZalogowany)
                {
                    foreach (PracownikVM pracownik in ListaPracownikow)
                    {
                        pracownik.SprawdzWidocznosc(ListaFiltrow, filtrDokumentyWystawioneDzisiaj);
                        if (szukanaFrazaPracownik != null)
                            pracownik.Ukryty = !pracownik.Imie.ToLower().Contains(szukanaFrazaPracownik.ToLower()) 
                                && !pracownik.Nazwisko.ToLower().Contains(szukanaFrazaPracownik.ToLower()) 
                                && !pracownik.Numer.ToLower().Contains(szukanaFrazaPracownik.ToLower());
                    }
                    return new ObservableCollection<PracownikVM>(listaPracownikow.Where(p => !p.Ukryty));
                }
                return new ObservableCollection<PracownikVM>();
            }
        }
        public ListCollectionView TowaryFiltr
        {
            get
            {
                try
                {
                    List<TowarVM> lista = new List<TowarVM>();
                    if (IsZalogowany)
                    {
                        foreach (TowarVM towar in ListaTowarow)
                        {
                            if (!towar.Premium)
                                continue;
                            if (TowaryFiltrIdPracownika != 0 && towar.IdBrygadzisty != TowaryFiltrIdPracownika)
                                continue;
                            if (TowaryFiltrWMagazynie && towar.StanMagazynowy <= 0)
                                continue;
                            if (TowaryFiltrWydane && towar.StanWydanych <= 0)
                                continue;
                            if (TowaryFiltrWycofane && towar.IloscWycofanych <= 0)
                                continue;
                            if (TowaryFiltrBezPotwierdzenia && !towar.IsWydaniaBezPotwierdzenia)
                                continue;
                            if (!szukanaFrazaTowar.Equals("") && !towar.Marka.ToLower().Contains(szukanaFrazaTowar.ToLower()) && !towar.Model.ToLower().Contains(szukanaFrazaTowar.ToLower()))
                                continue;
                            lista.Add(towar);
                        }
                        ListCollectionView lcv = new ListCollectionView(lista);
                        lcv.GroupDescriptions.Add(new PropertyGroupDescription("NazwaTypu"));
                        return lcv;
                    }
                    else
                        return new ListCollectionView(new List<TowarVM>());
                }
                catch (Exception ex) { dialog.ShowError(ex.Message, "Test b³êdu"); log.Error(ex); throw ex; }
            }
        }
        public List<TowarVM> MagazynFiltr
        {
            get
            {
                try
                {
                    List<TowarVM> lista = new List<TowarVM>();
                    if (IsZalogowany)
                    {
                        foreach (TowarVM towar in listaTowarow)
                        {
                            if (towar.Premium)
                                continue;                            
                            if (MagazynFiltrWMagazynie && towar.StanMagazynowy <= 0)
                                continue;
                            if (MagazynFiltrWydane && towar.StanWydanych <= 0)
                                continue;
                            if (MagazynFiltrWycofane && towar.IloscWycofanych <= 0)
                                continue;
                            if (MagazynFiltrBezPotwierdzenia && !towar.IsWydaniaBezPotwierdzenia)
                                continue;
                            if (!szukanaFrazaMagazyn.Equals("") && !towar.Nazwa.ToLower().Contains(szukanaFrazaMagazyn.ToLower()) && !towar.Marka.ToLower().Contains(szukanaFrazaMagazyn.ToLower()) && !towar.Model.ToLower().Contains(szukanaFrazaMagazyn.ToLower()))
                                continue;
                            lista.Add(towar);
                        }
                        return lista;
                    }
                    else
                        return new List<TowarVM>();
                }
                catch (Exception ex) { dialog.ShowError(ex.Message, "Test b³êdu"); log.Error(ex); throw ex; }
            }
        }

        public ObservableCollection<WydanieViewVM> ListaWydanDoPotwierdzenia { get { return listaWydanDoPotwierdzenia; } set { listaWydanDoPotwierdzenia = value; } }

        public bool IsTowarWybrany { get { if (towarWybrany != null) return true; else return false; } }
        public bool IsMagazynWybrany { get { if (magazynWybrany != null) return true; else return false; } }
        #endregion

        #region ustawienia
        private TypTowaruVM nowyTypTowaru;
        private MarkaTowaruVM nowaMarkaTowaru;
        private string user;
        private string password;
        public string User { get { return user; } set { user = value; RaisePropertyChanged("User"); } }
        public string Password { get { return password; } set { password = value; RaisePropertyChanged("Password"); } }
        public string FtpPath { get { return Ustawienia.FtpPath; } set { Ustawienia.FtpPath = value; } }
        public string FtpUser { get { return Ustawienia.FtpUser; } set { Ustawienia.FtpUser = value; } }
        public string FtpPassword { get { return Ustawienia.FtpPassword; } set { Ustawienia.FtpPassword = value; } }
        public TypTowaruVM NowyTypTowaru { get { return nowyTypTowaru; } set { nowyTypTowaru = value; RaisePropertyChanged("NowyTypTowaru"); } }
        public MarkaTowaruVM NowaMarkaTowaru { get { return nowaMarkaTowaru; } set { nowaMarkaTowaru = value; RaisePropertyChanged("NowaMarkaTowaru"); } }
        public ObservableCollection<TypTowaruVM> ListaTypowTowarow
        {
            get { if (IsZalogowany) return new ObservableCollection<TypTowaruVM>(TowarVM.PobierzTypyTowarow()); else return new ObservableCollection<TypTowaruVM>(); }
        }
        public ObservableCollection<string> ListaNazwTypowTowarow
        {
            get { if (IsZalogowany) return new ObservableCollection<string>(Towar.PobierzTypyTowarow().Select(t => t.Nazwa)); else return new ObservableCollection<string>(); }
        }
        public ObservableCollection<MarkaTowaruVM> ListaMarekTowarow
        {
            get { if (IsZalogowany) return new ObservableCollection<MarkaTowaruVM>(TowarVM.PobierzMarkiTowarow()); else return new ObservableCollection<MarkaTowaruVM>(); }
        }
        public ObservableCollection<string> ListaNazwMarekTowarow
        {
            get { if (IsZalogowany) return new ObservableCollection<string>(Towar.PobierzMarkiTowarow().Select(t => t.Nazwa)); else return new ObservableCollection<string>(); }
        }
        public bool IsZalogowany { get { return DbAdapterEF.CheckStatus(); } }
        public bool IsAdmin { get { if (User.Equals("admin") || Uzytkownik == null) return true; else return false; } } // TYMCZASOWO       
        public Pracownik Uzytkownik { get; set; }
        #endregion

        #endregion

        #region constructors
        public MainViewModel()
        {
            dialog = new DialogService();
            log4net.Config.XmlConfigurator.Configure();
            // TYMCZASOWO DLA WYGODY
            User = "";
            Password = "";
            log.Info("START");
        }
        #endregion

        #region commands
        private RelayCommand zalogujCommand;
        private RelayCommand zapiszUstawieniaCommand;
        private RelayCommand dodajNowyTypTowaruCommand;
        private RelayCommand dodajNowaMarkeTowaruCommand;
        private RelayCommand odswiezListePracownikowCommand;
        private RelayCommand odswiezListeTowarowCommand;
        private RelayCommand odswiezListeMagazynCommand;
        private RelayCommand filtrAktywnyCommand;
        private RelayCommand<object> filtrBrygadzistaSelectedCommand;
        private RelayCommand<TypTowaruVM> usunTypTowaruCommand;
        private RelayCommand<MarkaTowaruVM> usunMarkeTowaruCommand;
        private RelayCommand<PracownikVM> przekazMasowoCommand;
        private RelayCommand<WydanieViewVM> potwierdzPrzyjecieCommand;
        private RelayCommand<object> nowePrzyjecieZapiszCommand;
        private RelayCommand<object> noweWydanieZapiszCommand;
        private RelayCommand<object> noweWycofanieZapiszCommand;
        private RelayCommand wylogujCommand;

        public RelayCommand ZalogujCommand
        {
            get { if (zalogujCommand == null) zalogujCommand = new RelayCommand(Zaloguj); return zalogujCommand; }
        }
        public RelayCommand ZapiszUstawieniaCommand
        {
            get { if (zapiszUstawieniaCommand == null) zapiszUstawieniaCommand = new RelayCommand(ZapiszUstawienia); return zapiszUstawieniaCommand; }
        }
        public RelayCommand DodajNowyTypTowaruCommand
        {
            get { if (dodajNowyTypTowaruCommand == null) dodajNowyTypTowaruCommand = new RelayCommand(DodajTypTowaru); return dodajNowyTypTowaruCommand; }
        }
        public RelayCommand DodajNowaMarkeTowaruCommand
        {
            get { if (dodajNowaMarkeTowaruCommand == null) dodajNowaMarkeTowaruCommand = new RelayCommand(DodajMarkeTowaru); return dodajNowaMarkeTowaruCommand; }
        }

        public RelayCommand OdswiezListePracownikowCommand
        {
            get { if (odswiezListePracownikowCommand == null) odswiezListePracownikowCommand = new RelayCommand(OdswiezListePracownikow); return odswiezListePracownikowCommand; }
        }
        public RelayCommand FiltrAktywnyCommand
        {
            get { if (filtrAktywnyCommand == null) filtrAktywnyCommand = new RelayCommand(ZmianaDanychFiltraPracownikow); return filtrAktywnyCommand; }
        }
        public RelayCommand<object> FiltrBrygadzistaSelectedCommand
        {
            get { if (filtrBrygadzistaSelectedCommand == null) filtrBrygadzistaSelectedCommand = new RelayCommand<object>(UstawFiltry); return filtrBrygadzistaSelectedCommand; }
        }
        public RelayCommand OdswiezListeTowarowCommand
        {
            get { if (odswiezListeTowarowCommand == null) odswiezListeTowarowCommand = new RelayCommand(OdswiezListeTowarow); return odswiezListeTowarowCommand; }
        }
        public RelayCommand OdswiezListeMagazynCommand
        {
            get { if (odswiezListeMagazynCommand == null) odswiezListeMagazynCommand = new RelayCommand(OdswiezListeMagazyn); return odswiezListeMagazynCommand; }
        }
        public RelayCommand<TypTowaruVM> UsunTypTowaruCommand
        {
            get { if (usunTypTowaruCommand == null) usunTypTowaruCommand = new RelayCommand<TypTowaruVM>(UsunTypTowaru); return usunTypTowaruCommand; }
        }
        public RelayCommand<MarkaTowaruVM> UsunMarkeTowaruCommand
        {
            get { if (usunMarkeTowaruCommand == null) usunMarkeTowaruCommand = new RelayCommand<MarkaTowaruVM>(UsunMarke); return usunMarkeTowaruCommand; }
        }

        public RelayCommand<PracownikVM> PrzekazMasowoCommand
        {
            get { if (przekazMasowoCommand == null) przekazMasowoCommand = new RelayCommand<PracownikVM>(PrzekazMasowoNarzedzia); return przekazMasowoCommand; }
        }
        public RelayCommand<WydanieViewVM> PotwierdzPrzyjecieCommand
        {
            get { if (potwierdzPrzyjecieCommand == null) potwierdzPrzyjecieCommand = new RelayCommand<WydanieViewVM>(PotwierdzPrzyjecie); return potwierdzPrzyjecieCommand; }
        }
        public RelayCommand<object> NowePrzyjecieZapiszCommand
        {
            get { if (nowePrzyjecieZapiszCommand == null) nowePrzyjecieZapiszCommand = new RelayCommand<object>(NowePrzyjecieZapisz); return nowePrzyjecieZapiszCommand; }
        }
        public RelayCommand<object> NoweWydanieZapiszCommand
        {
            get { if (noweWydanieZapiszCommand == null) noweWydanieZapiszCommand = new RelayCommand<object>(NoweWydanieZapisz); return noweWydanieZapiszCommand; }
        }
        public RelayCommand<object> NoweWycofanieZapiszCommand
        {
            get { if (noweWycofanieZapiszCommand == null) noweWycofanieZapiszCommand = new RelayCommand<object>(NoweWycofanieZapisz); return noweWycofanieZapiszCommand; }
        }
        public RelayCommand WylogujCommand
        {
            get { if (wylogujCommand == null) wylogujCommand = new RelayCommand(Wyloguj); return wylogujCommand; }
        }

        #endregion

        #region methods
        private void Zaloguj()
        {
            DbAdapterEF.User = User;
            DbAdapterEF.Password = Password;
            if (IsZalogowany)
            {
                DanePoczatkowe();
            }
            else
                dialog.ShowWarning("B³¹d po³aczenia z baz¹ danych");
            
        }
        private void DanePoczatkowe()
        {
            try
            {
                dialog = new DialogService();
                szukanaFrazaTowar = "";
                szukanaFrazaMagazyn = "";
                Uzytkownik = DbAdapterEF.GetUzytkownika(User);                
                Ustawienia.IdUzytkownika = IsAdmin? 0 : Uzytkownik.Id;
                Ustawienia.NazwaUzytkownika = IsAdmin ? "admin" : Uzytkownik.Login;
                if (Uzytkownik != null && (Uzytkownik.Password == null || Uzytkownik.Password.Equals("")))
                {
                    Uzytkownik.Password = Tools.SHA256(password);
                    Uzytkownik.Save();
                }
                NowyTypTowaru = new TypTowaruVM();
                NowaMarkaTowaru = new MarkaTowaruVM();
                PobierzListeFiltrow();
                PobierzListePracownikow();                
                PobierzListeTowarow();
                PobierzListePotwierdzen();
                RaisePropertyChanged("IsZalogowany");
                RaisePropertyChanged("ListaTypowTowarow");
                RaisePropertyChanged("ListaNazwTypowTowarow");
                RaisePropertyChanged("ListaMarekTowarow");
                RaisePropertyChanged("ListaNazwMarekTowarow");
                RaisePropertyChanged("PracownicyFiltr");
                RaisePropertyChanged("TowaryFiltr");
                RaisePropertyChanged("MagazynFiltr");
                RaisePropertyChanged("IsAdmin");
                RaisePropertyChanged("ListaWydanDoPotwierdzenia");
                
            }
            catch (Exception ex) { dialog.ShowError(ex); log.Error(ex); }
        }

        private void PobierzListePracownikow()
        {
            try
            {
                List<Pracownik> lista = DbAdapterEF.GetListaPracownikow();
                listaPracownikow = new ObservableCollection<PracownikVM>();
                foreach (Pracownik p in lista)
                {
                    listaPracownikow.Add(new PracownikVM(p));
                }
                RaisePropertyChanged("ListaPracownikow");
                RaisePropertyChanged("ListaBrygadzistow");
                RaisePropertyChanged("PracownicyFiltr");                
            }
            catch (Exception ex) { dialog.ShowError(ex); log.Error(ex); throw ex; }
        }
        private void PobierzListeFiltrow()
        {
            try
            {
                listaFiltrow = new ObservableCollection<Filtr>()
                {
                    new Filtr(typeof(BadanieVM)),
                    new Filtr(typeof(OswiadczenieVM)),
                    new Filtr(typeof(PaszportVM)),
                    new Filtr(typeof(SzkolenieVM)),
                    new Filtr(typeof(WizaVM)),
                    new Filtr(typeof(ZameldowanieVM)),
                    new Filtr(typeof(ZezwolenieVM)),
                    new Filtr(typeof(ZatrudnienieVM))
                };
                RaisePropertyChanged("ListaFiltrow");
            }
            catch (Exception ex) { dialog.ShowError(ex); log.Error(ex); throw ex; }
        }
        private void PobierzListeTowarow()
        {
            try
            {
                List<Towar> lista = DbAdapterEF.GetListaTowarow();
                listaTowarow = new ObservableCollection<TowarVM>();
                foreach (Towar t in lista)
                {
                    listaTowarow.Add(new TowarVM(t));
                }                             
            }
            catch (Exception ex) { dialog.ShowError(ex); log.Error(ex); throw ex; }
        }
        private void PobierzListePotwierdzen()
        {
            if (IsZalogowany)
            {
                int id = IsAdmin ? 0 : Uzytkownik.Id;
                List<WydanieView> lista = DbAdapterEF.GetListaWydan();
                if (lista.Any(w => w.IsDoPotwierdzeniaPrzyjecia(id)))
                    listaWydanDoPotwierdzenia = new ObservableCollection<WydanieViewVM>(lista.Where(w => w.IsDoPotwierdzeniaPrzyjecia(id)).Select(w => new WydanieViewVM(w)));
                else
                    listaWydanDoPotwierdzenia = new ObservableCollection<WydanieViewVM>();
            }
            else
            {
                listaWydanDoPotwierdzenia = new ObservableCollection<WydanieViewVM>();
            }
        }

        public void OdswiezListeTowarow()
        {
            try
            {
                int idWybrane = 0;
                if (TowarWybrany != null)
                    idWybrane = TowarWybrany.Id;                
                PobierzListeTowarow();
                RaisePropertyChanged("TowaryFiltr");
                if (idWybrane > 0)
                {
                    TowarWybrany = ListaTowarow.Where(t => t.Id == idWybrane).First();
                    RaisePropertyChanged("TowarWybrany");
                }
            }
            catch (Exception ex) { dialog.ShowError(ex); log.Error(ex); throw ex; }
        }
        public void OdswiezTowarWybrany()
        {
            RaisePropertyChanged("TowarWybrany");
        }
        public void OdswiezListePracownikow()
        {
            PobierzListePracownikow();
        }
        public void OdswiezListeMagazyn()
        {
            try
            {
                PobierzListeTowarow();
                RaisePropertyChanged("MagazynFiltr");
            }
            catch (Exception ex) { dialog.ShowError(ex); log.Error(ex); throw ex; }
        }

        private void ZapiszDanePracownika(int id)
        {
            try
            {
                PracownikVM pracownik = ListaPracownikow.Where(p => p.Id == id).First();
                pracownik.Save();
                RaisePropertyChanged("PracownicyFiltr");
            }
            catch (Exception ex) { dialog.ShowError(ex); log.Error(ex); throw ex; }
        }
        private void ZapiszUstawienia()
        {
            try
            {
                Ustawienia.Zapisz();
            }
            catch (Exception ex) { dialog.ShowError(ex); log.Error(ex); throw ex; }
        }
        private void ZmianaDanychFiltraPracownikow()
        {
            try
            {
                RaisePropertyChanged("PracownicyFiltr");
            }
            catch (Exception ex) { dialog.ShowError(ex); log.Error(ex); throw ex; }
        }
        
        private void UstawFiltry(object pracownik)
        {
            try
            {
                if (pracownik is PracownikVM)
                    TowaryFiltrIdPracownika = (pracownik as PracownikVM).Id;
                else
                    TowaryFiltrIdPracownika = 0;
                RaisePropertyChanged("TowaryFiltr");
            }
            catch (Exception ex) { dialog.ShowError(ex); log.Error(ex); throw ex; }
        }

        private void ZapiszDaneTowaru()
        {
            try
            {
                string walidacja = towarWybrany.Towar.Validate();
                if (walidacja.Equals(""))
                {
                    towarWybrany.Save();
                    RaisePropertyChanged("ListCollectionTowary");
                }
                else
                    dialog.ShowInfo(walidacja, "Ostrze¿enie");
            }
            catch (Exception ex) { dialog.ShowError(ex); log.Error(ex); throw ex; }
        }
        private void DodajTypTowaru()
        {
            try
            {
                Towar.ZapiszNowyTypTowaru(nowyTypTowaru.Nazwa);                
                RaisePropertyChanged("ListaTypowTowarow");
            }
            catch (Exception ex) { dialog.ShowError(ex, "B³¹d zapisu typu towaru"); log.Error(ex); throw ex; }
            finally { NowyTypTowaru = new TypTowaruVM(); }
        }
        private void UsunTypTowaru(TypTowaruVM typ)
        {
            try
            {
                if (typ == null)
                {
                    dialog.ShowError("Zaznacz typ towaru", "Uwaga");
                    return;
                }
                if (listaTowarow.Any(t => t.IdTypu == typ.Id))
                {
                    List<string> lista = listaTowarow.Where(t => t.IdTypu == typ.Id).Select(t => t.Nazwa).ToList();
                    dialog.ShowError(String.Join(", ", lista.ToArray()), "Typ towaru przypisany");
                    return;
                }
                ListaTypowTowarow.Remove(typ);
                TowarVM.UsunTypTowaru(typ);
                NowyTypTowaru = null;
                RaisePropertyChanged("ListaTypowTowarow");
            }
            catch (Exception ex) { dialog.ShowError(ex, "B³¹d usuwania typu towaru"); log.Error(ex); throw ex; }
        }
        private void UsunMarke(MarkaTowaruVM marka)
        {
            try
            {
                if (marka == null)
                {
                    dialog.ShowError("Zaznacz  markê", "Uwaga");
                    return;
                }
                if (listaTowarow.Any(t => t.IdMarki == marka.Id))
                {
                    List<string> lista = listaTowarow.Where(t => t.IdMarki == marka.Id).Select(t => t.Nazwa).ToList();
                    dialog.ShowError(String.Join(", ", lista.ToArray()), "Marka towaru przypisana");
                    return;
                }
                ListaMarekTowarow.Remove(marka);
                TowarVM.UsunMarkeTowaru(marka);
                NowaMarkaTowaru = null;
                RaisePropertyChanged("ListaMarekTowarow");
            }
            catch (Exception ex) { dialog.ShowError(ex, "B³¹d usuwania marki towaru"); log.Error(ex); throw ex; }
        }
        private void DodajMarkeTowaru()
        {
            try
            {
                if (nowaMarkaTowaru.Nazwa != null && !nowaMarkaTowaru.Nazwa.Equals(""))
                {
                    Towar.ZapiszNowaMarkeTowaru(nowaMarkaTowaru.Nazwa);
                    RaisePropertyChanged("ListaMarekTowarow");
                }
            }
            catch (Exception ex) { dialog.ShowError(ex, "B³¹d zapisu typu marki"); log.Error(ex); throw ex; }
            finally { NowaMarkaTowaru = new MarkaTowaruVM(); }
        }
        private void PrzekazMasowoNarzedzia(PracownikVM pracownik)
        {
            try
            {
                if (pracownik != null)
                {
                    string komunikat = "";                                        
                    foreach (TowarVM towar in TowaryFiltr)
                    {
                        if (towar.Zaznaczony)
                        {
                            komunikat += string.Format("{0} - {1}\n", towar.Premium ? towar.Marka : towar.Nazwa, towar.Przekaz(pracownik.Id));                            
                        }
                    }
                    dialog.ShowInfo(komunikat);
                }
                OdswiezListeTowarow();
            }
            catch (Exception ex) { dialog.ShowError(ex, "B³¹d przekazywania narzêdzi"); log.Error(ex); throw ex; }
        }

        private void PotwierdzPrzyjecie(WydanieViewVM wydanie)
        {
            try
            {
                wydanie.PotwierdzPrzyjecie();
                // Na ¿yczenie Kalosza edytujemy równie¿ wpis w tabeli wydania                
                PobierzListePotwierdzen();
                RaisePropertyChanged("ListaWydanDoPotwierdzenia");
                RaisePropertyChanged("TowarWybrany");
            }
            catch (Exception ex) { dialog.ShowError(ex, "B³¹d potwierdzenia wydania"); log.Error(ex); throw ex; }
        }

        private void NowePrzyjecieZapisz(object parameters)
        {
            try
            {
                var values = (object[])parameters;
                Przyjecie przyjecie = new Przyjecie();
                przyjecie.DataPrzyjecia = Convert.ToDateTime(values[0]).Date.Equals(DateTime.Now.Date) ? DateTime.Now : Convert.ToDateTime(values[0]);
                if (przyjecie.DataPrzyjecia.Date.Equals(DateTime.Now))
                    przyjecie.DataPrzyjecia = DateTime.Now;
                przyjecie.IdTowaru = TowarWybrany.Id;
                przyjecie.NumerFaktury = values[1].ToString();
                przyjecie.Path = TowarWybrany.PathPrzyjecie;
                przyjecie.Uwagi = values[2].ToString();
                string walidacja = przyjecie.Validate();
                if (walidacja.Equals(""))
                {
                    przyjecie.Save();
                    TowarWybrany.OdswiezCechy();
                    OdswiezListeTowarow();
                }
                else
                    dialog.ShowWarning(walidacja);
                
            }
            catch (Exception ex) { dialog.ShowError(ex, "B³¹d przyjêcia towaru"); log.Error(ex); throw ex; }
        }
        private void NoweWydanieZapisz(object parameters)
        {
            try
            {
                var values = (object[])parameters;
                Wydanie wydanie = new Wydanie();
                wydanie.DataWydania = Convert.ToDateTime(values[0]).Date.Equals(DateTime.Now.Date) ? DateTime.Now : Convert.ToDateTime(values[0]);
                wydanie.IdTowaru = TowarWybrany.Id;                
                if (Convert.ToInt32(values[1]) > 0)
                    wydanie.IdPrzyjmujacego = Convert.ToInt32(values[1]);
                if (TowarWybrany.IdBrygadzisty > 0)
                    wydanie.IdWydajacego = TowarWybrany.IdBrygadzisty;
                wydanie.Uwagi = values[2].ToString();
                string walidacja = wydanie.Validate();
                if (walidacja.Equals(""))
                {
                    wydanie.Save();
                    TowarWybrany.OdswiezCechy();
                    OdswiezListeTowarow();
                    PobierzListePotwierdzen();
                    RaisePropertyChanged("ListaWydanDoPotwierdzenia");
                }
                else
                    dialog.ShowWarning(walidacja);
            }
            catch (Exception ex) { dialog.ShowError(ex, "B³¹d wydania towaru"); log.Error(ex); throw ex; }
        }
        private void NoweWycofanieZapisz(object parameters)
        {
            try
            {
                var values = (object[])parameters;
                Wycofanie wycofanie = new Wycofanie();
                wycofanie.DataWycofania = Convert.ToDateTime(values[0]).Date.Equals(DateTime.Now.Date) ? DateTime.Now : Convert.ToDateTime(values[0]);
                wycofanie.IdTowaru = TowarWybrany.Id;
                wycofanie.Uwagi = values[2].ToString();
                string walidacja = wycofanie.Validate();
                if (walidacja.Equals(""))
                {
                    wycofanie.Save();
                    TowarWybrany.OdswiezCechy();
                    OdswiezListeTowarow();
                }
                else
                    dialog.ShowWarning(walidacja);
            }
            catch (Exception ex) { dialog.ShowError(ex, "B³¹d wycofywania towaru"); log.Error(ex); throw ex; }
        }
        private void Wyloguj()
        {
            DbAdapterEF.User = "";
            DbAdapterEF.Password = "";
            RaisePropertyChanged("IsZalogowany");
        }
        #endregion
    }
}
