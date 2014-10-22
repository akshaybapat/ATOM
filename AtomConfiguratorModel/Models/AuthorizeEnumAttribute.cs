using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace AtomConfiguratorModel.Models
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]

  public class AuthorizeEnumAttribute : AuthorizeAttribute
  {
    public AuthorizeEnumAttribute(params object[] roles)
    {
      if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
        throw new ArgumentException("roles");
      foreach (var item in roles)
      {

        string strRoleName = GetEnumDescription(item as Enum);
        if (this.Roles == "")
        {
          this.Roles = strRoleName;
        }
        else
        {
          this.Roles = this.Roles + " , " + strRoleName;
        }

        this.Roles = string.Join(",", Roles);
      }
      
     // this.Roles = "ATOM USER";
    }
    public static string GetEnumDescription(Enum value)
    {
      FieldInfo fi = value.GetType().GetField(value.ToString());

      DescriptionAttribute[] attributes =
          (DescriptionAttribute[])fi.GetCustomAttributes(
          typeof(DescriptionAttribute),
          false);

      if (attributes != null &&
          attributes.Length > 0)
        return attributes[0].Description;
      else
        return value.ToString();
    }

  }
}