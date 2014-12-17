using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtomConfiguratorModel.Models
{
    public class DataAccessRequestMaster
    {
        public int id { get; set; }
        public string username { get; set; }
        public DateTime requestDate { get; set; }
        public string typeOfData { get; set; }
        public string KeyFacility { get; set; }
        public string KeyBusinessPartner { get; set; }
        public string KeyRegion { get; set; }
        public bool DataOwnerApproved { get; set; }
        public DateTime DataOwnerApproval { get; set; }
        public bool DataManagerApproved { get; set; }
        public DateTime DataManagerApproval { get; set; }
        public bool RequestStatus { get { return DataOwnerApproved && DataOwnerApproved; } }

    }
}