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
    public class MetricsController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: Metrics
        public ActionResult Index(string sortOrder, string currentFilter, string columnFilter, string searchString, int? page)
        {
            //return View(db.Metrics.ToList());

            ViewBag.ColumnFilter = (columnFilter != null) ? columnFilter : "ALL";

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

            var Metrics = db.Metrics.AsQueryable();

            ViewBag.MetricsList = db.Metrics.Select(b => b.MetricName).Distinct();

            if (!String.IsNullOrEmpty(searchString))
            {
                Metrics = Metrics.Where(s => s.MetricName.ToUpper().Contains(searchString.ToUpper()) );
            }

            if (!String.IsNullOrEmpty(columnFilter) && !columnFilter.Equals("ALL"))
            {

                Metrics = Metrics.Where(s => s.MetricName.ToUpper().Contains(columnFilter.ToUpper()));

            }

            switch (sortOrder)
            {

                case "Metrics_Desc":
                    Metrics = Metrics.OrderByDescending(s => s.MetricName);
                    break;

                default:
                    Metrics = Metrics.OrderBy(s => s.MetricName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Metrics.ToPagedList(pageNumber, pageSize));
        }

        // GET: Metrics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Metric metric = db.Metrics.Find(id);
            if (metric == null)
            {
                return HttpNotFound();
            }
            return View(metric);
        }

        // GET: Metrics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Metrics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,MetricName,Status")] Metric metric)
        {
            if (ModelState.IsValid)
            {
                db.Metrics.Add(metric);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(metric);
        }

        // GET: Metrics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Metric metric = db.Metrics.Find(id);
            if (metric == null)
            {
                return HttpNotFound();
            }
            return View(metric);
        }

        // POST: Metrics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,MetricName,Status")] Metric metric)
        {
            if (ModelState.IsValid)
            {
                db.Entry(metric).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(metric);
        }

        // GET: Metrics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Metric metric = db.Metrics.Find(id);
            if (metric == null)
            {
                return HttpNotFound();
            }
            return View(metric);
        }

        // POST: Metrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Metric metric = db.Metrics.Find(id);
            db.Metrics.Remove(metric);
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
