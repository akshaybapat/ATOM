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
    public class BuildingController : Controller
    {
        private FFCube2Entities db = new FFCube2Entities();

        // GET: /Building/
        public ActionResult Index()
        {
            var dimbuildings = db.DimBuildings.Include(d => d.DimFacility);
            return View(dimbuildings.ToList());
        }

        // GET: /Building/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DimBuilding dimbuilding = db.DimBuildings.Find(id);
            if (dimbuilding == null)
            {
                return HttpNotFound();
            }
            return View(dimbuilding);
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
