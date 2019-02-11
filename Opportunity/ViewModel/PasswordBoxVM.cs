using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using log4net;
using Opportunity.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel
{
    public class PasswordBoxVM : ViewModelBase
    {
        #region fields
        private string login;
        private bool nowyUzytkownik;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public string Login { get { return login; } set { login = value; RaisePropertyChanged("Login"); } }
        public string PasswordOld { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public bool NowyUzytkownik { get { return nowyUzytkownik; } set { nowyUzytkownik = value; RaisePropertyChanged("NowyUzytkownik"); } }
        public bool IsHasloNadane { get; set; }
        #endregion

        #region constructors
        public PasswordBoxVM()
        {
            Password = "";
            Password2 = "";
        }
        #endregion

        #region commands
        private RelayCommand zapiszHasloCommand;
        public RelayCommand ZapiszHasloCommand
        {
            get
            {
                if (zapiszHasloCommand == null)
                    zapiszHasloCommand = new RelayCommand(ZapiszHaslo);
                return zapiszHasloCommand;
            }
        }
        #endregion

        #region methods
        private void ZapiszHaslo()
        {
            try
            {
                if (!Password.Equals("") && Password.Equals(Password2))
                {
                    if (NowyUzytkownik)
                    {
                        IsHasloNadane = DbAdapterEF.DodajUzytkownika(Login, Password);
                    }
                    else
                    {
                        IsHasloNadane = DbAdapterEF.ZmianaHasla(login, Password, PasswordOld);
                    }
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
