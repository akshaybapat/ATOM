using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AtomConfiguratorModel.Models
{
    public class DimModel
    {
        [Display(Name = "Business Partner")]
        public String BusinessPartner { get; set; }
        [Display(Name = "Business Unit")]
        public String BusinessUnit { get; set; }
        [Display(Name = "Country")]
        public String Country { get; set; }
        [Display(Name = "Finance Company")]
        public String FinancialCompany { get; set; }
        [Display(Name = "Region")]
        public String Region { get; set; }
        [Display(Name = "Site")]
        public String Site { get; set; }

    }
}