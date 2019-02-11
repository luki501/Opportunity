using log4net;
using Opportunity.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Model.Obiekty
{
    public class Paszport : ADokument
    {
        #region fields
        private Paszporty paszportDb;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");

        // TODO bezterminowy
        public override int Id { get { return paszportDb.id; } }
        public override string Numer { get { return paszportDb.numer; } set { paszportDb.numer = value; } }
        public override DateTime? DataWystawienia { get { return paszportDb.data_wydania; } set { paszportDb.data_wydania = value; } }
        public override DateTime? DataWaznosci { get { return paszportDb.data_waznosci; } set { paszportDb.data_waznosci = value; } }        
        public override string Uwagi { get { return paszportDb.uwagi; } set { paszportDb.uwagi = value; } }
        public override string Path { get { return paszportDb.path; } set { paszportDb.path = value; } }
        public bool Biometryczny { get { return paszportDb.biometryczny; } set { paszportDb.biometryczny = value; } }
        public override DateTime LastUpdate { get { return paszportDb.last_update; } }
        public Paszporty PaszportDb { get { return paszportDb; } }
        #endregion

        #region constructors
        public Paszport()
        {
            paszportDb = new Paszporty();
        }
        public Paszport(Paszporty p)
        {
            paszportDb = p;
        }

        public Paszport(string numer, DateTime dataWystawienia, DateTime dataWaznosci, string uwagi)
        {
            paszportDb = new Paszporty();
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
                    paszportDb.id_pracownika = idPracownika;
                    DbAdapterEF.PaszportInsert(this);
                }
                else
                    DbAdapterEF.PaszportUpdate(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        public override void Remove()
        {
            try
            {
                DbAdapterEF.PaszportRemove(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
