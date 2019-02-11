using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Model
{
    public class Uzytkownik
    {
        #region fields
        public string User { get; set; }

        #endregion

        public override string ToString()
        {
            return User;
        }
    }
}
