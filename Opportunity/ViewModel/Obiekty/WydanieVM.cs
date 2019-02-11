using log4net;
using Opportunity.Model.Database;
using Opportunity.Model.Obiekty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel.Obiekty
{
    public class WydanieVM
    {
        #region fields
        private Wydanie wydanie;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public int Id { get { return wydanie.Id; } }
        public int? IdPrzyjmujacego { get { return wydanie.IdPrzyjmujacego; } set { wydanie.IdPrzyjmujacego = value; } }
        public int? IdWydajacego { get { return wydanie.IdWydajacego; } set { wydanie.IdWydajacego = value; } }
        public int IdTowaru { get { return wydanie.IdTowaru; } set { wydanie.IdTowaru = value; } }
        public decimal Ilosc { get { return wydanie.Ilosc; } set { wydanie.Ilosc = value; } }
        public DateTime DataWydania { get { return wydanie.DataWydania; } set { wydanie.DataWydania = value; } }
        public string Uwagi { get { return wydanie.Uwagi; } set { wydanie.Uwagi = value; } }          
        #endregion

        #region constructors
        public WydanieVM()
        {
            wydanie = new Wydanie();
        }
        public WydanieVM(int idTowaru)
        {
            wydanie = new Wydanie();
            IdTowaru = idTowaru;
        }
        public WydanieVM(Wydanie wydanie)
        {
            this.wydanie = wydanie;
        }

        internal void Save()
        {
            try
            {
                wydanie.Save(IdWydajacego, IdPrzyjmujacego);
            }
            catch(Exception ex) { log.Error(ex); throw ex; }
        }

        internal string Validate()
        {
            try
            {
                Towar towar =  wydanie.TowarWydawany;
                if (towar.Premium)
                {
                    if (towar.DataPrzyjecia.HasValue && ((DateTime)towar.DataPrzyjecia).Date > DataWydania.Date)
                        return "Data wydania jest wcześniejsza od daty przyjęcia";
                    if (towar.DataOstatniegoWydania.HasValue && ((DateTime)towar.DataOstatniegoWydania).Date > DataWydania.Date)
                        return "Wybrana data jest wcześniejsza od daty ostatniego wydania";
                    if ((IdPrzyjmujacego == null || IdPrzyjmujacego == 0) && (IdWydajacego == null || IdWydajacego == 0))
                        return "Wybierz pracownika";
                    if (IdPrzyjmujacego == IdWydajacego)
                        return "Wybierz innego pracownika";
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
