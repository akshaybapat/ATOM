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
    
    public partial class DimProductNumber
    {
        public int id { get; set; }
        public string ProductNumber { get; set; }
        public string ProductRevision { get; set; }
        public Nullable<int> KeyProductLine { get; set; }
        public Nullable<int> KeyFFInstance { get; set; }
        public Nullable<int> KeySite { get; set; }
        public Nullable<int> KeyBP { get; set; }
        public Nullable<int> KeyPartFamily { get; set; }
    
        public virtual DimFFInstance DimFFInstance { get; set; }
        public virtual DimProductLine DimProductLine { get; set; }
    }
}
