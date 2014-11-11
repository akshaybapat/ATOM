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

            var metricConfigurations = db.MetricConfigurations.Include(m => m.DimBusinessPartner).Include(m => m.DimFacility).Include(m => m.DimFFInstance).Include(m => m.Metric).Include(m => m.MetricRoleType);
           

            if (!String.IsNullOrEmpty(searchString))
            {
                metricConfigurations = metricConfigurations.Where(s => s.Metric.MetricName.ToUpper().Contains(searchString.ToUpper()) || s.DimBusinessPartner.BusinessPartnerName.ToUpper().Contains(searchString.ToUpper()) || s.DimFacility.SiteName.ToUpper().Contains(searchString.ToUpper()) || s.DimFFInstance.DatabaseName.ToUpper().Contains(searchString.ToUpper()) || s.MetricRoleType.RoleType.ToUpper().Contains(searchString.ToUpper()));

            }

            switch (sortOrder)
            {
                case "Metric" :
                    metricConfigurations = metricConfigurations.OrderBy(s => s.Metric.MetricName);
                    break;

                case "Metric_Desc":
                    metricConfigurations = metricConfigurations.OrderByDescending(s => s.Metric.MetricName);
                    break;

                case "MetricRoleType":
                    metricConfigurations = metricConfigurations.OrderBy(s => s.Metric.MetricName);
                    break;

                case "MetricRoleType_Desc":
                    metricConfigurations = metricConfigurations.OrderByDescending(s => s.Metric.MetricName);
                    break;

                case "DimFacility":
                    metricConfigurations = metricConfigurations.OrderBy(s => s.DimFacility.SiteName);
                    break;

                case "DimFacility_Desc":
                    metricConfigurations = metricConfigurations.OrderByDescending(s => s.DimFacility.SiteName);
                    break;

                case "DimBusinessPartner":
                    metricConfigurations = metricConfigurations.OrderBy(s => s.DimBusinessPartner.BusinessPartnerName);
                    break;

                case "DimBusinessPartner_Desc":
                    metricConfigurations = metricConfigurations.OrderByDescending(s => s.DimBusinessPartner.BusinessPartnerName);
                    break;

                case "FFInstance":
                    metricConfigurations = metricConfigurations.OrderBy(s => s.DimFFInstance.DatabaseName);
                    break;


                case "FFInstance_Desc":
                    metricConfigurations = metricConfigurations.OrderByDescending(s => s.DimFFInstance.DatabaseName);
                    break;

                case "Status":
                    metricConfigurations = metricConfigurations.OrderBy(s => s.Status);
                    break;


                case "Status_Desc":
                    metricConfigurations = metricConfigurations.OrderByDescending(s => s.Status);
                    break;


               default:
                    metricConfigurations = metricConfigurations.OrderBy(s => s.Metric.MetricName);
                    break;

            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(metricConfigurations.ToPagedList(pageNumber, pageSize));
            //return View(metricConfigurations.ToList());
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
            ViewBag.KeyBusinessPartnerID = new SelectList(db.DimBusinessPartners, "id", "BusinessPartnerName");
            ViewBag.KeySiteID = new SelectList(db.DimFacilities, "id", "SiteName");
            ViewBag.KeyFFInstanceID = new SelectList(db.DimFFInstances, "id", "DataSourceName");
            ViewBag.KeyMetricID = new SelectList(db.Metrics, "id", "MetricName");
            ViewBag.KeyMetricRoleTypeID = new SelectList(db.MetricRoleTypes, "id", "RoleName");
            return View();
        }

        // POST: MetricConfigurations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Create([Bind(Include = "id,KeyMetricID,KeyMetricRoleTypeID,KeySiteID,KeyBusinessPartnerID,KeyFFInstanceID,Goal,Red,Green,Alert,Alert_MasterDataChange,Alert_SystemErrors,MetricManager,MetricOwner,MetricManagerValidationStatus,MetricOwnerValidationStatus,Status")] MetricConfiguration metricConfiguration)
        {
            if (ModelState.IsValid)
            {
                db.MetricConfigurations.Add(metricConfiguration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KeyBusinessPartnerID = new SelectList(db.DimBusinessPartners, "id", "BusinessPartnerName", metricConfiguration.KeyBusinessPartnerID);
            ViewBag.KeySiteID = new SelectList(db.DimFacilities, "id", "SiteName", metricConfiguration.KeySiteID);
            ViewBag.KeyFFInstanceID = new SelectList(db.DimFFInstances, "id", "DataSourceName", metricConfiguration.KeyFFInstanceID);
            ViewBag.KeyMetricID = new SelectList(db.Metrics, "id", "MetricName", metricConfiguration.KeyMetricID);
            ViewBag.KeyMetricRoleTypeID = new SelectList(db.MetricRoleTypes, "id", "RoleName", metricConfiguration.KeyMetricRoleTypeID);
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
            ViewBag.KeyBusinessPartnerID = new SelectList(db.DimBusinessPartners, "id", "BusinessPartnerName", metricConfiguration.KeyBusinessPartnerID);
            ViewBag.KeySiteID = new SelectList(db.DimFacilities, "id", "SiteName", metricConfiguration.KeySiteID);
            ViewBag.KeyFFInstanceID = new SelectList(db.DimFFInstances, "id", "DataSourceName", metricConfiguration.KeyFFInstanceID);
            ViewBag.KeyMetricID = new SelectList(db.Metrics, "id", "MetricName", metricConfiguration.KeyMetricID);
            ViewBag.KeyMetricRoleTypeID = new SelectList(db.MetricRoleTypes, "id", "RoleName", metricConfiguration.KeyMetricRoleTypeID);
            return View(metricConfiguration);
        }

        // POST: MetricConfigurations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,KeyMetricID,KeyMetricRoleTypeID,KeySiteID,KeyBusinessPartnerID,KeyFFInstanceID,Goal,Red,Green,Alert,Alert_MasterDataChange,Alert_SystemErrors,MetricManager,MetricOwner,MetricManagerValidationStatus,MetricOwnerValidationStatus,Status")] MetricConfiguration metricConfiguration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(metricConfiguration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KeyBusinessPartnerID = new SelectList(db.DimBusinessPartners, "id", "BusinessPartnerName", metricConfiguration.KeyBusinessPartnerID);
            ViewBag.KeySiteID = new SelectList(db.DimFacilities, "id", "SiteName", metricConfiguration.KeySiteID);
            ViewBag.KeyFFInstanceID = new SelectList(db.DimFFInstances, "id", "DataSourceName", metricConfiguration.KeyFFInstanceID);
            ViewBag.KeyMetricID = new SelectList(db.Metrics, "id", "MetricName", metricConfiguration.KeyMetricID);
            ViewBag.KeyMetricRoleTypeID = new SelectList(db.MetricRoleTypes, "id", "RoleName", metricConfiguration.KeyMetricRoleTypeID);
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
