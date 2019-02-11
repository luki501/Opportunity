using log4net;
using Opportunity.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Model.Obiekty
{
    public class Potwierdzenie
    {
        #region fields
        private Potwierdzenia potwierdzenieDb;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public int Id { get { return potwierdzenieDb.id; } }
        public int IdPracownika { get { return potwierdzenieDb.id_pracownika; } set { potwierdzenieDb.id_pracownika = value; } }
        public int IdWydania { get { return potwierdzenieDb.id_wydania; } set { potwierdzenieDb.id_wydania = value; } }
        public DateTime DataPotwierdzenia { get { return potwierdzenieDb.data; } set { potwierdzenieDb.data = value; } }
        public string Uwagi { get { return potwierdzenieDb.uwagi; } set { potwierdzenieDb.uwagi = value; } }
        public Potwierdzenia PotwierdzenieDb { get { return potwierdzenieDb; } }
        #endregion

        #region constructors
        public Potwierdzenie()
        {
            potwierdzenieDb = new Potwierdzenia();
        }
        public Potwierdzenie(Potwierdzenia potwierdzenie)
        {
            potwierdzenieDb = potwierdzenie;
        }
        #endregion

        #region methods
        public void Save()
        {
            try
            {
                if (Id == 0)
                {
                    DbAdapterEF.PotwierdzenieInsert(this);
                }
                else
                    DbAdapterEF.PotwierdzenieUpdate(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        public void Remove()
        {
            try
            {
                DbAdapterEF.PotwierdzenieRemove(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
