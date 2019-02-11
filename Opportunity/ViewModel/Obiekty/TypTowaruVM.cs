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
    public class TypTowaruVM : ViewModelBase
    {
        #region fields
        private TypTowaru typTowaru;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public int Id { get { return typTowaru.Id; } }
        public string Nazwa { get { return typTowaru.Nazwa; } set { typTowaru.Nazwa = value; RaisePropertyChanged("Nazwa"); } }
        public TypTowaru Typ { get { return typTowaru; } }
        #endregion

        #region constructors
        public TypTowaruVM(TypTowaru typ)
        {
            typTowaru = typ;
        }
        public TypTowaruVM()
        {
            typTowaru = new TypTowaru();
        }
        #endregion

        #region commands
        private RelayCommand typModeluZapiszCommand;  

        public RelayCommand TypModeluZapiszCommand {
            get { if (typModeluZapiszCommand == null) typModeluZapiszCommand = new RelayCommand(ZapiszZmiany); return typModeluZapiszCommand; } }        

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
                typTowaru.ZapiszZmiany();
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion

    }
}
