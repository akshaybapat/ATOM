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
    
    public partial class FactffDefect
    {
        public int id { get; set; }
        public Nullable<int> KeyProductNumber { get; set; }
        public Nullable<int> KeyStartTime { get; set; }
        public Nullable<int> KeyStationType { get; set; }
        public Nullable<int> KeyStationName { get; set; }
        public string Revision { get; set; }
        public Nullable<int> IsUnit { get; set; }
        public string DefectCode { get; set; }
        public string SerialNo { get; set; }
        public string ProductionOrderNo { get; set; }
        public Nullable<int> FFInstance { get; set; }
        public string ComponentNo { get; set; }
        public Nullable<int> KeyDefectTime { get; set; }
        public Nullable<int> KeyTestDate { get; set; }
        public string DefectLocation { get; set; }
        public string ComponentDescription { get; set; }
        public string ProductionLine { get; set; }
    }
}
