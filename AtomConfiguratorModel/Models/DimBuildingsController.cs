using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AtomConfiguratorModel.Models
{
    public class DimBuildingsController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: DimBuildings
        public ActionResult Index(string sortOrder,string searchString)
        {
            DimFacilityController facContr = new DimFacilityController();
            //facContr.Index()
            var dimBuildings = db.DimBuildings.Include(d => d.DimFacility);

            
            if (!String.IsNullOrEmpty(searchString))

            {
                dimBuildings = dimBuildings.Where(s => s.BuildingName.Contains(searchString));
               // return View(dimBuildings);
            }

            switch (sortOrder)
            {
                case "Building_Desc":
                    dimBuildings = dimBuildings.OrderByDescending(s => s.BuildingName);
                    break;
                case "Facility":
                    //dimBuildings = dimBuildings.OrderBy(s => s.KeyFacility);

                  var query = from b in dimBuildings
                              join f in db.DimFacilities on b.KeyFacility equals f.id 
                              orderby f.SiteName
                              select b ;
                                                 
                               
                        dimBuildings = query.Select(x =>
                        new DimBuilding()
                        {
                            BuildingName = x.BuildingName,
                            KeyFacility = x.KeyFacility,

                        });
                                      
                    /*
                    IQueryable<DimFacility> facilities = from building in dimBuildings
                                 join facility in db.DimFacilities on building.KeyFacility equals facility.id
                                 orderby building.KeyFacility
                                 select facility;

                    //dimBuildings = dimBuildings.Where(s => s.BuildingName.Contains(sortOrder));
                    foreach(var f in facilities)
                    {
                        dimBuildings = dimBuildings.Where(s => s.KeyFacility.Equals(f.id)).AsQueryable();
                    }                           
                   */

                    break;

                   case "Facility_Desc":
                    dimBuildings = dimBuildings.OrderByDescending(s => s.KeyFacility);
                    ; break;
                default:
                    dimBuildings = dimBuildings.OrderBy(s => s.BuildingName);
                    ; break;
            }

            return View(dimBuildings.ToList());
        }

        // GET: DimBuildings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimBuilding dimBuilding = db.DimBuildings.Find(id);
            if (dimBuilding == null)
            {
                return HttpNotFound();
            }
            return View(dimBuilding);
        }

        // GET: DimBuildings/Create
        public ActionResult Create()
        {
            ViewBag.KeyFacility = new SelectList(db.DimFacilities, "id", "SiteName");
            return View();
        }

        // POST: DimBuildings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,BuildingName,KeyFacility")] DimBuilding dimBuilding)
        {
            if (ModelState.IsValid)
            {
                db.DimBuildings.Add(dimBuilding);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KeyFacility = new SelectList(db.DimFacilities, "id", "SiteName", dimBuilding.KeyFacility);
            return View(dimBuilding);
        }

        // GET: DimBuildings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimBuilding dimBuilding = db.DimBuildings.Find(id);
            if (dimBuilding == null)
            {
                return HttpNotFound();
            }
            ViewBag.KeyFacility = new SelectList(db.DimFacilities, "id", "SiteName", dimBuilding.KeyFacility);
            return View(dimBuilding);
        }

        // POST: DimBuildings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,BuildingName,KeyFacility")] DimBuilding dimBuilding)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimBuilding).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KeyFacility = new SelectList(db.DimFacilities, "id", "SiteName", dimBuilding.KeyFacility);
            return View(dimBuilding);
        }

        // GET: DimBuildings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimBuilding dimBuilding = db.DimBuildings.Find(id);
            if (dimBuilding == null)
            {
                return HttpNotFound();
            }
            return View(dimBuilding);
        }

        // POST: DimBuildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimBuilding dimBuilding = db.DimBuildings.Find(id);
            db.DimBuildings.Remove(dimBuilding);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: DimBuildings/BuildingList
        public ActionResult BuildingList(String id)
        {

            IEnumerable<DimBuilding> buildings = new List<DimBuilding>();

            String SiteName = id;

            var SiteID = from r in db.DimFacilities where r.SiteName.Equals(SiteName) select r.id;

            var query = db.DimBuildings.Where(x => x.KeyFacility == SiteID.FirstOrDefault()).ToList();

            buildings = query.Select(x =>
                        new DimBuilding()
                        {
                            BuildingName = x.BuildingName,
                            KeyFacility = x.KeyFacility,
                        });

            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(buildings, JsonRequestBehavior.AllowGet);
            }


            return RedirectToAction("Index");
        }

    }
}
