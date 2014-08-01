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
    public class DimCountryController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: /DimCountry/
        public ActionResult Index(string searchString)
        {
            var dimcountries = db.DimCountries.Include(d => d.DimRegion);

            if (!String.IsNullOrEmpty(searchString))
            {
                dimcountries = dimcountries.Where(s => s.CountryName.Contains(searchString));

            } 

            return View(dimcountries.ToList());
        }

        // GET: /DimCountry/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimCountry dimcountry = db.DimCountries.Find(id);
            if (dimcountry == null)
            {
                return HttpNotFound();
            }
            return View(dimcountry);
        }

        // GET: /DimCountry/Create
        public ActionResult Create()
        {
            ViewBag.KeyRegion = new SelectList(db.DimRegions, "id", "RegionName");
            return View();
        }

        // POST: /DimCountry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,CountryName,KeyRegion")] DimCountry dimcountry)
        {
            if (ModelState.IsValid)
            {
                db.DimCountries.Add(dimcountry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KeyRegion = new SelectList(db.DimRegions, "id", "RegionName", dimcountry.KeyRegion);
            return View(dimcountry);
        }

        // GET: /DimCountry/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimCountry dimcountry = db.DimCountries.Find(id);
            if (dimcountry == null)
            {
                return HttpNotFound();
            }
            ViewBag.KeyRegion = new SelectList(db.DimRegions, "id", "RegionName", dimcountry.KeyRegion);
            return View(dimcountry);
        }

        // POST: /DimCountry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,CountryName,KeyRegion")] DimCountry dimcountry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimcountry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KeyRegion = new SelectList(db.DimRegions, "id", "RegionName", dimcountry.KeyRegion);
            return View(dimcountry);
        }

        // GET: /DimCountry/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimCountry dimcountry = db.DimCountries.Find(id);
            if (dimcountry == null)
            {
                return HttpNotFound();
            }
            return View(dimcountry);
        }

        // POST: /DimCountry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimCountry dimcountry = db.DimCountries.Find(id);
            db.DimCountries.Remove(dimcountry);
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

        // GET: /DimCountry/CountryList
        public ActionResult CountryList(String id)
        {
            String RegionName = id;

            var RegionID = from r in db.DimRegions
                           where r.RegionName.Equals(RegionName)
                           select r.id;

            var countries = from r in db.DimCountries
                            where r.KeyRegion == RegionID.FirstOrDefault()
                            select r.CountryName;

            if (HttpContext.Request.IsAjaxRequest())

                return Json(new SelectList(
                                countries.ToList())
                           , JsonRequestBehavior.AllowGet);

            return RedirectToAction("Index");
        }


    }
}
