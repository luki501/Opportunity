using Opportunity.Model.Obiekty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel.Obiekty
{
    public class ZameldowanieVM : ADokumentVM
    {
        public ZameldowanieVM()
        {
            Dokument = new Zameldowanie();
            Typ = Dokument.GetType().Name;
        }

        public ZameldowanieVM(Zameldowanie zaproszenie) : base(zaproszenie)
        {

        }
    }
}
