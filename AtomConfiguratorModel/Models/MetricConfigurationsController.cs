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
    public class MetricConfigurationsController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: MetricConfigurations
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

            var metricConfigurations = db.MetricConfigurations.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {

                metricConfigurations = metricConfigurations.Where(s => s.Metric.MetricName.ToUpper().Contains(searchString.ToUpper()) || s.MetricRoleType.RoleType.ToUpper().Contains(searchString.ToUpper()) || s.DimBusinessPartner.BusinessPartnerName.ToUpper().Contains(searchString.ToUpper()) || s.DimFacility.SiteName.ToUpper().Contains(searchString.ToUpper()) || s.DimFFInstance.DatabaseName.ToUpper().Contains(searchString.ToUpper()) || s.Goal.ToUpper().Contains(searchString.ToUpper()) || s.Status.ToUpper().Contains(searchString.ToUpper()) || s.Red.ToUpper().Contains(searchString.ToUpper()) || s.Green.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Metric": metricConfigurations = metricConfigurations.OrderBy(s => s.Metric.MetricName); break;
                case "Metric_Desc": metricConfigurations = metricConfigurations.OrderByDescending(s => s.Metric.MetricName); break;
                case "MetricRoleType": metricConfigurations = metricConfigurations.OrderBy(s => s.MetricRoleType.RoleType); break;
                case "MetricRoleType_Desc": metricConfigurations = metricConfigurations.OrderByDescending(s => s.MetricRoleType.RoleType); break;
                case "DimBusinessPartner": metricConfigurations = metricConfigurations.OrderBy(s => s.DimBusinessPartner.BusinessPartnerName); break;
                case "DimBusinessPartner_Desc": metricConfigurations = metricConfigurations.OrderByDescending(s => s.DimBusinessPartner.BusinessPartnerName); break;
                case "DimFacility": metricConfigurations = metricConfigurations.OrderBy(s => s.DimFacility.SiteName); break;
                case "DimFacility_Desc": metricConfigurations = metricConfigurations.OrderByDescending(s => s.DimFacility.SiteName); break;
                case "DimFFInstance": metricConfigurations = metricConfigurations.OrderBy(s => s.DimFFInstance.DatabaseName); break;
                case "DimFFInstance_Desc": metricConfigurations = metricConfigurations.OrderByDescending(s => s.DimFFInstance.DatabaseName); break;
                case "Goal": metricConfigurations = metricConfigurations.OrderBy(s => s.Goal); break;
                case "Goal_Desc": metricConfigurations = metricConfigurations.OrderByDescending(s => s.Goal); break;
                case "Status": metricConfigurations = metricConfigurations.OrderBy(s => s.Status); break;
                case "Status_Desc": metricConfigurations = metricConfigurations.OrderByDescending(s => s.Status); break;
                case "Red": metricConfigurations = metricConfigurations.OrderBy(s => s.Red); break;
                case "Red_Desc": metricConfigurations = metricConfigurations.OrderByDescending(s => s.Red); break;
                case "Green": metricConfigurations = metricConfigurations.OrderBy(s => s.Green); break;
                case "Green_Desc": metricConfigurations = metricConfigurations.OrderByDescending(s => s.Green); break;

                default:
                    metricConfigurations = metricConfigurations.OrderBy(s => s.Metric.MetricName);
                    break;
            }


            //return View(dimffinstances.ToList());
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(metricConfigurations.ToPagedList(pageNumber, pageSize)); 
            //return View(db.MetricConfigurations.ToList());

        }

        // GET: MetricConfigurations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetricConfiguration metricConfiguration = db.MetricConfigurations.Find(id);
            if (metricConfiguration == null)
            {
                return HttpNotFound();
            }
            return View(metricConfiguration);
        }

        // GET: MetricConfigurations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MetricConfigurations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,KeyMetricID,KeyMetricRoleTypeID,KeySiteID,KeyBusinessPartnerID,KeyFFInstanceID,Goal,Red,Green,Alert,Alert_MasterDataChange,Alert_SystemErrors,MetricManagerValidationStatus,MetricOwnerValidationStatus,Status")] MetricConfiguration metricConfiguration)
        {
            if (ModelState.IsValid)
            {
                db.MetricConfigurations.Add(metricConfiguration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(metricConfiguration);
        }

        // GET: MetricConfigurations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetricConfiguration metricConfiguration = db.MetricConfigurations.Find(id);
            if (metricConfiguration == null)
            {
                return HttpNotFound();
            }
            return View(metricConfiguration);
        }

        // POST: MetricConfigurations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,KeyMetricID,KeyMetricRoleTypeID,KeySiteID,KeyBusinessPartnerID,KeyFFInstanceID,Goal,Red,Green,Alert,Alert_MasterDataChange,Alert_SystemErrors,MetricManagerValidationStatus,MetricOwnerValidationStatus,Status")] MetricConfiguration metricConfiguration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(metricConfiguration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(metricConfiguration);
        }

        // GET: MetricConfigurations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetricConfiguration metricConfiguration = db.MetricConfigurations.Find(id);
            if (metricConfiguration == null)
            {
                return HttpNotFound();
            }
            return View(metricConfiguration);
        }

        // POST: MetricConfigurations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MetricConfiguration metricConfiguration = db.MetricConfigurations.Find(id);
            db.MetricConfigurations.Remove(metricConfiguration);
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
