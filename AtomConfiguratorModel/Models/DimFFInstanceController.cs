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
    public class DimFFInstanceController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: /DimFFInstance/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var dimffinstances = db.DimFFInstances.Include(d => d.DimModule);

            if (!String.IsNullOrEmpty(searchString))
            {

                dimffinstances = dimffinstances.Where(s => s.HostName.ToUpper().Contains(searchString.ToUpper()) || s.DatabaseName.ToUpper().Contains(searchString.ToUpper()) || s.DataSourceName.ToUpper().Contains(searchString.ToUpper()) || s.ProjectName.ToUpper().Contains(searchString.ToUpper()) || s.DataFilePrefix.ToUpper().Contains(searchString.ToUpper()) || s.DimModule.ModuleName.ToUpper().Contains(searchString.ToUpper()) || s.DimProductNumbers.FirstOrDefault().ProductNumber.ToUpper().Contains(searchString.ToUpper()) || s.SiteName.ToUpper().Contains(searchString.ToUpper()) || s.QAContactName.ToUpper().Contains(searchString.ToUpper()) || s.ITContactName.ToUpper().Contains(searchString.ToUpper()) || s.UserName.ToUpper().Contains(searchString.ToUpper()) || s.BaanCoNo.ToUpper().Contains(searchString.ToUpper()));
            }
                switch (sortOrder)
                {

                    default:
                        dimffinstances = dimffinstances.OrderBy(s => s.HostName);
                        break;
                }

            
                //return View(dimffinstances.ToList());
                int pageSize = 15;
                int pageNumber = (page ?? 1);
                return View(dimffinstances.ToPagedList(pageNumber, pageSize));
            
        }

        // GET: /DimFFInstance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimFFInstance dimffinstance = db.DimFFInstances.Find(id);
            if (dimffinstance == null)
            {
                return HttpNotFound();
            }
            return View(dimffinstance);
        }

        // GET: /DimFFInstance/Create
        public ActionResult Create()
        {
            ViewBag.KeyModule = new SelectList(db.DimModules, "id", "ModuleName");
            return View();
        }

        // POST: /DimFFInstance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,DataSourceName,HostName,DatabaseName,UserName,Password,ProjectName,DataFilePrefix,ReplicationDelayMinute,IsActive,KeyModule,ITContactName,ITPhone,ITEmail,QAContactName,QAPhone,QAEmail,SiteName,BaanCoNo")] DimFFInstance dimffinstance)
        {
            if (ModelState.IsValid)
            {
                db.DimFFInstances.Add(dimffinstance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KeyModule = new SelectList(db.DimModules, "id", "ModuleName", dimffinstance.KeyModule);
            return View(dimffinstance);
        }

        // GET: /DimFFInstance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimFFInstance dimffinstance = db.DimFFInstances.Find(id);
            if (dimffinstance == null)
            {
                return HttpNotFound();
            }
            ViewBag.KeyModule = new SelectList(db.DimModules, "id", "ModuleName", dimffinstance.KeyModule);
            return View(dimffinstance);
        }

        // POST: /DimFFInstance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,DataSourceName,HostName,DatabaseName,UserName,Password,ProjectName,DataFilePrefix,ReplicationDelayMinute,IsActive,KeyModule,ITContactName,ITPhone,ITEmail,QAContactName,QAPhone,QAEmail,SiteName,BaanCoNo")] DimFFInstance dimffinstance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dimffinstance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KeyModule = new SelectList(db.DimModules, "id", "ModuleName", dimffinstance.KeyModule);
            return View(dimffinstance);
        }

        // GET: /DimFFInstance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimFFInstance dimffinstance = db.DimFFInstances.Find(id);
            if (dimffinstance == null)
            {
                return HttpNotFound();
            }
            return View(dimffinstance);
        }

        // POST: /DimFFInstance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DimFFInstance dimffinstance = db.DimFFInstances.Find(id);
            db.DimFFInstances.Remove(dimffinstance);
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
