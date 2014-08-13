using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtomConfiguratorModel.Models
{
    public class FFSiteController : Controller
    {

        private FFCube2Entities db = new FFCube2Entities();
        // GET: FFSite
        public ActionResult Index()
        {
            ViewBag.KeyRegion = (from r in db.DimRegions
            select r.RegionName).Distinct();
            return View();
        }


        // GET: FFSite/GetDropDownData
        public JsonResult GetDropDownData(string typeofData, string filter)
        {
            switch (typeofData)
            {

                case "RegionList":

                    var queryregions = db.DimRegions.ToList();

                    IEnumerable<DimRegion> regions = queryregions.Select(x => new DimRegion { RegionName = x.RegionName, id = x.id });

                    return Json(new SelectList(regions.ToList()) , JsonRequestBehavior.AllowGet);

                case "CountryList":
                    
                    var querycountries = db.DimCountries.Where(x => x.DimRegion.RegionName.Equals(filter)).ToList();

                    IEnumerable<DimCountry> countries = querycountries.Select(x => new DimCountry { CountryName = x.CountryName, id = x.id });

                    return Json(countries, JsonRequestBehavior.AllowGet);

                case "FacilityList":

                     var queryfacilities = db.DimFacilities.Where(x => x.DimCountry.CountryName.Equals(filter)).ToList();

                     IEnumerable<DimFacility> facilities = queryfacilities.Select(x => new DimFacility { SiteName = x.SiteName, id = x.id });

                     return Json(facilities, JsonRequestBehavior.AllowGet);

                case "BuildingList":

                    var querybuildings = db.DimBuildings.Where(x => x.DimFacility.SiteName.Equals(filter)).ToList();

                    IEnumerable<DimBuilding> buildings = querybuildings.Select(x => new DimBuilding { BuildingName = x.BuildingName, id = x.id });
                                                                      
                      return Json(buildings, JsonRequestBehavior.AllowGet);

                case "ModuleList":

                    var querymodules = db.DimModules.Where(x => x.DimBuilding.BuildingName.Equals(filter)).ToList();

                    IEnumerable<DimModule> modules = querymodules.Select(x => new DimModule { ModuleName = x.ModuleName, id = x.id });

                    return Json(modules, JsonRequestBehavior.AllowGet);

                                       
                default:
                    return Json(new SelectList(null));
            }
        }
    }
}