using log4net;
using Opportunity.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Model.Obiekty
{
    public class MarkaTowaru
    {
        #region fields
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public Slownik_marki markaDb;
        public int Id { get { return markaDb.id; } }
        public string Nazwa { get { return markaDb.nazwa; } set { markaDb.nazwa = value; } }
        #endregion

        #region constructors
        public MarkaTowaru(Slownik_marki marka)
        {
            markaDb = marka;
        }

        public MarkaTowaru()
        {
            markaDb = new Slownik_marki();
        }
        #endregion

        #region methods
        public override string ToString()
        {
            return Nazwa;
        }
        public void ZapiszZmiany()
        {
            try
            {
                DbAdapterEF.MarkaTowaruUpdate(markaDb);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
