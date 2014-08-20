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
    public class DimStationTypesController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: DimStationTypes
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

            var dimStationTypes = db.DimStationTypes.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                dimStationTypes = dimStationTypes.Where(s => s.StationTypeName.ToUpper().Contains(searchString.ToUpper()) || s.DimBucket.BucketName.ToUpper().Contains(searchString.ToUpper()) || s.FFInstanceID.ToString().ToUpper().Contains(searchString.ToUpper()) || s.Status.ToString().Equals(searchString));
            }

            switch (sortOrder)
            {
                case "StationTypeName_Desc":
                    dimStationTypes = dimStationTypes.OrderByDescending(s => s.StationTypeName);
                    break;

                case "BucketName_Desc":
                    dimStationTypes = dimStationTypes.OrderByDescending(s => s.DimBucket.BucketName);
                    break;

                case "BucketName":
                    dimStationTypes = dimStationTypes.OrderBy(s => s.DimBucket.BucketName);
                    break;

                case "FFInstanceID_Desc":
                    dimStationTypes = dimStationTypes.OrderByDescending(s => s.FFInstanceID);
                    break;

                case "FFInstanceID":
                    dimStationTypes = dimStationTypes.OrderBy(s => s.FFInstanceID);
                    break;

                case "Status_Desc":
                    dimStationTypes = dimStationTypes.OrderByDescending(s => s.Status.ToString());
                    break;

                case "Status":
                    dimStationTypes = dimStationTypes.OrderBy(s => s.Status.ToString());
                    break;

                default:
                    dimStationTypes = dimStationTypes.OrderBy(s => s.StationTypeName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dimStationTypes.ToPagedList(pageNumber, pageSize));
        }

        // GET: DimStationTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimStationType dimStationType = db.DimStationTypes.Find(id);
            if (dimStationType == null)
            {
                return HttpNotFound();
            }
            return View(dimStationType);
        }

        // GET: DimStationTypes/Create
        public ActionResult Create()
        {
            ViewBag.KeyBucket = new SelectList(db.DimBuckets, "id", "BucketName");
            return View();
        }

        // POST: DimStationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,StationTypeName,KeyBucket,Status,FFInstanceID")] DimStationType dimStationType)
        {
            if (ModelState.IsValid)
            {
                db.DimStationTypes.Add(dimStationType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KeyBucket = new SelectList(db.DimBuckets, "id", "BucketName", dimStationType.KeyBucket);
            return View(dimStationType);
        }

        // GET: DimStationTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimStationType dimStationType = db.DimStationTypes.Find(id);
            if (dimStationType == null)
            {
                return HttpNotFound();
            }
            ViewBag.KeyBucket = new SelectList(db.DimBuckets, "id", "BucketName", dimStationType.KeyBucket);
            return View(dimStationType);
        }

        // POST: DimStationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,StationTypeName,KeyBucket,Status,FFInstanceID")] DimStationType dimStationType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimStationType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KeyBucket = new SelectList(db.DimBuckets, "id", "BucketName", dimStationType.KeyBucket);
            return View(dimStationType);
        }

        // GET: DimStationTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimStationType dimStationType = db.DimStationTypes.Find(id);
            if (dimStationType == null)
            {
                return HttpNotFound();
            }
            return View(dimStationType);
        }

        // POST: DimStationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimStationType dimStationType = db.DimStationTypes.Find(id);
            db.DimStationTypes.Remove(dimStationType);
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
