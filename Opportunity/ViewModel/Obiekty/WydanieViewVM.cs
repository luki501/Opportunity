using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using log4net;
using Opportunity.Model;
using Opportunity.Model.Obiekty;
using System;

namespace Opportunity.ViewModel.Obiekty
{
    public class WydanieViewVM : ViewModelBase
    {
        #region fields
        private WydanieView wydanie;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public int Id { get { return wydanie.Id; } }
        public DateTime DataWydania { get { return wydanie.DataWydania; } }
        public decimal Ilosc { get { return wydanie.Ilosc; } }
        public int IdTowaru { get { return wydanie.IdTowaru; } }
        public string NazwaTowaru { get { return wydanie.NazwaTowaru; } }
        public int? IdWydajacego { get { return wydanie.IdWydajacego; } }
        public string Wydajacy { get { return wydanie.Wydajacy; } }
        public int? IdPrzyjmujacego { get { return wydanie.IdPrzyjmujacego; } }
        public string Przyjmujacy { get { return wydanie.Przyjmujacy; } }
        public DateTime? DataPotwierdzeniaWydania { get { return wydanie.DataPotwierdzeniaWydania; } }
        public DateTime? DataPotwierdzeniaPrzyjecia { get { return wydanie.DataPotwierdzeniaPrzyjecia; } }
        public bool IsPotwierdzeniePrzyjmujacego { get { if (DataPotwierdzeniaPrzyjecia == null) return false; else return true; } }
        public string PotwierdzenieUwagi { get; set; } // { return potwierdzenieUwagi; } set { potwierdzenieUwagi = value; RaisePropertyChanged("PotwierdzenieUwagi"); } }        
        #endregion

        #region constructors
        public WydanieViewVM(WydanieView wydanie)
        {
            this.wydanie = wydanie;            
        }
        #endregion

        #region commands
        #endregion

        #region methods
        public void PotwierdzPrzyjecie()
        {
            try
            {
                Potwierdzenie potwierdzenie = new Potwierdzenie();
                potwierdzenie.IdWydania = Id;
                potwierdzenie.DataPotwierdzenia = DateTime.Now;
                potwierdzenie.IdPracownika = 0;
                if (Ustawienia.IdUzytkownika != null)
                    potwierdzenie.IdPracownika = (int)Ustawienia.IdUzytkownika;
                potwierdzenie.Uwagi = PotwierdzenieUwagi;
                potwierdzenie.Save();
                wydanie.OznaczJakoWykonane(true);
            }
            catch(Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
