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
    public class DimFacilityController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();
        
        // GET: /DimFacility/
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

            var dimfacilities = db.DimFacilities.Include(d => d.DimCountry);

            if (!String.IsNullOrEmpty(searchString))
            {
                dimfacilities = dimfacilities.Where(s => s.SiteName.ToUpper().Contains(searchString.ToUpper()) || s.DimCountry.CountryName.ToUpper().Contains(searchString.ToUpper()));
                
            }

            switch (sortOrder)
            {

                case "Facility_Desc":
                    dimfacilities = dimfacilities.OrderByDescending(s => s.SiteName);
                    break;

                case "Country_Desc":
                    dimfacilities = dimfacilities.OrderByDescending(s => s.DimCountry.CountryName);
                    break;

                case "Country":
                    dimfacilities = dimfacilities.OrderBy(s => s.DimCountry.CountryName);
                    break;

                default:
                    dimfacilities = dimfacilities.OrderBy(s => s.SiteName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dimfacilities.ToPagedList(pageNumber, pageSize));
        }

        // GET: /DimFacility/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimFacility dimfacility = db.DimFacilities.Find(id);
            if (dimfacility == null)
            {
                return HttpNotFound();
            }
            return View(dimfacility);
        }

        // GET: /DimFacility/Create
        public ActionResult Create()
        {
            ViewBag.KeyCountry = new SelectList(db.DimCountries, "id", "CountryName");
            return View();
        }

        // POST: /DimFacility/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,SiteName,KeyCountry")] DimFacility dimfacility)
        {
            if (ModelState.IsValid)
            {
                db.DimFacilities.Add(dimfacility);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KeyCountry = new SelectList(db.DimCountries, "id", "CountryName", dimfacility.KeyCountry);
            return View(dimfacility);
        }

        // GET: /DimFacility/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimFacility dimfacility = db.DimFacilities.Find(id);
            if (dimfacility == null)
            {
                return HttpNotFound();
            }
            ViewBag.KeyCountry = new SelectList(db.DimCountries, "id", "CountryName", dimfacility.KeyCountry);
            return View(dimfacility);
        }

        // POST: /DimFacility/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,SiteName,KeyCountry")] DimFacility dimfacility)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimfacility).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KeyCountry = new SelectList(db.DimCountries, "id", "CountryName", dimfacility.KeyCountry);
            return View(dimfacility);
        }

        // GET: /DimFacility/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimFacility dimfacility = db.DimFacilities.Find(id);
            if (dimfacility == null)
            {
                return HttpNotFound();
            }
            return View(dimfacility);
        }

        // POST: /DimFacility/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimFacility dimfacility = db.DimFacilities.Find(id);
            db.DimFacilities.Remove(dimfacility);
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

        // GET: /DimFacility/FacilityForm
        public ActionResult FacilityForm(string buildingsbycountry)
        {
       
            ViewBag.KeyRegion = (from r in db.DimRegions
            select r.RegionName).Distinct();

            var Facilities = from r in db.DimFacilities
                          select r;

            if (!string.IsNullOrEmpty(buildingsbycountry))
            {
                Facilities = Facilities.Where(x => x.DimCountry.CountryName.Equals(buildingsbycountry));
            }


            var RegLst = new List<string>();

            RegLst.AddRange(Facilities.Select(s => s.SiteName).Distinct());

            if (HttpContext.Request.IsAjaxRequest())

                return Json(new SelectList(
                             RegLst)
                           , JsonRequestBehavior.AllowGet);

            ViewBag.buildingsbycountry = new SelectList(RegLst);
                                   
            SiteModel site = new SiteModel();

            return View(site);
        }

        // GET: /DimFacility/FacilityList
        public ActionResult FacilityList(String id)
        {
            String CountryName = id;

            var CountryID = from r in db.DimCountries
                            where r.CountryName.Equals(CountryName)
                            select r.id;

            var facilities = from r in db.DimFacilities
                            where r.KeyCountry == CountryID.FirstOrDefault()
                            select r.SiteName;

            if (HttpContext.Request.IsAjaxRequest()) 

            return Json(new SelectList(
                            facilities.ToList())
                       , JsonRequestBehavior.AllowGet);

            return RedirectToAction("Index");
        }
        

    }
}
