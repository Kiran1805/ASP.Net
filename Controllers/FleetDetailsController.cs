using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FleetManagementSystemApiWeb.Models;

namespace FleetManagementSystemApiWeb.Controllers
{
    public class FleetDetailsController : ApiController
    {
        private FleetManagementDBEntities db = new FleetManagementDBEntities();

        // GET: api/FleetDetails
        public IQueryable<FleetDetail> GetFleetDetails()
        {
            return db.FleetDetails;
        }

        // GET: api/FleetDetails/5
        [ResponseType(typeof(FleetDetail))]
        public IHttpActionResult GetFleetDetail(int id)
        {
            FleetDetail fleetDetail = db.FleetDetails.Find(id);
            if (fleetDetail == null)
            {
                return NotFound();
            }

            return Ok(fleetDetail);
        }

        // PUT: api/FleetDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFleetDetail(int id, FleetDetail fleetDetail)
        {
            fleetDetail = new FleetDetail { Id = id, ModelName = "Mercedes Benz", VehicleNumber = "KA21JH365", DriverName = "Frank", DistanceTravelled = 29, SufficientFuel = false };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fleetDetail.Id)
            {
                return BadRequest();
            }

            db.Entry(fleetDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FleetDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/FleetDetails
        [ResponseType(typeof(FleetDetail))]
        public IHttpActionResult PostFleetDetail(FleetDetail fleetDetail)
        {
            fleetDetail = new FleetDetail { Id = 4, ModelName = "Wolksvagen", VehicleNumber = "KE11SD1649", DriverName = "Jumbo", DistanceTravelled = 49, SufficientFuel = false };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FleetDetails.Add(fleetDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FleetDetailExists(fleetDetail.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = fleetDetail.Id }, fleetDetail);
        }

        // DELETE: api/FleetDetails/5
        [ResponseType(typeof(FleetDetail))]
        public IHttpActionResult DeleteFleetDetail(int id)
        {
            FleetDetail fleetDetail = db.FleetDetails.Find(id);
            if (fleetDetail == null)
            {
                return NotFound();
            }

            db.FleetDetails.Remove(fleetDetail);
            db.SaveChanges();

            return Ok(fleetDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FleetDetailExists(int id)
        {
            return db.FleetDetails.Count(e => e.Id == id) > 0;
        }
    }
}