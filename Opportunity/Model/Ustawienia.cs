using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Model
{
    public sealed class Ustawienia
    {
        private static Ustawienia _ustawienia;
        private static int? idUzytkownika;
        private static string nazwaUzytkownika;
        static ILog log = LogManager.GetLogger("LogFileAppender");

        private Ustawienia()
        {
            _ustawienia = new Ustawienia();            
        }

        public static Ustawienia DaneAplikacji { get { return _ustawienia; } }

        public static string FtpPath
        {
            get { return Opportunity.Properties.Settings.Default["ftp_path"].ToString(); }
            set { Opportunity.Properties.Settings.Default["ftp_path"] = value; }
        }
        public static string FtpUser
        {
            get { return Opportunity.Properties.Settings.Default["ftp_user"].ToString(); }
            set { Opportunity.Properties.Settings.Default["ftp_user"] = value; }
        }
        public static string FtpPassword
        {
            get { return Opportunity.Properties.Settings.Default["ftp_password"].ToString(); }
            set { Opportunity.Properties.Settings.Default["ftp_password"] = value; }
        }
        public static string DomyslnyTypTowaru
        {
            get { return Opportunity.Properties.Settings.Default["domyslny_typ_towaru"].ToString(); }
            set { Opportunity.Properties.Settings.Default["domyslny_typ_towaru"] = value; }
        }
        public static string DataSource
        {
            get { return Opportunity.Properties.Settings.Default["datasource"].ToString(); }
            set { Opportunity.Properties.Settings.Default["datasource"] = value; }
        }
        public static string MediaPath
        {
            get { return Opportunity.Properties.Settings.Default["media_path"].ToString(); }
            set { Opportunity.Properties.Settings.Default["media_path"] = value; }
        }
        public static void Zapisz()
        {
            Opportunity.Properties.Settings.Default.Save();
        }

        public static bool ZapiszPlikFtp(string fileName)
        {
            try
            {
                string path = string.Format("{0}/{1}", Ustawienia.FtpPath, Path.GetFileName(fileName));
                return Tools.ZapiszPlikFtp(fileName, path, FtpUser, FtpPassword);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        public static int? IdUzytkownika
        {
            get { return idUzytkownika; }
            set { idUzytkownika = value; }
        }
        public static string NazwaUzytkownika
        {
            get { return nazwaUzytkownika; }
            set { nazwaUzytkownika = value; }
        }
    }
}
