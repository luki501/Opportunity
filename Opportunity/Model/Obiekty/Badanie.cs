using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Opportunity.Model.Database;

namespace Opportunity.Model.Obiekty
{
    public class Badanie : ADokument
    {
        #region fields
        private Badania badanieDb;
        private DateTime dataWystawienia;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public override int Id { get { return badanieDb.id; } }
        public override string Numer { get { return badanieDb.numer; } set { badanieDb.numer = value; } }
        public override DateTime? DataWystawienia { get { return badanieDb.data_wydania; } set { badanieDb.data_wydania = value; } }
        public override DateTime? DataWaznosci { get { return badanieDb.data_waznosci; } set { badanieDb.data_waznosci = value; } }
        public override string Uwagi { get { return badanieDb.uwagi; } set { badanieDb.uwagi = value; } }
        public override string Path { get { return badanieDb.path; } set { badanieDb.path = value; } }
        public override DateTime LastUpdate { get { return badanieDb.last_update; } }
        public Badania BadanieDb { get { return badanieDb; } }
        #endregion

        #region constructors
        public Badanie()
        {
            badanieDb = new Badania();
        }
        public Badanie(Badania badania)
        {
            badanieDb = badania;            
        }

        public Badanie(string numer, DateTime dataWystawienia, DateTime dataWaznosci, string uwagi)
        {
            badanieDb = new Badania();
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
                    badanieDb.id_pracownika = idPracownika;
                    DbAdapterEF.BadanieInsert(this);
                }
                else
                    DbAdapterEF.BadanieUpdate(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        public override void Remove()
        {
            try
            {
                DbAdapterEF.BadanieRemove(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
