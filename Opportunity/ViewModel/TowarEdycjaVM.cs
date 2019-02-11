using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using log4net;
using Opportunity.Model.Obiekty;
using System;
using System.Collections.ObjectModel;

namespace Opportunity.ViewModel
{
    public class TowarEdycjaVM : ViewModelBase
    {
        #region fields
        private Towar nowyTowar;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        private Przyjecie nowePrzyjecie;        
        public Towar NowyTowar { get { return nowyTowar; } set { nowyTowar = value; RaisePropertyChanged("NowyTowar"); } }
        public Przyjecie NowePrzyjecie { get { return nowePrzyjecie; } set { nowePrzyjecie = value; RaisePropertyChanged("NowePrzyjecie"); } }
        public ObservableCollection<TypTowaru> ListaTypowTowarow
        {
            get { return new ObservableCollection<TypTowaru>(Towar.PobierzTypyTowarow()); }
        }
        public ObservableCollection<MarkaTowaru> ListaMarekTowarow
        {
            get { return new ObservableCollection<MarkaTowaru>(Towar.PobierzMarkiTowarow()); }
        }


        public bool IsTowarZapisany
        {
            get { if (NowyTowar.Id == 0) return false; else return true; }
        }
        public bool IsTowarPrzyjety
        {
            get { if (NowyTowar.IloscPrzyjetych > 0 && Premium) return true; else return false; }
        }
        public bool IsTowarDoPrzyjecia
        {
            get { if (IsTowarZapisany && !IsTowarPrzyjety) return true; else return false; }
        }

        public bool Premium { get { return nowyTowar.Premium; } set { nowyTowar.Premium = value; RaisePropertyChanged("Premium"); }}

        public int WybranyTypId { get { return nowyTowar.IdTypu; } set { nowyTowar.IdTypu = value; } }
        public int? WybranaMarkaId { get { return nowyTowar.IdMarki; } set { nowyTowar.IdMarki = value; } }
        #endregion

        #region constructors
        public TowarEdycjaVM()
        {
            NowyTowar = new Towar();
            NowePrzyjecie = new Przyjecie();
        }
        #endregion

        #region commands
        private RelayCommand zapiszTowarCommand;
        private RelayCommand dodajPrzyjecieCommand;

        public RelayCommand ZapiszTowarCommand
        {
            get
            {
                if (zapiszTowarCommand == null)
                    zapiszTowarCommand = new RelayCommand(ZapiszTowar);
                return zapiszTowarCommand;
            }
        }
        public RelayCommand DodajPrzyjecieCommand
        {
            get
            {
                if (dodajPrzyjecieCommand == null)
                    dodajPrzyjecieCommand = new RelayCommand(DodajPrzyjecie);
                return dodajPrzyjecieCommand;
            }
        }
        
        #endregion

        #region methods

        private void ZapiszTowar()
        {
            try
            {
                NowyTowar.Save(); // TODO premium not null
                NowePrzyjecie.IdTowaru = NowyTowar.Id;
                RaisePropertyChanged("IsTowarDoPrzyjecia");
                RaisePropertyChanged("IsTowarZapisany");
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        private void DodajPrzyjecie()
        {
            try
            {
                NowePrzyjecie.Save();
                NowyTowar.Przyjecia.Add(NowePrzyjecie);
                NowePrzyjecie = new Przyjecie();
                RaisePropertyChanged("IsTowarDoPrzyjecia");
            }
            catch(Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
