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
    public class DimFinancialCompanyController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: /DimFinancialCompany/
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

            var dimFinancialCompanies = db.DimFinancialCompanies.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                dimFinancialCompanies = dimFinancialCompanies.Where(s => s.FinancialCompanyName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {

                case "FCName_Desc":
                    dimFinancialCompanies = dimFinancialCompanies.OrderByDescending(s => s.FinancialCompanyName);
                    break;

                default:
                    dimFinancialCompanies = dimFinancialCompanies.OrderBy(s => s.FinancialCompanyName);
                    break;
            }

            //return View(dimFinancialCompanies.ToList());
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dimFinancialCompanies.ToPagedList(pageNumber, pageSize));
        }

        // GET: /DimFinancialCompany/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimFinancialCompany dimfinancialcompany = db.DimFinancialCompanies.Find(id);
            if (dimfinancialcompany == null)
            {
                return HttpNotFound();
            }
            return View(dimfinancialcompany);
        }

        // GET: /DimFinancialCompany/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /DimFinancialCompany/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,FinancialCompanyName")] DimFinancialCompany dimfinancialcompany)
        {
            if (ModelState.IsValid)
            {
                db.DimFinancialCompanies.Add(dimfinancialcompany);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dimfinancialcompany);
        }

        // GET: /DimFinancialCompany/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimFinancialCompany dimfinancialcompany = db.DimFinancialCompanies.Find(id);
            if (dimfinancialcompany == null)
            {
                return HttpNotFound();
            }
            return View(dimfinancialcompany);
        }

        // POST: /DimFinancialCompany/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,FinancialCompanyName")] DimFinancialCompany dimfinancialcompany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimfinancialcompany).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dimfinancialcompany);
        }

        // GET: /DimFinancialCompany/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimFinancialCompany dimfinancialcompany = db.DimFinancialCompanies.Find(id);
            if (dimfinancialcompany == null)
            {
                return HttpNotFound();
            }
            return View(dimfinancialcompany);
        }

        // POST: /DimFinancialCompany/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimFinancialCompany dimfinancialcompany = db.DimFinancialCompanies.Find(id);
            db.DimFinancialCompanies.Remove(dimfinancialcompany);
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
