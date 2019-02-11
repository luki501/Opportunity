using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Opportunity.Model.Database;

namespace Opportunity.Model.Obiekty
{
    public class Zameldowanie : ADokument
    {                
        #region fields
        private Zaproszenia zaproszenieDb;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public override int Id { get { return zaproszenieDb.id; } }
        public override string Numer { get { return zaproszenieDb.numer; } set { zaproszenieDb.numer = value; } }
        public override DateTime? DataWystawienia { get { return zaproszenieDb.data_wystawienia; } set { zaproszenieDb.data_wystawienia = value; } }
        public override DateTime? DataWaznosci { get { return zaproszenieDb.data_waznosci; } set { zaproszenieDb.data_waznosci = value; } }
        public override string Uwagi { get { return zaproszenieDb.uwagi; } set { zaproszenieDb.uwagi = value; } }
        public override string Path { get { return zaproszenieDb.path; } set { zaproszenieDb.path = value; } }
        public string Zapraszajacy { get { return zaproszenieDb.zapraszajacy; } set { zaproszenieDb.zapraszajacy = value; } }
        public bool? Odebrane { get { return zaproszenieDb.odebrane; } set { zaproszenieDb.odebrane = value; } }
        public bool? Oplacone { get { return zaproszenieDb.oplacone; } set { zaproszenieDb.oplacone = value; } }
        public override DateTime LastUpdate { get { return zaproszenieDb.last_update; } }
        public Zaproszenia ZaproszenieDb { get { return zaproszenieDb; } }
        #endregion

        #region constructors
        public Zameldowanie()
        {
            zaproszenieDb = new Zaproszenia();
        }
        public Zameldowanie(Zaproszenia zaproszenia)
        {
            zaproszenieDb = zaproszenia;
        }

        public Zameldowanie(string numer, DateTime dataWystawienia, DateTime dataWaznosci, string uwagi)
        {
            zaproszenieDb = new Zaproszenia();
            Numer = numer;
            if (dataWystawienia != DateTime.MinValue)
                DataWystawienia = dataWystawienia;
            if (dataWaznosci != DateTime.MinValue)
                DataWaznosci = dataWaznosci;
            Uwagi = uwagi;
        }
        #endregion

        #region methods
        public override void Save(int idPracownika)
        {
            try
            {
                if (Id == 0)
                {
                    zaproszenieDb.id_pracownika = idPracownika;
                    DbAdapterEF.ZameldowanieInsert(this);
                }
                else
                    DbAdapterEF.ZameldowanieUpdate(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        public override void Remove()
        {
            try
            {
                DbAdapterEF.ZameldowanieeRemove(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
