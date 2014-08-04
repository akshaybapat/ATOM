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
    public class DimBusinessUnitController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: /DimBusinessUnit/
        public ActionResult Index(string sortOrder, string searchString)
        {
            var dimbusinessunits = db.DimBusinessUnits.Include(d => d.DimCostCenter);


            if (!String.IsNullOrEmpty(searchString))
            {
                dimbusinessunits = dimbusinessunits.Where(s => s.BusinessUnitName.ToUpper().Contains(searchString.ToUpper()) || s.DimCostCenter.CostCenter.ToUpper().Contains(searchString.ToUpper()));
               
            }

            switch (sortOrder)
            {

                case "BUName_Desc":
                    dimbusinessunits = dimbusinessunits.OrderByDescending(s => s.BusinessUnitName);
                    break;

                case "CostCenter_Desc":
                    dimbusinessunits = dimbusinessunits.OrderByDescending(s => s.DimCostCenter.CostCenter);
                    break;

                case "CostCenter":
                    dimbusinessunits = dimbusinessunits.OrderBy(s => s.DimCostCenter.CostCenter);
                    break;

                default:
                    dimbusinessunits = dimbusinessunits.OrderBy(s => s.BusinessUnitName);
                    break;
            }

            return View(dimbusinessunits.ToList());
        }

        // GET: /DimBusinessUnit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimBusinessUnit dimbusinessunit = db.DimBusinessUnits.Find(id);
            if (dimbusinessunit == null)
            {
                return HttpNotFound();
            }
            return View(dimbusinessunit);
        }

        // GET: /DimBusinessUnit/Create
        public ActionResult Create()
        {
            ViewBag.KeyCostCenter = new SelectList(db.DimCostCenters, "id", "CostCenter");
            return View();
        }

        // POST: /DimBusinessUnit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,BusinessUnitName,KeyCostCenter")] DimBusinessUnit dimbusinessunit)
        {
            if (ModelState.IsValid)
            {
                db.DimBusinessUnits.Add(dimbusinessunit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KeyCostCenter = new SelectList(db.DimCostCenters, "id", "CostCenter", dimbusinessunit.KeyCostCenter);
            return View(dimbusinessunit);
        }

        // GET: /DimBusinessUnit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimBusinessUnit dimbusinessunit = db.DimBusinessUnits.Find(id);
            if (dimbusinessunit == null)
            {
                return HttpNotFound();
            }
            ViewBag.KeyCostCenter = new SelectList(db.DimCostCenters, "id", "CostCenter", dimbusinessunit.KeyCostCenter);
            return View(dimbusinessunit);
        }

        // POST: /DimBusinessUnit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,BusinessUnitName,KeyCostCenter")] DimBusinessUnit dimbusinessunit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimbusinessunit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KeyCostCenter = new SelectList(db.DimCostCenters, "id", "CostCenter", dimbusinessunit.KeyCostCenter);
            return View(dimbusinessunit);
        }

        // GET: /DimBusinessUnit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimBusinessUnit dimbusinessunit = db.DimBusinessUnits.Find(id);
            if (dimbusinessunit == null)
            {
                return HttpNotFound();
            }
            return View(dimbusinessunit);
        }

        // POST: /DimBusinessUnit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimBusinessUnit dimbusinessunit = db.DimBusinessUnits.Find(id);
            db.DimBusinessUnits.Remove(dimbusinessunit);
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
    }
}
