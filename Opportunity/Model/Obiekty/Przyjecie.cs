using log4net;
using Opportunity.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Model.Obiekty
{
    public class Przyjecie
    {
        #region fields
        private Przyjecia przyjecieDb;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public int Id { get { return przyjecieDb.id; } }
        public int IdTowaru { get { return przyjecieDb.id_towaru; } set { przyjecieDb.id_towaru = value; } }
        public decimal Ilosc { get { return przyjecieDb.ilosc; } set { przyjecieDb.ilosc = value; } }
        public DateTime DataPrzyjecia { get { return przyjecieDb.data; } set { przyjecieDb.data = value; } }
        public string NumerFaktury { get { return przyjecieDb.numer_faktury; } set { przyjecieDb.numer_faktury = value; } }
        public string Path { get { return przyjecieDb.path; } set { przyjecieDb.path = value; } }
        public string Uwagi { get { return przyjecieDb.uwagi; } set { przyjecieDb.uwagi = value; } }
        public Przyjecia PrzyjecieDb { get { return przyjecieDb; } }
        #endregion

        #region constructors
        public Przyjecie()
        {
            przyjecieDb = new Przyjecia();
            DanePoczatkowe();
        }
        public Przyjecie(Przyjecia przyjecie)
        {
            przyjecieDb = przyjecie;                    
        }
        
        #endregion

        #region methods
        public void Save()
        {
            try
            {
                if (Id == 0)
                {                    
                    DbAdapterEF.PrzyjecieInsert(this);
                }
                else
                    DbAdapterEF.PrzyjecieUpdate(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        public void Remove()
        {
            try
            {
                DbAdapterEF.PrzyjecieRemove(this);
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }

        private void DanePoczatkowe()
        {
            przyjecieDb.data = DateTime.Now;
            przyjecieDb.ilosc = 1;
        }
        internal string Validate()
        {
            try
            {
                return "";
            }
            catch (Exception ex) { log.Error(ex); throw ex; }
        }
        #endregion
    }
}
