//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace AtomConfiguratorModel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DimFFInstance
    {
        public DimFFInstance()
        {
            this.DimProductNumbers = new HashSet<DimProductNumber>();
            this.MetricConfigurations = new HashSet<MetricConfiguration>();
        }

        public int id { get; set; }
        [Display(Name = "Data Source")]
        public string DataSourceName { get; set; }
        [Display(Name = "Hostname")]
        public string HostName { get; set; }
        [Display(Name = "Database")]
        public string DatabaseName { get; set; }
        [Display(Name = "Username")]
        public string UserName { get; set; }
        public string Password { get; set; }
        [Display(Name = "Project")]
        public string ProjectName { get; set; }
        [Display(Name = "Data File Prefix")]
        public string DataFilePrefix { get; set; }
        [Display(Name = "Replication Delay (min)")]
        public Nullable<int> ReplicationDelayMinute { get; set; }
        public Nullable<bool> IsActive { get; set; }
        [Display(Name = "Module")]
        public Nullable<int> KeyModule { get; set; }
        [Display(Name = "IT Contact Name")]
        public string ITContactName { get; set; }
        [Display(Name = "IT Phone")]
        public string ITPhone { get; set; }
        [Display(Name = "IT Email")]
        public string ITEmail { get; set; }
        [Display(Name = "QA Contact Name")]
        public string QAContactName { get; set; }
        [Display(Name = "QA Phone")]
        public string QAPhone { get; set; }
        [Display(Name = "QA Email")]
        public string QAEmail { get; set; }
        [Display(Name = "Site")]
        public string SiteName { get; set; }
        [Display(Name = "Baan Comp No.")]
        public string BaanCoNo { get; set; }	
    
        public virtual ICollection<DimProductNumber> DimProductNumbers { get; set; }
        public virtual DimModule DimModule { get; set; }
        public virtual ICollection<MetricConfiguration> MetricConfigurations { get; set; }
    }
}
