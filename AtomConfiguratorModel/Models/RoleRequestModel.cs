using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtomConfiguratorModel.Models
{
    public class RoleRequestModel
    {

        public int id;
        public String Username;
        public String RoleName;
        public ICollection<String> MasterData;


    }
}