using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace AtomConfiguratorModel.Models
{
    public class DimStationTypesController : Controller
    {

        private FFCube2Entities db = new FFCube2Entities();

        // GET: DimStationTypes
        public ActionResult Index()
        {
            return View();
        }

        //POST: DimStationTypes/Update
        [HttpPost, ActionName("Update")]
        public HttpResponseMessage Update(BucketedStnTypesModel bucketstationtypes)
        {
            var Bucket = db.DimBuckets.Where(x => x.BucketName.Equals(bucketstationtypes.bucketname)).First();


            if (bucketstationtypes.orderedstationtypes != null)
            {
                var sequence = 0;

                foreach (string stationtype in bucketstationtypes.orderedstationtypes)
                {
                    System.Diagnostics.Debug.Write(stationtype + '\n');

                    var stnTypeRow = db.DimStationTypes.Where(x => x.StationTypeName.Equals(stationtype)).First();


                    stnTypeRow.KeyBucket = Bucket.id;

                    stnTypeRow.Sequence = sequence;

                    //System.Diagnostics.Debug.Write(ffRow.KeyModule);


                    db.SaveChanges();

                    System.Diagnostics.Debug.Write("Ordered Bucket List Saved" + '\n');

                    sequence++;

                }

                sequence = 0;



            }


            if (bucketstationtypes.nonbucketedstationtypes != null)
            {


                foreach (string stationtype in bucketstationtypes.nonbucketedstationtypes)
                {
                    System.Diagnostics.Debug.Write(stationtype + '\n');

                    var stnTypeRow = db.DimStationTypes.Where(x => x.StationTypeName.Equals(stationtype)).First();

                    if(stnTypeRow.KeyBucket.HasValue)
                    stnTypeRow.KeyBucket = null;

                    db.SaveChanges();

                    System.Diagnostics.Debug.Write("Non Bucketed List Saved" + '\n');

                  

                }


            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }

}