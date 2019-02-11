using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Opportunity.Model.Obiekty;

namespace Opportunity.Model.Database
{
    public class PracownikAdapterEF
    {
        #region fields
        public Pracownicy PracownikDb { get; set; }
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        #endregion

        #region constructors
        public PracownikAdapterEF(Pracownicy p)
        {
            PracownikDb = p;
        }

        public PracownikAdapterEF()
        {
            PracownikDb = new Pracownicy();
        }

        internal void PracownikInsert()
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities())
                {
                    context.Pracownicy.Add(PracownikDb);
                    context.SaveChanges();
                }

            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        internal void PracownikUpdate()
        {
            try
            {
                using (OpportunityEntities context = new OpportunityEntities())
                {
                    context.SaveChanges();
                }

            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
