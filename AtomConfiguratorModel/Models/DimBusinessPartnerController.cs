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
    public class DimBusinessPartnerController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: /DimBusinessPartner/
        public ActionResult Index(string searchString)
        {

            if (!String.IsNullOrEmpty(searchString))
            {
                return View(db.DimBusinessPartners.Where(s => s.BusinessPartnerName.Contains(searchString)));
            } 

            return View(db.DimBusinessPartners.ToList());
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
