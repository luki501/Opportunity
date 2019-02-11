using Opportunity.Model.Obiekty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel.Obiekty
{
    public class BadanieVM : ADokumentVM
    {
        #region fields

        #endregion

        #region construtors
        public BadanieVM()
        {
            Dokument = new Badanie();
            Typ = Dokument.GetType().Name;
        }
        public BadanieVM(Badanie badanie) : base(badanie)
        {
            
        }
        #endregion


    }
}
