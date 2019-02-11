using log4net;
using Opportunity.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Model.Obiekty
{
    public class Wydanie
    {
        #region fields
        private Wydania wydanieDb;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public int Id { get { return wydanieDb.id; } }
        public int? IdPrzyjmujacego { get { return wydanieDb.id_przyjmujacego; } set { wydanieDb.id_przyjmujacego = value; } }
        public int? IdWydajacego { get { return wydanieDb.id_wydajacego; } set { wydanieDb.id_wydajacego = value; } }
        public int IdTowaru { get { return wydanieDb.id_towaru; } set { wydanieDb.id_towaru = value; } }
        public decimal Ilosc { get { return wydanieDb.ilosc; } set { wydanieDb.ilosc = value; } }
        public DateTime DataWydania { get { return wydanieDb.data; } set { wydanieDb.data = value; } }
        public string Uwagi { get { return wydanieDb.uwagi; } set { wydanieDb.uwagi = value; } }
        public Wydania WydanieDb { get { return wydanieDb; } }
        public Towar TowarWydawany { get { return DbAdapterEF.GetTowar(IdTowaru); } }
        #endregion

        #region constructors
        public Wydanie()
        {
            wydanieDb = new Wydania();
            DanePoczatkowe();
        }
        public Wydanie(Wydania wydanie)
        {
            wydanieDb = wydanie;
            DanePoczatkowe();
        }
        #endregion

        #region methods
        public void Save()
        {
            try
            {
                if (Id == 0)
                {
                    DbAdapterEF.WydanieInsert(this);
                }
                else
                    DbAdapterEF.WydanieUpdate(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        public void Save(int? idWydajacego, int? idPrzyjmujacego)
        {
            try
            {
                if (Id == 0)
                {
                    wydanieDb.id_wydajacego = idWydajacego;
                    wydanieDb.id_przyjmujacego = idPrzyjmujacego;
                    DbAdapterEF.WydanieInsert(this);
                }
                else
                    DbAdapterEF.WydanieUpdate(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        public void Remove()
        {
            try
            {
                DbAdapterEF.WydanieRemove(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        private void DanePoczatkowe()
        {
            wydanieDb.data = DateTime.Now;
            wydanieDb.ilosc = 1;
        }
        internal string Validate()
        {
            try
            {
                Towar towar = TowarWydawany;
                if (towar.Premium)
                {
                    if (towar.DataPrzyjecia.HasValue && ((DateTime)towar.DataPrzyjecia).Date > DataWydania.Date)
                        return "Data wydania jest wcześniejsza od daty przyjęcia";
                    if (towar.DataOstatniegoWydania.HasValue && ((DateTime)towar.DataOstatniegoWydania).Date > DataWydania.Date)
                        return "Wybrana data jest wcześniejsza od daty ostatniego wydania";
                    if ((IdPrzyjmujacego == null || IdPrzyjmujacego == 0) && (IdWydajacego == null || IdWydajacego == 0))
                    {
                        if (towar.StanMagazynowy == 1)
                            return "Towar jest już na magazynie";
                        else
                            return "Wybierz pracownika";
                    }
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
