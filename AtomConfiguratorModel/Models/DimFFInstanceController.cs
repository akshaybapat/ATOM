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

                dimffinstances = dimffinstances.Where(s => s.HostName.ToUpper().Contains(searchString.ToUpper()) || s.DatabaseName.ToUpper().Contains(searchString.ToUpper()) || s.DataSourceName.ToUpper().Contains(searchString.ToUpper()) || s.ProjectName.ToUpper().Contains(searchString.ToUpper()) || s.DataFilePrefix.ToUpper().Contains(searchString.ToUpper()) || s.DimModule.ModuleName.ToUpper().Contains(searchString.ToUpper()) || s.DimProductNumbers.FirstOrDefault().ProductNumber.ToUpper().Contains(searchString.ToUpper()) || s.SiteName.ToUpper().Contains(searchString.ToUpper()) || s.QAContactName.ToUpper().Contains(searchString.ToUpper()) || s.ITContactName.ToUpper().Contains(searchString.ToUpper()) || s.UserName.ToUpper().Contains(searchString.ToUpper()) || s.BaanCoNo.ToUpper().Contains(searchString.ToUpper()) || s.DimModule.ModuleName.ToUpper().Contains(searchString.ToUpper()));
            }
                switch (sortOrder)
                {
                    case "Hostname": dimffinstances = dimffinstances.OrderBy(s => s.HostName); break;
                    case "Hostname_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.HostName); break;
                    case "Database": dimffinstances = dimffinstances.OrderBy(s => s.DatabaseName); break;
                    case "Database_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.DatabaseName); break;
                    case "DataSourceName": dimffinstances = dimffinstances.OrderBy(s => s.DataSourceName); break;
                    case "DataSourceName_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.DataSourceName); break;
                    case "ProjectName": dimffinstances = dimffinstances.OrderBy(s => s.ProjectName); break;
                    case "ProjectName_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.ProjectName); break;
                    case "SiteName": dimffinstances = dimffinstances.OrderBy(s => s.SiteName); break;
                    case "SiteName_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.SiteName); break;
                    case "QAContactName": dimffinstances = dimffinstances.OrderBy(s => s.QAContactName); break;
                    case "QAContactName_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.QAContactName); break;
                    case "ITContactName": dimffinstances = dimffinstances.OrderBy(s => s.ITContactName); break;
                    case "ITContactName_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.ITContactName); break;
                    case "UserName": dimffinstances = dimffinstances.OrderBy(s => s.UserName); break;
                    case "UserName_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.UserName); break;
                    case "BaanCoNo": dimffinstances = dimffinstances.OrderBy(s => s.BaanCoNo); break;
                    case "BaanCoNo_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.BaanCoNo); break;
                    case "Module": dimffinstances = dimffinstances.OrderBy(s => s.DimModule.ModuleName); break;
                    case "Module_Desc": dimffinstances = dimffinstances.OrderByDescending(s => s.DimModule.ModuleName); break;
                    
                    default:
                        dimffinstances = dimffinstances.OrderBy(s => s.HostName);
                        break;
                }

            
                //return View(dimffinstances.ToList());
                int pageSize = 10;
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
