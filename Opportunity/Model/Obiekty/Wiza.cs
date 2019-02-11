using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Opportunity.Model.Database;

namespace Opportunity.Model.Obiekty
{
    public class Wiza : ADokument
    {
        #region fields
        private Wizy wizaDb;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public override int Id { get { return wizaDb.id; } }
        public override string Numer { get { return wizaDb.numer; } set { wizaDb.numer = value; } }
        public override DateTime? DataWystawienia { get { return wizaDb.data_wystawienia; } set { wizaDb.data_wystawienia = value; } }
        public override DateTime? DataWaznosci { get { return wizaDb.data_waznosci; } set { wizaDb.data_waznosci = value; } }
        public override string Uwagi { get { return wizaDb.uwagi; } set { wizaDb.uwagi = value; } }
        public override string Path { get { return wizaDb.path; } set { wizaDb.path = value; } }
        public override DateTime LastUpdate { get { return wizaDb.last_update; } }
        public Wizy WizaDb { get { return wizaDb; } }
        #endregion

        #region constructors
        public Wiza()
        {
            wizaDb = new Wizy();
        }
        public Wiza(Wizy w)
        {
            wizaDb = w;
        }

        public Wiza(string numer, DateTime dataWystawienia, DateTime dataWaznosci, string uwagi)
        {
            wizaDb = new Wizy();
            Numer = numer;
            if (dataWystawienia != DateTime.MinValue)
                DataWystawienia = dataWystawienia.Date;
            if (dataWaznosci != DateTime.MinValue)
                DataWaznosci = dataWaznosci.Date;
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
                    wizaDb.id_pracownika = idPracownika;
                    DbAdapterEF.WizaInsert(this);
                }
                else
                    DbAdapterEF.WizaUpdate(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        public override void Remove()
        {
            try
            {
                DbAdapterEF.WizaRemove(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
