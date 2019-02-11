using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Opportunity.Model.Database;

namespace Opportunity.Model.Obiekty
{
    public class Zezwolenie : ADokument
    {
        #region fields
        private Zezwolenia zezwolenieDb;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public override int Id { get { return zezwolenieDb.id; } }
        public override string Numer { get { return zezwolenieDb.numer; } set { zezwolenieDb.numer = value; } }
        public override DateTime? DataWystawienia { get { return zezwolenieDb.data_wystawienia; } set { zezwolenieDb.data_wystawienia = value; } }
        public override DateTime? DataWaznosci { get { return zezwolenieDb.data_waznosci; } set { zezwolenieDb.data_waznosci = value; } }
        public override string Uwagi { get { return zezwolenieDb.uwagi; } set { zezwolenieDb.uwagi = value; } }
        public override string Path { get { return zezwolenieDb.path; } set { zezwolenieDb.path = value; } }
        public override DateTime LastUpdate { get { return zezwolenieDb.last_update; } }
        public Zezwolenia ZezwolenieDb { get { return zezwolenieDb; } }
        #endregion

        #region constructors
        public Zezwolenie()
        {
            zezwolenieDb = new Zezwolenia();
        }
        public Zezwolenie(Zezwolenia zezwolenia)
        {
            zezwolenieDb = zezwolenia;
        }

        public Zezwolenie(string numer, DateTime dataWystawienia, DateTime dataWaznosci, string uwagi)
        {
            zezwolenieDb = new Zezwolenia();
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
                    zezwolenieDb.id_pracownika = idPracownika;
                    DbAdapterEF.ZezwolenieInsert(this);
                }
                else
                    DbAdapterEF.ZezwolenieUpdate(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        public override void Remove()
        {
            try
            {
                DbAdapterEF.ZezwolenieRemove(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
