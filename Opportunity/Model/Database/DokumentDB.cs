using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Model.Database
{
    interface DokumentDB
    {
        #region fields

        #endregion

        #region
        void Save(int idPracownika);
        void Remove();
        #endregion
    }
}
