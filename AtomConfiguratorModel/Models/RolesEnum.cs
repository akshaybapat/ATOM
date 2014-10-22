using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AtomConfiguratorModel.Models
{
  public class RolesEnum
  {
    public enum Roles
    {
      [Description("ATOM USER")]
      AtomUser,
      [Description("SALES USER")]
      SalesUser
    }

  }
}