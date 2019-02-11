using Opportunity.Model.Obiekty;
using Opportunity.ViewModel.Obiekty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel
{
    public static class TowarManager
    {
        public static WydanieVM GetWydanie(Wydanie wydanie)
        {
            return new WydanieVM(wydanie);
        }        
    }
}
