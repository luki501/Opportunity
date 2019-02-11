using Opportunity.Model.Obiekty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel.Obiekty
{
    public class ZezwolenieVM : ADokumentVM
    {
        public ZezwolenieVM()
        {
            Dokument = new Zezwolenie();
            Typ = Dokument.GetType().Name;
        }

        public ZezwolenieVM(Zezwolenie zezwolenie) : base(zezwolenie)
        {

        }
    }
}
