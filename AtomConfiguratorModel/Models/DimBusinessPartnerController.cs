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
    public class DimBusinessPartnerController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: /DimBusinessPartner/
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

            var dimBusinessPartners = db.DimBusinessPartners.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                dimBusinessPartners = dimBusinessPartners.Where(s => s.BusinessPartnerName.ToUpper().Contains(searchString.ToUpper()) || s.BPCode.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {

                case "BPName_Desc":
                    dimBusinessPartners = dimBusinessPartners.OrderByDescending(s => s.BusinessPartnerName);
                    break;

                case "BPCode_Desc":
                    dimBusinessPartners = dimBusinessPartners.OrderByDescending(s => s.BPCode);
                    break;

                case "BPCode":
                    dimBusinessPartners = dimBusinessPartners.OrderBy(s => s.BPCode);
                    break;

                default:
                    dimBusinessPartners = dimBusinessPartners.OrderBy(s => s.BusinessPartnerName);
                    break;
            }

            //return View(dimBusinessPartners.ToList());
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dimBusinessPartners.ToPagedList(pageNumber, pageSize));
        }

        // GET: /DimBusinessPartner/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimBusinessPartner dimbusinesspartner = db.DimBusinessPartners.Find(id);
            if (dimbusinesspartner == null)
            {
                return HttpNotFound();
            }
            return View(dimbusinesspartner);
        }

        // GET: /DimBusinessPartner/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /DimBusinessPartner/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,BusinessPartnerName,BPCode")] DimBusinessPartner dimbusinesspartner)
        {
            if (ModelState.IsValid)
            {
                db.DimBusinessPartners.Add(dimbusinesspartner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dimbusinesspartner);
        }

        // GET: /DimBusinessPartner/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimBusinessPartner dimbusinesspartner = db.DimBusinessPartners.Find(id);
            if (dimbusinesspartner == null)
            {
                return HttpNotFound();
            }
            return View(dimbusinesspartner);
        }

        // POST: /DimBusinessPartner/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,BusinessPartnerName,BPCode")] DimBusinessPartner dimbusinesspartner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimbusinesspartner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dimbusinesspartner);
        }

        // GET: /DimBusinessPartner/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimBusinessPartner dimbusinesspartner = db.DimBusinessPartners.Find(id);
            if (dimbusinesspartner == null)
            {
                return HttpNotFound();
            }
            return View(dimbusinesspartner);
        }

        // POST: /DimBusinessPartner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimBusinessPartner dimbusinesspartner = db.DimBusinessPartners.Find(id);
            db.DimBusinessPartners.Remove(dimbusinesspartner);
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
