using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Objects.SqlClient;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Configuration;
using System.Data.EntityClient;
using System.Web.Script.Serialization;

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
        public JsonResult GetDropDownData(string typeofData, string filter, string buildingfilter, string bucketfilter)
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

                    var queryfacilities = db.DimFacilities.AsQueryable();

                    if(filter !=null )

                     queryfacilities = db.DimFacilities.Where(x => x.DimCountry.CountryName.Equals(filter));

                    var listfacilities = queryfacilities.ToList();

                    IEnumerable<DimFacility> facilities = listfacilities.Select(x => new DimFacility { SiteName = x.SiteName, id = x.id });

                     return Json(facilities, JsonRequestBehavior.AllowGet);

                case "BuildingList":

                    var querybuildings = db.DimBuildings.Where(x => x.DimFacility.SiteName.Equals(filter)).ToList();

                    IEnumerable<DimBuilding> buildings = querybuildings.Select(x => new DimBuilding { BuildingName = x.BuildingName, id = x.id });
                                                                      
                      return Json(buildings, JsonRequestBehavior.AllowGet);

                case "ModuleList":

                    var querymodules = db.DimModules.Where(x => x.DimBuilding.BuildingName.Equals(filter)).ToList();

                    IEnumerable<DimModule> modules = querymodules.Select(x => new DimModule { ModuleName = x.ModuleName, id = x.id });

                    return Json(modules, JsonRequestBehavior.AllowGet);

                case "FFInstanceList":

                    var queryffinstances = db.DimFFInstances.AsEnumerable();

                    IEnumerable<DimFFInstance> ffinstances;

                    if (buildingfilter == null)
                    {
                        //int? nullfilter = null;

                        queryffinstances = db.DimFFInstances.Where(x => x.DimModule.DimBuilding.DimFacility.SiteName.Equals(filter));

                        queryffinstances = queryffinstances.Where(x => !x.KeyModule.HasValue || x.KeyModule == null );


                        SqlConnection sc = (SqlConnection)db.Database.Connection; //get the SQLConnection that your entity object would use
                        string adoConnStr = sc.ConnectionString;

                        SqlConnection vcon = new SqlConnection(adoConnStr);

                        List<DimFFInstance> ffinstlist = new List<DimFFInstance>();

                        SqlDataAdapter da = new SqlDataAdapter("select id,ProjectName from DimFFInstance where KeyModule IS NULL and SiteName LIKE '" + filter + "'", vcon);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        foreach (DataRow row in dt.Rows)
                        {
                            DimFFInstance ffinst = new DimFFInstance();
                            ffinst.id = int.Parse(row["id"].ToString());
                            ffinst.ProjectName = row["ProjectName"].ToString();
                            ffinstlist.Add(ffinst);
                        }

                        return Json(ffinstlist, JsonRequestBehavior.AllowGet);

                        System.Diagnostics.Debug.Write(dt.Rows[0]["ProjectName"]);

                     //ffinstances = queryffinstances.Select(x => new DimFFInstance { ProjectName = x.ProjectName, id = x.id , KeyModule = x.KeyModule ?? null }).ToList();

                     //return Json(ffinstances, JsonRequestBehavior.AllowGet);

                    }
                    else

                        queryffinstances = db.DimFFInstances.Where(x => x.DimModule.DimBuilding.BuildingName.Equals(buildingfilter));

                    ffinstances = queryffinstances.Select(x => new DimFFInstance { ProjectName = x.ProjectName, id = x.id }).ToList();

                    System.Diagnostics.Debug.Write(Json(ffinstances).Data.ToString());

                    return Json(ffinstances, JsonRequestBehavior.AllowGet);

                case "StationTypesList":

                    var querystationtypes = db.DimStationTypes.Where(x => x.DimFFInstance.ProjectName.Equals(filter));

                    if (bucketfilter != null) querystationtypes = querystationtypes.Where(x => x.DimBucket.BucketName.Equals(bucketfilter)).OrderBy(x => x.Sequence);

                    else querystationtypes = querystationtypes.Where(x => x.KeyBucket == null);

                    var liststationtypes = querystationtypes.ToList();

                    IEnumerable<DimStationType> stationtypes = liststationtypes.Select(x => new DimStationType { StationTypeName = x.StationTypeName, id = x.id });

                    return Json(stationtypes, JsonRequestBehavior.AllowGet);

                case "BucketsList":

                    var querybuckets = db.DimBuckets.ToList();

                    IEnumerable<DimBucket> buckets = querybuckets.Select(x => new DimBucket { BucketName = x.BucketName, id = x.id });

                    return Json(buckets, JsonRequestBehavior.AllowGet);

                case "CustomersList":

                    var querycustomers = db.DimBusinessPartners.AsEnumerable();

                    if (buildingfilter != null)
                    {
                        querycustomers = db.DimBusinessPartners.Where(x => x.DimBuilding.BuildingName.Equals(buildingfilter)).ToList();
                    }
                    else {
                        querycustomers = db.DimBusinessPartners.Where(x => x.DimFacility.SiteName.Equals(filter)).ToList();

                        querycustomers = querycustomers.Where(x => x.KeyBuilding == null).ToList();
                    }

                    IEnumerable<DimBusinessPartner> customers = querycustomers.Select(x => new DimBusinessPartner { BPCode = x.BPCode, id = x.id });

                    return Json(customers, JsonRequestBehavior.AllowGet);

                case "MetricList":

                    var querymetrics = db.Metrics.AsQueryable();

                    IEnumerable<Metric> metrics = querymetrics.Select(x => new Metric { MetricName = x.MetricName , id = x.id });

                    return Json(new SelectList(querymetrics.ToList()), JsonRequestBehavior.AllowGet);

                                       
                default:
                    return Json(new SelectList(null));
            }
        }
    }
}