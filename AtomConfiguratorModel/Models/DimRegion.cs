//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AtomConfiguratorModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    
    public partial class DimRegion
    {
        public DimRegion()
        {
            this.DimCountries = new HashSet<DimCountry>();
        }
    
        public int id { get; set; }

         [Display(Name = "Region")]
        public string RegionName { get; set; }
    
        public virtual ICollection<DimCountry> DimCountries { get; set; }
    }
}