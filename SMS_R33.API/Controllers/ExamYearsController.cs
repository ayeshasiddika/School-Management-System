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
    builder.EntitySet<ExamYear>("ExamYears");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*")]
    public class ExamYearsController : ODataController
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: odata/ExamYears
        [EnableQuery]
        public IQueryable<ExamYear> GetExamYears()
        {
            return db.ExamYears;
        }

        // GET: odata/ExamYears(5)
        [EnableQuery]
        public SingleResult<ExamYear> GetExamYear([FromODataUri] int key)
        {
            return SingleResult.Create(db.ExamYears.Where(examYear => examYear.ExamYearId == key));
        }

        // PUT: odata/ExamYears(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<ExamYear> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExamYear examYear = db.ExamYears.Find(key);
            if (examYear == null)
            {
                return NotFound();
            }

            patch.Put(examYear);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamYearExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(examYear);
        }

        // POST: odata/ExamYears
        public IHttpActionResult Post(ExamYear examYear)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExamYears.Add(examYear);
            db.SaveChanges();

            return Created(examYear);
        }

        // PATCH: odata/ExamYears(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<ExamYear> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExamYear examYear = db.ExamYears.Find(key);
            if (examYear == null)
            {
                return NotFound();
            }

            patch.Patch(examYear);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamYearExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(examYear);
        }

        // DELETE: odata/ExamYears(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            ExamYear examYear = db.ExamYears.Find(key);
            if (examYear == null)
            {
                return NotFound();
            }

            db.ExamYears.Remove(examYear);
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

        private bool ExamYearExists(int key)
        {
            return db.ExamYears.Count(e => e.ExamYearId == key) > 0;
        }
    }
}
