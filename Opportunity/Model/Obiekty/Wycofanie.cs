using log4net;
using Opportunity.Model.Database;
using System;

namespace Opportunity.Model.Obiekty
{
    public class Wycofanie
    {
        #region fields
        private Wycofania wycofanieDb;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public int Id { get { return wycofanieDb.id; } }
        public int IdTowaru { get { return wycofanieDb.id_towaru; } set { wycofanieDb.id_towaru = value; } }
        public decimal Ilosc { get { return wycofanieDb.ilosc; } set { wycofanieDb.ilosc = value; } }
        public DateTime DataWycofania { get { return wycofanieDb.data; } set { wycofanieDb.data = value; } }        
        public string Path { get { return wycofanieDb.path; } set { wycofanieDb.path = value; } }
        public string Uwagi { get { return wycofanieDb.uwagi; } set { wycofanieDb.uwagi = value; } }
        public Wycofania WycofanieDb { get { return wycofanieDb; } }
        public Towar TowarWycofywany { get { return DbAdapterEF.GetTowar(IdTowaru); } }
        #endregion

        #region constructors
        public Wycofanie()
        {
            wycofanieDb = new Wycofania();
            DanePoczatkowe();
        }
        public Wycofanie(Wycofania wycofanie)
        {
            wycofanieDb = wycofanie;
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
                    DbAdapterEF.WycofanieInsert(this);
                }
                else
                    DbAdapterEF.WycofanieUpdate(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        public void Remove()
        {
            try
            {
                DbAdapterEF.WycofanieRemove(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        private void DanePoczatkowe()
        {
            wycofanieDb.data = DateTime.Now;
            wycofanieDb.ilosc = 1;
        }

        internal string Validate()
        {
            try
            {
                Towar towar = TowarWycofywany;
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
