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
    public class DimPhysicalStationsController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: DimPhysicalStations
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

            var dimPhysicalStations = db.DimPhysicalStations.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                dimPhysicalStations = dimPhysicalStations.Where(s => s.StationName.ToUpper().Contains(searchString.ToUpper()) || s.HostAlias.ToUpper().Contains(searchString.ToUpper()) || s.DimStationType.StationTypeName.Equals(searchString) || s.MemorySize.ToUpper().Contains(searchString.ToUpper()) || s.CPUMegaHertz.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "StationName_Desc":
                    dimPhysicalStations = dimPhysicalStations.OrderByDescending(s => s.StationName);
                    break;

                case "HostAlias_Desc":
                    dimPhysicalStations = dimPhysicalStations.OrderByDescending(s => s.HostAlias);
                    break;

                case "HostAlias":
                    dimPhysicalStations = dimPhysicalStations.OrderBy(s => s.HostAlias);
                    break;

                case "StationTypeName_Desc":
                    dimPhysicalStations = dimPhysicalStations.OrderByDescending(s => s.DimStationType.StationTypeName);
                    break;

                case "StationTypeName":
                    dimPhysicalStations = dimPhysicalStations.OrderBy(s => s.KeyStationType);
                    break;

                case "MemorySize_Desc":
                    dimPhysicalStations = dimPhysicalStations.OrderByDescending(s => s.MemorySize);
                    break;

                case "MemorySize":
                    dimPhysicalStations = dimPhysicalStations.OrderBy(s => s.MemorySize);
                    break;

                case "CPUMegaHertz_Desc":
                    dimPhysicalStations = dimPhysicalStations.OrderByDescending(s => s.CPUMegaHertz);
                    break;

                case "CPUMegaHertz":
                    dimPhysicalStations = dimPhysicalStations.OrderBy(s => s.CPUMegaHertz);
                    break;

                default:
                    dimPhysicalStations = dimPhysicalStations.OrderBy(s => s.StationName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dimPhysicalStations.ToPagedList(pageNumber, pageSize));
        }

        // GET: DimPhysicalStations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimPhysicalStation dimPhysicalStation = db.DimPhysicalStations.Find(id);
            if (dimPhysicalStation == null)
            {
                return HttpNotFound();
            }
            return View(dimPhysicalStation);
        }

        // GET: DimPhysicalStations/Create
        public ActionResult Create()
        {
            ViewBag.KeyStationType = new SelectList(db.DimStationTypes, "id", "StationTypeName");
            return View();
        }

        // POST: DimPhysicalStations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,StationName,HostAlias,CPUMegaHertz,MemorySize,KeyStationType")] DimPhysicalStation dimPhysicalStation)
        {
            if (ModelState.IsValid)
            {
                db.DimPhysicalStations.Add(dimPhysicalStation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KeyStationType = new SelectList(db.DimStationTypes, "id", "StationTypeName", dimPhysicalStation.KeyStationType);
            return View(dimPhysicalStation);
        }

        // GET: DimPhysicalStations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimPhysicalStation dimPhysicalStation = db.DimPhysicalStations.Find(id);
            if (dimPhysicalStation == null)
            {
                return HttpNotFound();
            }
            ViewBag.KeyStationType = new SelectList(db.DimStationTypes, "id", "StationTypeName", dimPhysicalStation.KeyStationType);
            return View(dimPhysicalStation);
        }

        // POST: DimPhysicalStations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,StationName,HostAlias,CPUMegaHertz,MemorySize,KeyStationType")] DimPhysicalStation dimPhysicalStation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimPhysicalStation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KeyStationType = new SelectList(db.DimStationTypes, "id", "StationTypeName", dimPhysicalStation.KeyStationType);
            return View(dimPhysicalStation);
        }

        // GET: DimPhysicalStations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimPhysicalStation dimPhysicalStation = db.DimPhysicalStations.Find(id);
            if (dimPhysicalStation == null)
            {
                return HttpNotFound();
            }
            return View(dimPhysicalStation);
        }

        // POST: DimPhysicalStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimPhysicalStation dimPhysicalStation = db.DimPhysicalStations.Find(id);
            db.DimPhysicalStations.Remove(dimPhysicalStation);
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
