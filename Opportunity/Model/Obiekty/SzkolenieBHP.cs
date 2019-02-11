using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Opportunity.Model.Database;

namespace Opportunity.Model.Obiekty
{
    public class SzkolenieBHP : ADokument
    {
        #region fields
        private SzkoleniaBHP szkolenieDb;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public override int Id { get { return szkolenieDb.id; } }
        public override string Numer { get { return szkolenieDb.numer; } set { szkolenieDb.numer = value; } }
        public override DateTime? DataWystawienia { get { return szkolenieDb.data_wystawienia; } set { szkolenieDb.data_wystawienia = value; } }
        public override DateTime? DataWaznosci { get { return szkolenieDb.data_waznosci; } set { szkolenieDb.data_waznosci = value; } }
        public override string Uwagi { get { return szkolenieDb.uwagi; } set { szkolenieDb.uwagi = value; } }
        public override string Path { get { return szkolenieDb.path; } set { szkolenieDb.path = value; } }
        public override DateTime LastUpdate { get { return szkolenieDb.last_update; } }
        public SzkoleniaBHP SzkolenieDb { get { return szkolenieDb; } }
        #endregion

        #region constructors
        public SzkolenieBHP()
        {
            szkolenieDb = new SzkoleniaBHP();
        }
        public SzkolenieBHP(SzkoleniaBHP szkoleniaBHP)
        {
            szkolenieDb = szkoleniaBHP;
        }

        public SzkolenieBHP(string numer, DateTime dataWystawienia, DateTime dataWaznosci, string uwagi)
        {
            szkolenieDb = new SzkoleniaBHP();
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
                    szkolenieDb.id_pracownika = idPracownika;
                    DbAdapterEF.SzkolenieBHPInsert(this);
                }
                else
                    DbAdapterEF.SzkolenieBHPUpdate(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        public override void Remove()
        {
            try
            {
                DbAdapterEF.SzkolenieBHPRemove(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion

    }
}
