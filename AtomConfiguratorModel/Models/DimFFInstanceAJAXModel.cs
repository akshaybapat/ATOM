using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtomConfiguratorModel.Models
{
    public class DimFFInstanceAJAXModel
    {
        public string buildingname { get; set; }
        public List<String> availablecustomers { get; set; }
        public List<String> assignedcustomers { get; set; }
    }
}