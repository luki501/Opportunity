using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using log4net;
using Opportunity.Model.Obiekty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel.Obiekty
{
    public class MarkaTowaruVM : ViewModelBase
    {
        #region fields
        private MarkaTowaru markaTowaru;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public int Id { get { return markaTowaru.Id; } }
        public string Nazwa { get { return markaTowaru.Nazwa; } set { markaTowaru.Nazwa = value; RaisePropertyChanged("Nazwa"); } }
        public MarkaTowaru Marka { get { return markaTowaru; } }
        #endregion

        #region constructors
        public MarkaTowaruVM(MarkaTowaru marka)
        {
            markaTowaru = marka;
        }
        public MarkaTowaruVM()
        {
            markaTowaru = new MarkaTowaru();
        }
        #endregion

        #region commands
        private RelayCommand markaTowaruZapiszCommand;

        public RelayCommand MarkaTowaruZapiszCommand
        {
            get { if (markaTowaruZapiszCommand == null) markaTowaruZapiszCommand = new RelayCommand(ZapiszZmiany); return markaTowaruZapiszCommand; }
        }

        #endregion

        #region methods
        public override string ToString()
        {
            return Nazwa;
        }
        private void ZapiszZmiany()
        {
            try
            {
                markaTowaru.ZapiszZmiany();
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
