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
    
    public partial class DimCostCenter
    {
        public DimCostCenter()
        {
            this.DimBusinessUnits = new HashSet<DimBusinessUnit>();
        }
    
        public int id { get; set; }
        public string CostCenter { get; set; }
    
        public virtual ICollection<DimBusinessUnit> DimBusinessUnits { get; set; }
    }
}
