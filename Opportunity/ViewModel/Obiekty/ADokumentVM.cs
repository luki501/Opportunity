using GalaSoft.MvvmLight;
using Opportunity.Model.Obiekty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel.Obiekty
{
    public abstract class ADokumentVM : ViewModelBase
    {
        #region fields
        public ADokument Dokument { get; set; }
        public int Id => Dokument.Id;
        public string Numer { get { return Dokument.Numer; } set { Dokument.Numer = value; } }
        public DateTime? DataWystawienia { get { return Dokument.DataWystawienia; } set { Dokument.DataWystawienia = value; } }
        public DateTime? DataWaznosci { get { return Dokument.DataWaznosci; } set { Dokument.DataWaznosci = value; } }
        public string Uwagi { get { return Dokument.Uwagi; } set { Dokument.Uwagi = value; } }
        public string Path { get { return Dokument.Path; } set { Dokument.Path = value; } }
        public string Typ { get; set; }
        public DateTime LastUpdate { get { return Dokument.LastUpdate; } }
        #endregion

        #region constructors
        public ADokumentVM()
        {
            
        }
        public ADokumentVM(ADokument dokument)
        {
            Dokument = dokument;
            Typ = dokument.GetType().Name;
        }


        #endregion

        #region methods
        /*public void Remove()
        {
            try
            {
                Dokument.Remove();
            }
            catch (Exception ex) { throw ex; }
        }

        public void Save(int idPracownika)
        {
            Dokument.Save(idPracownika);
        }*/
        #endregion
    }
}
