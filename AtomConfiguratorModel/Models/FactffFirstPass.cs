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
    
    public partial class FactffFirstPass
    {
        public int id { get; set; }
        public Nullable<int> KeyProductNumber { get; set; }
        public Nullable<int> KeyStartTime { get; set; }
        public Nullable<int> KeyEndTime { get; set; }
        public Nullable<int> KeyStationType { get; set; }
        public Nullable<int> KeyStationName { get; set; }
        public Nullable<int> TotalTestCount { get; set; }
        public Nullable<int> FirstPassCount { get; set; }
        public Nullable<decimal> ProductFPY { get; set; }
        public Nullable<decimal> ProductRTY { get; set; }
        public string ProductionLine { get; set; }
        public string Revision { get; set; }
        public Nullable<decimal> ProdAvgFPY { get; set; }
        public Nullable<decimal> StationTypeFPY { get; set; }
        public Nullable<int> FFInstanceID { get; set; }
        public Nullable<int> PartIsUnit { get; set; }
    
        public virtual DimPhysicalStation DimPhysicalStation { get; set; }
    }
}
