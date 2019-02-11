using Opportunity.Model.Obiekty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel.Obiekty
{
    public class PaszportVM : ADokumentVM
    {
        public PaszportVM()
        {
            Dokument = new Paszport();
            Typ = Dokument.GetType().Name;
        }

        public PaszportVM(Paszport paszport) : base (paszport)
        {

        }
    }
}
