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
    public class DimBucketsController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: DimBuckets
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

            var dimBuckets = db.DimBuckets.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                dimBuckets = dimBuckets.Where(s => s.BucketName.ToUpper().Contains(searchString.ToUpper()) || s.DimProcessType.ProcessTypeName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "BucketName_Desc":
                    dimBuckets = dimBuckets.OrderByDescending(s => s.BucketName);
                    break;

                case "ProcessTypeName_Desc":
                    dimBuckets = dimBuckets.OrderByDescending(s => s.DimProcessType.ProcessTypeName);
                    break;

                case "ProcessTypeName":
                    dimBuckets = dimBuckets.OrderBy(s => s.DimProcessType.ProcessTypeName);
                    break;

                default:
                    dimBuckets = dimBuckets.OrderBy(s => s.BucketName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dimBuckets.ToPagedList(pageNumber, pageSize));
        }

        // GET: DimBuckets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimBucket dimBucket = db.DimBuckets.Find(id);
            if (dimBucket == null)
            {
                return HttpNotFound();
            }
            return View(dimBucket);
        }

        // GET: DimBuckets/Create
        public ActionResult Create()
        {
            ViewBag.KeyProcessType = new SelectList(db.DimProcessTypes, "id", "ProcessTypeName");
            return View();
        }

        // POST: DimBuckets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,BucketName,KeyProcessType")] DimBucket dimBucket)
        {
            if (ModelState.IsValid)
            {
                db.DimBuckets.Add(dimBucket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KeyProcessType = new SelectList(db.DimProcessTypes, "id", "ProcessTypeName", dimBucket.KeyProcessType);
            return View(dimBucket);
        }

        // GET: DimBuckets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimBucket dimBucket = db.DimBuckets.Find(id);
            if (dimBucket == null)
            {
                return HttpNotFound();
            }
            ViewBag.KeyProcessType = new SelectList(db.DimProcessTypes, "id", "ProcessTypeName", dimBucket.KeyProcessType);
            return View(dimBucket);
        }

        // POST: DimBuckets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,BucketName,KeyProcessType")] DimBucket dimBucket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimBucket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KeyProcessType = new SelectList(db.DimProcessTypes, "id", "ProcessTypeName", dimBucket.KeyProcessType);
            return View(dimBucket);
        }

        // GET: DimBuckets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimBucket dimBucket = db.DimBuckets.Find(id);
            if (dimBucket == null)
            {
                return HttpNotFound();
            }
            return View(dimBucket);
        }

        // POST: DimBuckets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimBucket dimBucket = db.DimBuckets.Find(id);
            db.DimBuckets.Remove(dimBucket);
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
