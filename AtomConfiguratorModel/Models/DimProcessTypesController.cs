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
    public class DimProcessTypesController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: DimProcessTypes
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

            var dimProcessTypes = db.DimProcessTypes.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                dimProcessTypes = dimProcessTypes.Where(s => s.ProcessTypeName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "ProcessTypeName_Desc":
                    dimProcessTypes = dimProcessTypes.OrderByDescending(s => s.ProcessTypeName);
                    break;

                default:
                    dimProcessTypes = dimProcessTypes.OrderBy(s => s.ProcessTypeName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dimProcessTypes.ToPagedList(pageNumber, pageSize));
        }

        // GET: DimProcessTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimProcessType dimProcessType = db.DimProcessTypes.Find(id);
            if (dimProcessType == null)
            {
                return HttpNotFound();
            }
            return View(dimProcessType);
        }

        // GET: DimProcessTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DimProcessTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ProcessTypeName")] DimProcessType dimProcessType)
        {
            if (ModelState.IsValid)
            {
                db.DimProcessTypes.Add(dimProcessType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dimProcessType);
        }

        // GET: DimProcessTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimProcessType dimProcessType = db.DimProcessTypes.Find(id);
            if (dimProcessType == null)
            {
                return HttpNotFound();
            }
            return View(dimProcessType);
        }

        // POST: DimProcessTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ProcessTypeName")] DimProcessType dimProcessType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimProcessType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dimProcessType);
        }

        // GET: DimProcessTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimProcessType dimProcessType = db.DimProcessTypes.Find(id);
            if (dimProcessType == null)
            {
                return HttpNotFound();
            }
            return View(dimProcessType);
        }

        // POST: DimProcessTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimProcessType dimProcessType = db.DimProcessTypes.Find(id);
            db.DimProcessTypes.Remove(dimProcessType);
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
