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
    public class DimBucketsController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: DimBuckets
        public ActionResult Index()
        {
            var dimBuckets = db.DimBuckets.Include(d => d.DimProcessType);
            return View(dimBuckets.ToList());
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
