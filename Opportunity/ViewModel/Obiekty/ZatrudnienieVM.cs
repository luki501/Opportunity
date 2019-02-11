using Opportunity.Model.Obiekty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel.Obiekty
{
    public class ZatrudnienieVM : ADokumentVM
    {
        public ZatrudnienieVM()
        {
            Dokument = new Zatrudnienie();
            Typ = Dokument.GetType().Name;
        }

        public ZatrudnienieVM(Zatrudnienie zatrudnienie) : base(zatrudnienie)
        {

        }
    }
}
