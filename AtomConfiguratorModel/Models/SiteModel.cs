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
       // [Required(ErrorMessage = "Please Enter Region")]
        public Region Region { get; set; }
        //[Required(ErrorMessage = "Please Enter Country")]
        public Country Country { get; set; }
        //[Required(ErrorMessage = "Please Enter Site")]
        public Facility Facility { get; set; }
        public DimBuilding Building { get; set; }
        public DimModule Module { get; set; }

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

     
      
    
    public class Region
    {
        [MustBeSelectedAttribute(ErrorMessage = "Please Select Region")]
        public string ID { get; set; }
        public string Name { get; set; }
    }


    public class Country
    {
        [MustBeSelectedAttribute(ErrorMessage = "Please Select Country")]
        public string ID { get; set; }
        public string Name { get; set; }
        public int? Region { get; set; }
    }
    public class Facility
    {
        [MustBeSelectedAttribute(ErrorMessage = "Please Select Facility")]
        public string ID { get; set; }
        public string Name { get; set; }
        public int? Country { get; set; }
    }
    

}