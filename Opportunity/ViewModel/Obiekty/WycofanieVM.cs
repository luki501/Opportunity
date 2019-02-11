using log4net;
using Opportunity.Model.Obiekty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel.Obiekty
{
    public class WycofanieVM
    {
        #region fields
        private Wycofanie wycofanie;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public int Id { get { return wycofanie.Id; } }
        public int IdTowaru { get { return wycofanie.IdTowaru; } set { wycofanie.IdTowaru = value; } }
        public decimal Ilosc { get { return wycofanie.Ilosc; } set { wycofanie.Ilosc = value; } }
        public DateTime DataWycofania { get { return wycofanie.DataWycofania; } set { wycofanie.DataWycofania = value; } }
        public string Path { get { return wycofanie.Path; } set { wycofanie.Path = value; } }
        public string Uwagi { get { return wycofanie.Uwagi; } set { wycofanie.Uwagi = value; } }
        #endregion

        #region constructors
        public WycofanieVM()
        {
            wycofanie = new Wycofanie();
        }
        public WycofanieVM(int idTowaru)
        {
            wycofanie = new Wycofanie();
            IdTowaru = idTowaru;
        }
        public WycofanieVM(Wycofanie wycofanie)
        {
            this.wycofanie = wycofanie;
        }

        internal void Save()
        {
            try
            {
                wycofanie.Save();
            }
            catch(Exception ex) { log.Error(ex); throw ex; }
        }

        internal string Validate()
        {
            try
            {
                Towar towar = wycofanie.TowarWycofywany;
                if (towar.Premium)
                {
                    if (towar.DataPrzyjecia.HasValue && ((DateTime)towar.DataPrzyjecia).Date > DataWycofania.Date)
                        return "Data wydania jest wcześniejsza od daty przyjęcia";
                    if (towar.DataOstatniegoWydania.HasValue && ((DateTime)towar.DataOstatniegoWydania).Date > DataWycofania.Date)
                        return "Wybrana data jest wcześniejsza od daty ostatniego wydania";                    
                    return "";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
