using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using SMS_R33.API.Models;
using System.Web.Http.Cors;

namespace SMS_R33.API.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using SMS_R33.API.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Admission>("Admissions");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*")]
    public class AdmissionsController : ODataController
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: odata/Admissions
        [EnableQuery]
        public IQueryable<Admission> GetAdmissions()
        {
            return db.Admissions;
        }

        // GET: odata/Admissions(5)
        [EnableQuery]
        public SingleResult<Admission> GetAdmission([FromODataUri] int key)
        {
            return SingleResult.Create(db.Admissions.Where(admission => admission.AdmissionId == key));
        }

        // PUT: odata/Admissions(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Admission> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Admission admission = db.Admissions.Find(key);
            if (admission == null)
            {
                return NotFound();
            }

            patch.Put(admission);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdmissionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(admission);
        }

        // POST: odata/Admissions
        public IHttpActionResult Post(Admission admission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Admissions.Add(admission);
            db.SaveChanges();

            return Created(admission);
        }

        // PATCH: odata/Admissions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Admission> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Admission admission = db.Admissions.Find(key);
            if (admission == null)
            {
                return NotFound();
            }

            patch.Patch(admission);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdmissionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(admission);
        }

        // DELETE: odata/Admissions(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Admission admission = db.Admissions.Find(key);
            if (admission == null)
            {
                return NotFound();
            }

            db.Admissions.Remove(admission);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdmissionExists(int key)
        {
            return db.Admissions.Count(e => e.AdmissionId == key) > 0;
        }
    }
}
