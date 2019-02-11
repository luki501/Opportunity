using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Model.Database
{
    public partial class OpportunityEntities : DbContext
    {
        public OpportunityEntities(string connectionString) : base(connectionString)
        {

        }                
    
    }
}
