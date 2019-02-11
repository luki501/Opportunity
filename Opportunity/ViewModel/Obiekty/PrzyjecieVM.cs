using log4net;
using Opportunity.Model.Obiekty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel.Obiekty
{
    public class PrzyjecieVM
    {
        #region fields
        private Przyjecie przyjecie;
        protected static ILog log = LogManager.GetLogger("LogFileAppender");
        public int Id { get { return przyjecie.Id; } }
        public int IdTowaru { get { return przyjecie.IdTowaru; } set { przyjecie.IdTowaru = value; } }
        public decimal Ilosc { get { return przyjecie.Ilosc; } set { przyjecie.Ilosc = value; } }
        public DateTime DataPrzyjecia { get { return przyjecie.DataPrzyjecia; } set { przyjecie.DataPrzyjecia = value; } }
        public string NumerFaktury { get { return przyjecie.NumerFaktury; } set { przyjecie.NumerFaktury = value; } }
        public string Sciezka { get { return przyjecie.Path; } set { przyjecie.Path = value; } }
        public string Uwagi { get { return przyjecie.Uwagi; } set { przyjecie.Uwagi = value; } }
        #endregion

        #region constructors
        public PrzyjecieVM()
        {
            przyjecie = new Przyjecie();
        }
        public PrzyjecieVM(int idTowaru)
        {
            przyjecie = new Przyjecie();
            IdTowaru = idTowaru;
        }
        public PrzyjecieVM(Przyjecie przyjecie)
        {
            this.przyjecie = przyjecie;
        }

        internal void Save()
        {
            try
            {
                przyjecie.Save();
            }
            catch(Exception ex) { log.Error(ex); throw ex; }
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
