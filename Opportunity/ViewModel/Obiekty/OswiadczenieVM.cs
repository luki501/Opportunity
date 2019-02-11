using Opportunity.Model.Obiekty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel.Obiekty
{
    public class OswiadczenieVM : ADokumentVM
    {
        public OswiadczenieVM()
        {
            Dokument = new Oswiadczenie();            
            Typ = Dokument.GetType().Name;
        }

        public OswiadczenieVM(Oswiadczenie oswiadczenie) : base(oswiadczenie)
        {

        }
    }
}
