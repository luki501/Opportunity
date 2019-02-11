using Opportunity.Model.Obiekty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.ViewModel.Obiekty
{
    public class PotwierdzenieVM
    {
        #region fields
        private Potwierdzenie potwierdzenie;

        public int Id { get { return potwierdzenie.Id; } }
        public int IdPracownika { get { return potwierdzenie.IdPracownika; } set { potwierdzenie.IdPracownika = value; } }
        public int IdWydania { get { return potwierdzenie.IdWydania; } set { potwierdzenie.IdWydania = value; } }
        public DateTime DataPotwierdzenia { get { return potwierdzenie.DataPotwierdzenia; } set { potwierdzenie.DataPotwierdzenia = value; } }
        public string Uwagi { get { return potwierdzenie.Uwagi; } set { potwierdzenie.Uwagi = value; } }
        #endregion

        #region constructors
        public PotwierdzenieVM()
        {
            potwierdzenie = new Potwierdzenie();
        }
        public PotwierdzenieVM(Potwierdzenie potwierdzenie)
        {
            this.potwierdzenie = potwierdzenie;
        }
        #endregion
    }
}
