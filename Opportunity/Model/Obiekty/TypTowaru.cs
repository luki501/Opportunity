using log4net;
using Opportunity.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Model.Obiekty
{
    public class TypTowaru
    {
        #region fields
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public Slownik_typy_towarow typTowaruDb;
        public int Id { get { return typTowaruDb.id; } }
        public string Nazwa { get { return typTowaruDb.nazwa; } set { typTowaruDb.nazwa = value; } }
        #endregion

        #region constructors
        public TypTowaru(Slownik_typy_towarow typ)
        {
            typTowaruDb = typ;
        }

        public TypTowaru()
        {
            typTowaruDb = new Slownik_typy_towarow();
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
                DbAdapterEF.TypTowaruUpdate(typTowaruDb);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion

    }
}
