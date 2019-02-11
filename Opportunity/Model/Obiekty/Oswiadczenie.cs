using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Opportunity.Model.Database;

namespace Opportunity.Model.Obiekty
{
    public class Oswiadczenie : ADokument
    {                
        #region fields
        private Oswiadczenia oswiadczenieDb;
        private DateTime dataWystawienia;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");

        public override int Id { get { return oswiadczenieDb.id; } }
        public override string Numer { get { return oswiadczenieDb.numer; } set { oswiadczenieDb.numer = value; } }
        public override DateTime? DataWystawienia { get { return oswiadczenieDb.data_wydania; } set { oswiadczenieDb.data_wydania = value; } }
        public override DateTime? DataWaznosci { get { return oswiadczenieDb.data_waznosci; } set { oswiadczenieDb.data_waznosci = value; } }
        public override string Uwagi { get { return oswiadczenieDb.uwagi; } set { oswiadczenieDb.uwagi = value; } }
        public override string Path { get { return oswiadczenieDb.path; } set { oswiadczenieDb.path = value; } }
        public override DateTime LastUpdate { get { return oswiadczenieDb.last_update; } }
        public Oswiadczenia OswiadczenieDb { get { return oswiadczenieDb; } }
        #endregion

        #region constructors
        public Oswiadczenie()
        {
            oswiadczenieDb = new Oswiadczenia();
        }
        public Oswiadczenie(Oswiadczenia o)
        {
            oswiadczenieDb = o;
        }

        public Oswiadczenie(string numer, DateTime dataWystawienia, DateTime dataWaznosci, string uwagi)
        {
            oswiadczenieDb = new Oswiadczenia();
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
                    oswiadczenieDb.id_pracownika = idPracownika;
                    DbAdapterEF.OswiadczenieInsert(this);
                }
                else
                    DbAdapterEF.OswiadczenieUpdate(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        public override void Remove()
        {
            try
            {
                DbAdapterEF.OswiadczenieRemove(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion        

    }
}
