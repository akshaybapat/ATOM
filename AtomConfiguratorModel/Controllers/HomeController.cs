using AtomConfiguratorModel.Models;
using Flextronics.QMSCC.Commons.SystemIntegrations.FlexWare.AuthenticationServices;
using Flextronics.QMSCC.Commons.SystemIntegrations.FlexWare.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AtomConfiguratorModel.Controllers
{
    public class HomeController : Controller
    {
      bool isValidUser = false;
      [AuthorizeEnum(RolesEnum.Roles.AtomUser, RolesEnum.Roles.SalesUser)]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult RoleWizard()
        {
            return View();
        }

        public ActionResult DimNavigation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult LoginRedirect()
        {
          return RedirectToAction("Login");
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
          return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
          isValidUser = ValidateUser(model);
          if (isValidUser)
          {
            return RedirectToAction("Index");
          }
          else
          {
            ModelState.AddModelError("Error", "Please enter valid user name or password");
            return View();
          }

        }
        public ActionResult Logout()
        {
          if (Request.IsAuthenticated)
          {
            FormsAuthentication.SignOut();
            ClearSessions();
          }

          return RedirectToAction("Login");
        }

        private void ClearSessions()
        {
          if (Session["userRoles"] != null)
          {
            Session["userRoles"] = null;
          }
        }

        private bool ValidateUser(LoginModel model)
        {
          try
          {
            bool isValidUser = false;
            string Roleserrors = string.Empty;
            string MasterDataErrors = string.Empty;
            string url = ConfigurationManager.AppSettings["FlexwareUrlQA"];
            string strSolutionCode = Convert.ToString(ConfigurationManager.AppSettings["SolutionCode"]);
            Authentication authentication = new Authentication(url);

            string tokenAuthentication = authentication.AuthenticateUser(model.UserName, model.Password, strSolutionCode);
            userrolesws userRoles = FlexWareGetUserRoles(authentication);

            if (userRoles != null)
            {
              isValidUser = true;
              FormsAuthentication.SetAuthCookie(model.UserName, false);
              Session["userRoles"] = userRoles;
            }
            else
            {
              isValidUser = false;
            }

            masterdataelementws[] masterData = GetMasterData(authentication, out MasterDataErrors);

            if (masterData != null)
            {
              foreach (var item in masterData)
              {

              }
            }
            return isValidUser;
          }
          catch (Exception ex)
          {

            return isValidUser = false;
          }
        }

        private masterdataelementws[] GetMasterData(Authentication authentication, out string errors)
        {
          try
          {
            errors = string.Empty;
            return authentication.GetMasterData();
          }
          catch (Exception ex)
          {
            errors = "Error on MasterData: " + ReadException(ex);
          }

          return null;
        }

        private string ReadException(Exception ex)
        {
          if (ex.InnerException != null)
          {
            return ReadException(ex.InnerException);
          }
          else
          {
            return ex.Message;
          }
        }

        private userrolesws FlexWareGetUserRoles(Authentication authentication)
        {
          try
          {
            // errors = string.Empty;
            return authentication.GetUserRoles();
          }
          catch (Exception ex)
          {
            // errors = "Error on GetRoles: " + ReadException(ex);
          }
          return null;
        }
    }
}
