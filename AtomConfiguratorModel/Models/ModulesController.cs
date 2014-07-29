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
    public class ModulesController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: Modules
        public ActionResult Index()
        {
            var dimModules = db.DimModules.Include(d => d.DimBuilding);
            return View(dimModules.ToList());
        }

        // GET: Modules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimModule dimModule = db.DimModules.Find(id);
            if (dimModule == null)
            {
                return HttpNotFound();
            }
            return View(dimModule);
        }

        // GET: Modules/Create
        public ActionResult Create()
        {
            ViewBag.KeyBuilding = new SelectList(db.DimBuildings, "id", "BuildingName");
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ModuleName,KeyBuilding")] DimModule dimModule)
        {
            if (ModelState.IsValid)
            {
                db.DimModules.Add(dimModule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KeyBuilding = new SelectList(db.DimBuildings, "id", "BuildingName", dimModule.KeyBuilding);
            return View(dimModule);
        }

        // GET: Modules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimModule dimModule = db.DimModules.Find(id);
            if (dimModule == null)
            {
                return HttpNotFound();
            }
            ViewBag.KeyBuilding = new SelectList(db.DimBuildings, "id", "BuildingName", dimModule.KeyBuilding);
            return View(dimModule);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ModuleName,KeyBuilding")] DimModule dimModule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimModule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KeyBuilding = new SelectList(db.DimBuildings, "id", "BuildingName", dimModule.KeyBuilding);
            return View(dimModule);
        }

        // GET: Modules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimModule dimModule = db.DimModules.Find(id);
            if (dimModule == null)
            {
                return HttpNotFound();
            }
            return View(dimModule);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimModule dimModule = db.DimModules.Find(id);
            db.DimModules.Remove(dimModule);
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


        // GET: /Modules/ModulesList
        public ActionResult ModulesList(String id)
        {

            IEnumerable<DimModule> modules = new List<DimModule>();

            String BuildingName = id;

            var BuildingID = from r in db.DimBuildings where r.BuildingName.Equals(BuildingName) select r.id;

            var query = db.DimModules.Where(x => x.KeyBuilding == BuildingID.FirstOrDefault()).ToList();

            modules = query.Select(x =>
                        new DimModule()
                        {
                            ModuleName = x.ModuleName,
                        });

            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(modules, JsonRequestBehavior.AllowGet);
            }


            return RedirectToAction("Index");
        }

    }
}
