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
    public class DimFFInstanceController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: /DimFFInstance/
        public ActionResult Index()
        {
            var dimffinstances = db.DimFFInstances.Include(d => d.DimModule);
            return View(dimffinstances.ToList());
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
