using Opportunity.Model.Obiekty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel.Obiekty
{
    public class SzkolenieVM : ADokumentVM
    {
        public SzkolenieVM()
        {
            Dokument = new SzkolenieBHP();
            Typ = Dokument.GetType().Name;
        }

        public SzkolenieVM(SzkolenieBHP szkolenie) : base(szkolenie)
        {

        }
    }
}
