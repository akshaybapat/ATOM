using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net.Http;

namespace AtomConfiguratorModel.Models
{
    public class DimBusinessPartnerController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: /DimBusinessPartner/
        public ActionResult Index(string sortOrder, string currentFilter, string columnFilter, string searchString, int? page)
        {
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

            var dimBusinessPartners = db.DimBusinessPartners.AsQueryable();

            ViewBag.BPCodeList = db.DimBusinessPartners.Select(b => b.BPCode).Distinct();

            if (!String.IsNullOrEmpty(searchString))
            {
                dimBusinessPartners = dimBusinessPartners.Where(s => s.BusinessPartnerName.ToUpper().Contains(searchString.ToUpper()) || s.BPCode.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!String.IsNullOrEmpty(columnFilter) && !columnFilter.Equals("ALL"))
            {

                dimBusinessPartners = dimBusinessPartners.Where(s => s.BPCode.ToUpper().Contains(columnFilter.ToUpper()));

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



        // POST: /DimBusinessPartner/Update/
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Update")]
        public ActionResult Update(DimFFInstanceAJAXModel ffInstanceAJAXModel)
        {
            var Building = db.DimBuildings.Where(x => x.BuildingName.Equals(ffInstanceAJAXModel.buildingname));

            if (ffInstanceAJAXModel.assignedcustomers != null)
            {
                foreach (string ffinstance in ffInstanceAJAXModel.assignedcustomers)
                {
                    System.Diagnostics.Debug.Write(ffinstance + '\n');

                    var bpRow = db.DimBusinessPartners.Where(x => x.BPCode.Equals(ffinstance)).First();

                    if (!bpRow.KeyBuilding.HasValue)
                    {
                        bpRow.KeyBuilding = Building.FirstOrDefault().id;

                        System.Diagnostics.Debug.Write(bpRow.KeyBuilding);

                    }
                    db.SaveChanges();

                    System.Diagnostics.Debug.Write("Assigned List Saved" + '\n');

                }
            }

            if (ffInstanceAJAXModel.availablecustomers != null)
            {
                foreach (string ffinstance in ffInstanceAJAXModel.availablecustomers)
                {
            
                    var bpRow = db.DimBusinessPartners.Where(x => x.BPCode.Equals(ffinstance)).First();

                    if (bpRow.KeyBuilding.HasValue)
                    {
                        bpRow.KeyBuilding = null;

                        System.Diagnostics.Debug.Write(bpRow.KeyBuilding);

                    }
                    db.SaveChanges();

                 
                }
            }

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
