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
    
    public partial class DataPollingExecutionTime
    {
        public int id { get; set; }
        public Nullable<System.DateTime> Starttime { get; set; }
        public string Program { get; set; }
        public Nullable<int> NoOfRecords { get; set; }
        public string DatabaseName { get; set; }
        public Nullable<System.DateTime> Endtime { get; set; }
    }
}
