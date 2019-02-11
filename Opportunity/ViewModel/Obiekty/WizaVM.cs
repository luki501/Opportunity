using Opportunity.Model.Obiekty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel.Obiekty
{
    public class WizaVM : ADokumentVM
    {
        public WizaVM()
        {
            Dokument = new Wiza();
            Typ = Dokument.GetType().Name;
        }

        public WizaVM(Wiza wiza) : base(wiza)
        {

        }
    }
}
