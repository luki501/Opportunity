using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Model.Obiekty
{
    public abstract class ADokument
    {
        #region fields
        
        public abstract int Id { get; }
        public abstract string Numer { get; set; }
        public abstract DateTime? DataWystawienia { get; set; }
        public abstract DateTime? DataWaznosci { get; set; }
        public abstract string Uwagi { get; set; }
        public abstract string Path { get; set; }
        public abstract DateTime LastUpdate { get;}
        #endregion

        #region methods
        public abstract void Save(int idPracownika);
        public abstract void Remove();
        #endregion
    }
}
