using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using System.Text.RegularExpressions;

namespace AtomConfiguratorModel.Models
{
    public class SiteModel
    {
        public DimRegion SelectedRegion { get; set; }
        public DimCountry SelectedCountry { get; set; }
        public DimFacility SelectedFacility { get; set; }
        public DimBuilding SelectedBuilding { get; set; }
        public DimModule SelectedModule { get; set; }

        public virtual ICollection<DimRegion> ListRegions{ get; set; }
        public virtual ICollection<DimCountry> ListCountries { get; set; }
        public virtual ICollection<DimFacility> ListFacilities { get; set; }
        public virtual ICollection<DimBuilding> ListBuildings { get; set; }
        public virtual ICollection<DimModule> ListModules { get; set; }

          }

    
    public class MustBeSelectedAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            if (value == null || (int)value == 0)
                return false;
            else return true;
        }   



        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new ModelClientValidationRule[] 
        {
            new ModelClientValidationRule { ValidationType = "dropdown", ErrorMessage = 
            this.ErrorMessage } };

        }  
    

     }

     
      
    
    public class Region : DimRegion
    {
        [MustBeSelectedAttribute(ErrorMessage = "Please Select Region")]
        public string ID { get; set; }
        public string Name { get; set; }
    }


    public class Country : DimCountry
    {
        [MustBeSelectedAttribute(ErrorMessage = "Please Select Country")]
        public string ID { get; set; }
        public string Name { get; set; }
        public int? Region { get; set; }
    }
    public class Facility : DimFacility
    {
        [MustBeSelectedAttribute(ErrorMessage = "Please Select Facility")]
        public string ID { get; set; }
        public string Name { get; set; }
        public int? Country { get; set; }
    }
    

}