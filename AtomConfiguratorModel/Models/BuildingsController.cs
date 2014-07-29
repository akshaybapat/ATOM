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
    public class BuildingsController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: Buildings
        public ActionResult Index()
        {
            var dimBuildings = db.DimBuildings.Include(d => d.DimFacility);
            return View(dimBuildings.ToList());
        }

        // GET: Buildings/Details/5
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

        // GET: Buildings/Create
        public ActionResult Create()
        {
            ViewBag.KeyFacility = new SelectList(db.DimFacilities, "id", "SiteName");
            return View();
        }

        // POST: Buildings/Create
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

        // GET: Buildings/Edit/5
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

        // POST: Buildings/Edit/5
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

        // GET: Buildings/Delete/5
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

        // POST: Buildings/Delete/5
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

        // GET: /Buildings/BuildingList
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
