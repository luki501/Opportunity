using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using log4net;
using Opportunity.Model;
using Opportunity.Model.Obiekty;
using Opportunity.ViewModel.Obiekty;
using System;
using System.Collections.Generic;

namespace Opportunity.ViewModel
{
    public class PracownikEdycjaVM : ViewModelBase
    {
        #region fields
        private DialogService dialog;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        private ADokumentVM nowyDokument;
        private string wybranyTypDokumentu;
        public Pracownik NowyPracownik { get; set; }        
        public ADokumentVM NowyDokument { get { return nowyDokument; } set { nowyDokument = value; RaisePropertyChanged("NowyDokument"); } }
        public List<string> ListaTypowDokumentow { get; set; }
        public string WybranyTypDokumentu { get { return wybranyTypDokumentu; } set { wybranyTypDokumentu = value; if (value != null) UstawNowyDokument(); RaisePropertyChanged("NowyDokument", "WybranyTypDokumentu"); } }
        public bool PracownikZapisany
        {
            get
            {
                if (NowyPracownik.Id == 0)
                    return false;
                else
                    return true;
            }
        }
        public string Haslo { get; set; }
        public string Haslo2 { get; set; }
        #endregion

        #region constructors
        public PracownikEdycjaVM()
        {
            NowyPracownik = new Pracownik();
            PobierzDane();
        }
        #endregion

        #region commands
        private RelayCommand zapiszPracownikaCommand;
        private RelayCommand<object[]> dodajDokumentCommand;
        public RelayCommand ZapiszPracownikaCommand
        {
            get
            {
                if (zapiszPracownikaCommand == null)
                    zapiszPracownikaCommand = new RelayCommand(ZapiszPracownika);
                return zapiszPracownikaCommand;
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

        #endregion

        #region methods
        private void DodajDokument(object[] obj)
        {
            try
            {
                NowyPracownik.DodajDokument(NowyDokument.Dokument);
                wybranyTypDokumentu = null;
                NowyDokument = null;
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        private void PobierzDane()
        {
            dialog = new DialogService();
            ListaTypowDokumentow = new List<string> { "Badanie", "Oświadczenie", "Paszport", "Szkolenie", "Wiza", "Zameldowanie", "Zatrudnienie", "Zezwolenie" };
            Haslo = "";
            Haslo2 = "";            
        }
        private void ZapiszPracownika()
        {
            try
            {
                string walidacja = NowyPracownik.Validate();
                if (NowyPracownik.Brygadzista && NowyPracownik.Login == null)
                    walidacja = "Brygadzista musi mieć login i hasło";
                if (walidacja.Equals(""))
                {
                    NowyPracownik.Save();
                    RaisePropertyChanged("PracownikZapisany");
                }
                else
                    dialog.ShowWarning(walidacja);
            }
            catch (Exception ex) { log.Error(ex); dialog.ShowError(ex.Message); }
        }
        private void UstawNowyDokument()
        {
            try
            {
                NowyDokument = DokumentManager.GetDokument(WybranyTypDokumentu);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        internal bool ValidateBrygadzista()
        {
            try
            {
                return NowyPracownik.Validate().Equals("") && NowyPracownik.Brygadzista;
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
