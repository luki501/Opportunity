//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Opportunity.Model.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Slownik_marki
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Slownik_marki()
        {
            this.Towary = new HashSet<Towary>();
        }
    
        public int id { get; set; }
        public string nazwa { get; set; }
        public string uwagi { get; set; }
        public string uzytkownik { get; set; }
        public System.DateTime last_update { get; set; }
        public bool nieaktywna { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Towary> Towary { get; set; }
    }
}
