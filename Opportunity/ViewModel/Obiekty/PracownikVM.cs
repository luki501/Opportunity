using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using log4net;
using Opportunity.Model.Database;
using Opportunity.Model.Obiekty;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel.Obiekty
{
    public class PracownikVM : ViewModelBase
    {
        #region fields
        private Pracownik pracownik;
        private ADokumentVM nowyDokument;
        private string wybranyTypDokumentu;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public bool Ukryty { get; set; }
        public int Id { get { return pracownik.Id; } }
        public string Numer { get { return pracownik.Numer; } set { pracownik.Numer = value; } }
        public string Imie { get { return pracownik.Imie; } set { pracownik.Imie = value; } }
        public string Nazwisko { get { return pracownik.Nazwisko; } set { pracownik.Nazwisko = value; } }
        public bool Brygadzista { get { return pracownik.Brygadzista; } set { pracownik.Brygadzista = value; } }        

        public int? IdNadrzednego { get { return pracownik.IdNadrzednego; } set { pracownik.IdNadrzednego = value; } }
        public DateTime? DataUrodzenia { get { return pracownik.DataUrodzenia; } set { pracownik.DataUrodzenia = value; } }
        public string Uwagi { get { return pracownik.Uwagi; } set { pracownik.Uwagi = value; } } 
        public ObservableCollection<ADokumentVM> ListaDokumentow { get
            {
                return new ObservableCollection<ADokumentVM>(pracownik.ListaDokumentow.Select(d => DokumentManager.GetDokument(d)));
            }
        }
        public Paszport PaszportAktualny { get { return pracownik.PaszportAktualny; } }
        public SzkolenieBHP BHPAktualne { get { return pracownik.BHPAktualne; } }
        public Wiza WizaAktualna { get { return pracownik.WizaAktualna; } }
        public Oswiadczenie OswiadczenieAktualne { get { return pracownik.OswiadczenieAktualne; } }
        public Zezwolenie ZezwolenieAktualne { get { return pracownik.ZezwolenieAktualne; } }
        public Zameldowanie ZameldowanieAktualne { get { return pracownik.ZameldowanieAktualne; } }
        public Badanie BadanieAktualne { get { return pracownik.BadanieAktualne; } }
        public Zatrudnienie ZatrudnienieAktualne { get { return pracownik.ZatrudnienieAktualne; } }

        public Pracownik Pracownik { get { return pracownik; } }
        public ADokumentVM NowyDokument { get { return nowyDokument; } set { nowyDokument = value; RaisePropertyChanged("NowyDokument"); } }
        public List<string> ListaTypowDokumentow { get; set; }
        public string WybranyTypDokumentu { get { return wybranyTypDokumentu; } set { wybranyTypDokumentu = value; if (value != null) UstawNowyDokument(); RaisePropertyChanged("NowyDokument", "WybranyTypDokumentu"); } }

        public string DanePersonalne
        {
            get { return string.Format("{0} {1}", Nazwisko, Imie); }
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
        public PracownikVM()
        {            
            pracownik = new Pracownik();
            PobierzDane();            
        }

        public PracownikVM(Pracownik p)
        {
            pracownik = p;
            PobierzDane();
        }

        private void PobierzDane()
        {
            ListaTypowDokumentow = new List<string> { "Badanie", "Oswiadczenie", "Paszport", "Szkolenie", "Wiza", "Zameldowanie", "Zatrudnienie", "Zezwolenie" };
            Ukryty = false;
        }
        #endregion

        #region commands
        private RelayCommand zapiszDanePracownikaCommand;
        private RelayCommand<object[]> dodajDokumentCommand;
        private RelayCommand<ADokumentVM> usunDokumentCommand;
        private RelayCommand<ADokumentVM> zapiszDokumentCommand;        

        public RelayCommand ZapiszDanePracownikaCommand
        {
            get
            {
                if (zapiszDanePracownikaCommand == null)
                    zapiszDanePracownikaCommand = new RelayCommand(Save);
                return zapiszDanePracownikaCommand;
            }
        }
        public RelayCommand<object[]> DodajDokumentCommand
        {
            get
            {
                if (dodajDokumentCommand == null)
                    dodajDokumentCommand = new RelayCommand<object[]>(DodajDokument);
                return dodajDokumentCommand;
            }
        }
        public RelayCommand<ADokumentVM> UsunDokumentCommand
        {
            get
            {
                if (usunDokumentCommand == null)
                    usunDokumentCommand = new RelayCommand<ADokumentVM>(UsunDokument);
                return usunDokumentCommand;
            }
        }
        public RelayCommand<ADokumentVM> ZapiszDokumentCommand
        {
            get
            {
                if (zapiszDokumentCommand == null)
                    zapiszDokumentCommand = new RelayCommand<ADokumentVM>(ZapiszDokument);
                return zapiszDokumentCommand;
            }
        }

        #endregion

        #region methods
        internal void Save()
        {
            try
            {
                if (Brygadzista && (pracownik.Login == null || pracownik.Login.Equals("")))
                    pracownik.Login = pracownik.Numer;
                pracownik.Save();                
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal void Remove()
        {
            try
            {
                pracownik.Remove();
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        private void DodajDokument(object[] obj)
        {
            try
            {
                pracownik.DodajDokument(NowyDokument.Dokument);                
                WybranyTypDokumentu = null;
                NowyDokument = null;
                RaisePropertyChanged("ListaDokumentow");
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        private void UsunDokument(ADokumentVM dok)
        {
            try
            {
                pracownik.ListaDokumentow.Remove(dok.Dokument);
                pracownik.UsunDokument(dok.Dokument);
                dok.Dokument.Remove();
                RaisePropertyChanged("ListaDokumentow");
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        private void ZapiszDokument(ADokumentVM dok)
        {
            try
            {
                dok.Dokument.Save(Id);
                
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        private void UstawNowyDokument()
        {
            NowyDokument = DokumentManager.GetDokument(WybranyTypDokumentu);
        }

        internal void SprawdzWidocznosc(ObservableCollection<Filtr> listaFiltrow, bool filtrDokumentyWystawioneDzisiaj)
        {
            try
            {
                Ukryty = false;
                if (filtrDokumentyWystawioneDzisiaj)
                {
                    if (!ListaDokumentow.Any(d => d.LastUpdate.Date.Equals(DateTime.Now.Date)))
                        Ukryty = true;
                }
                else
                {
                    foreach (Filtr filtr in listaFiltrow.Where(f => f.Aktywny))
                    {
                        if (filtr.IsPracownikUkryty(this))
                        {
                            Ukryty = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        #endregion
    }
}
