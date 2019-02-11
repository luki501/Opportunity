using log4net;
using Opportunity.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Model.Obiekty
{
    public class Zatrudnienie : ADokument
    {
        #region fields
        private Zatrudnienia zatrudnienieDb;        
        protected static ILog log = LogManager.GetLogger("LogFileAppender");

        public override int Id { get { return zatrudnienieDb.id; } }
        public override string Numer { get { return zatrudnienieDb.numer; } set { zatrudnienieDb.numer = value; } }
        public override DateTime? DataWystawienia { get { return zatrudnienieDb.data_waznosci_od; } set { zatrudnienieDb.data_waznosci_od = value; } }
        public override DateTime? DataWaznosci { get { return zatrudnienieDb.data_waznosci_do; } set { zatrudnienieDb.data_waznosci_do = value; } }
        public override string Uwagi { get { return zatrudnienieDb.uwagi; } set { zatrudnienieDb.uwagi = value; } }
        public override string Path { get { return zatrudnienieDb.path; } set { zatrudnienieDb.path = value; } }
        public override DateTime LastUpdate { get { return zatrudnienieDb.last_update; } }
        public Zatrudnienia ZatrudnienieDb { get { return zatrudnienieDb; } }
        #endregion

        #region constructors
        public Zatrudnienie()
        {
            zatrudnienieDb = new Zatrudnienia();
        }
        public Zatrudnienie(Zatrudnienia z)
        {
            zatrudnienieDb = z;
        }

        public Zatrudnienie(string numer, DateTime dataWystawienia, DateTime dataWaznosci, string uwagi)
        {
            zatrudnienieDb = new Zatrudnienia();
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
                    zatrudnienieDb.id_pracownika = idPracownika;
                    DbAdapterEF.ZatrudnienieInsert(this);
                }
                else
                    DbAdapterEF.ZatrudnienieUpdate(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        public override void Remove()
        {
            try
            {
                DbAdapterEF.ZatrudnienieRemove(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion        

    }
}
