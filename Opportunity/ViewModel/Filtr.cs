using GalaSoft.MvvmLight;
using log4net;
using Opportunity.ViewModel.Obiekty;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel
{
    public class Filtr : ViewModelBase
    {
        #region fields
        private bool brak;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        private string numer;
        private DateTime? dataWaznosciMin;
        private DateTime? dataWaznosciMax;
        private bool wygasly;
        public bool brakDaty;

        public string Nazwa { get; set; }
        public bool Aktywny { get; set; }
        public Type TypFiltra { get; set; }
        public string Numer { get { return numer; } set { numer = value; RaisePropertyChanged("Numer"); } }
        public DateTime? DataWaznosciMin { get { return dataWaznosciMin; } set { dataWaznosciMin = value; if (value != null) BrakDaty = false; RaisePropertyChanged("DataWaznosciMin"); } }
        public DateTime? DataWaznosciMax { get { return dataWaznosciMax; } set { dataWaznosciMax = value; if (value != null) BrakDaty = false; RaisePropertyChanged("DataWaznosciMax"); } }
        public bool Wygasly { get { return wygasly; } set { wygasly = value; if (wygasly) BrakDaty = false; RaisePropertyChanged("Wygasly"); } }
        public bool BrakDaty { get { return brakDaty; } set { brakDaty = value; if (brakDaty) UstawFiltry(); RaisePropertyChanged("BrakDaty"); } }
        public bool Brak { get { return brak; } set { brak = value; } }        

        #endregion

        #region constructors
        public Filtr(Type typ)
        {
            TypFiltra = typ;
            Nazwa = typ.Name.Replace("VM", "");
        }
        #endregion

        #region methods        

        internal bool IsPracownikUkryty(PracownikVM pracownik)
        {
            try
            {
                if (Brak)
                {
                    if (pracownik.ListaDokumentow.Any(d => d.GetType() == TypFiltra))
                        return true;
                    else
                        return false;
                }
                else
                {
                    return !pracownik.ListaDokumentow.Any(d => d.GetType() == TypFiltra &&
                    (Numer == null || Numer.Equals("") || d.Numer.Contains(Numer))
                    &&
                    (dataWaznosciMin == null || d.DataWaznosci >= dataWaznosciMin)
                    &&
                    (dataWaznosciMax == null || d.DataWaznosci <= dataWaznosciMax)
                    &&
                    (!wygasly || d.DataWaznosci < DateTime.Now)
                    &&
                    (!brakDaty || d.DataWaznosci == null));
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        private void UstawFiltry()
        {
            try
            {
                if (brakDaty)
                {
                    DataWaznosciMin = null;
                    DataWaznosciMax = null;
                    Wygasly = false;
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
