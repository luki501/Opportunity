using log4net;
using Opportunity.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Model.Obiekty
{
    public class WydanieView
    {
        #region fields
        private v_wydania wydanieDb;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public int Id { get { return wydanieDb.id; } }
        public DateTime DataWydania { get { return wydanieDb.Data; } }
        public decimal Ilosc { get { return wydanieDb.Ilosc; } }
        public int IdTowaru { get { return wydanieDb.id_towaru; } }
        public string NazwaTowaru { get { return wydanieDb.nazwa_towaru; } }
        public int? IdWydajacego { get { return wydanieDb.id_wydajacego; } }
        public string Wydajacy { get { return wydanieDb.wydajacy; } }
        public int? IdPrzyjmujacego { get { return wydanieDb.id_przyjmujacego; } }
        public string Przyjmujacy { get { return wydanieDb.przyjmujacy; } }
        public DateTime? DataPotwierdzeniaWydania { get { return wydanieDb.data_potwierdzenia_wydania; } }
        public DateTime? DataPotwierdzeniaPrzyjecia { get { return wydanieDb.data_potwierdzenia_przyjecia; } }
        #endregion

        #region constructors
        public WydanieView(v_wydania w)
        {
            wydanieDb = w;
        }
        #endregion

        #region methods
        public bool IsDoPotwierdzeniaPrzyjecia(int idBrygadzisty)
        {
            try
            {
                return (IdPrzyjmujacego == null && idBrygadzisty == 0 || IdPrzyjmujacego == idBrygadzisty) && DataPotwierdzeniaPrzyjecia == null;
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        public bool OznaczJakoWykonane(bool wykonane)
        {
            try
            {
                DbAdapterEF.OznaczWykonanieWydania(Id, wykonane);
                return true;
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
