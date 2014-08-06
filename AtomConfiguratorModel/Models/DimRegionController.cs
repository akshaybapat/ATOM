using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace AtomConfiguratorModel.Models
{
    public class DimRegionController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: /DimRegion/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var dimRegions = db.DimRegions.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                dimRegions = dimRegions.Where(s => s.RegionName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "Region_Desc":
                    dimRegions = dimRegions.OrderByDescending(s => s.RegionName);
                    break;

                default:
                    dimRegions = dimRegions.OrderBy(s => s.RegionName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dimRegions.ToPagedList(pageNumber, pageSize));
        }

        // GET: /DimRegion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimRegion dimregion = db.DimRegions.Find(id);
            if (dimregion == null)
            {
                return HttpNotFound();
            }
            return View(dimregion);
        }

        // GET: /DimRegion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /DimRegion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,RegionName")] DimRegion dimregion)
        {
            if (ModelState.IsValid)
            {
                db.DimRegions.Add(dimregion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dimregion);
        }

        // GET: /DimRegion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimRegion dimregion = db.DimRegions.Find(id);
            if (dimregion == null)
            {
                return HttpNotFound();
            }
            return View(dimregion);
        }

        // POST: /DimRegion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,RegionName")] DimRegion dimregion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimregion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dimregion);
        }

        // GET: /DimRegion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimRegion dimregion = db.DimRegions.Find(id);
            if (dimregion == null)
            {
                return HttpNotFound();
            }
            return View(dimregion);
        }

        // POST: /DimRegion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimRegion dimregion = db.DimRegions.Find(id);
            db.DimRegions.Remove(dimregion);
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
