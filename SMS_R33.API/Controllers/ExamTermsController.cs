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
    builder.EntitySet<ExamTerm>("ExamTerms");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*")]
    public class ExamTermsController : ODataController
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: odata/ExamTerms
        [EnableQuery]
        public IQueryable<ExamTerm> GetExamTerms()
        {
            return db.ExamTerms;
        }

        // GET: odata/ExamTerms(5)
        [EnableQuery]
        public SingleResult<ExamTerm> GetExamTerm([FromODataUri] int key)
        {
            return SingleResult.Create(db.ExamTerms.Where(examTerm => examTerm.ExamTermId == key));
        }

        // PUT: odata/ExamTerms(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<ExamTerm> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExamTerm examTerm = db.ExamTerms.Find(key);
            if (examTerm == null)
            {
                return NotFound();
            }

            patch.Put(examTerm);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamTermExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(examTerm);
        }

        // POST: odata/ExamTerms
        public IHttpActionResult Post(ExamTerm examTerm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExamTerms.Add(examTerm);
            db.SaveChanges();

            return Created(examTerm);
        }

        // PATCH: odata/ExamTerms(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<ExamTerm> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExamTerm examTerm = db.ExamTerms.Find(key);
            if (examTerm == null)
            {
                return NotFound();
            }

            patch.Patch(examTerm);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamTermExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(examTerm);
        }

        // DELETE: odata/ExamTerms(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            ExamTerm examTerm = db.ExamTerms.Find(key);
            if (examTerm == null)
            {
                return NotFound();
            }

            db.ExamTerms.Remove(examTerm);
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

        private bool ExamTermExists(int key)
        {
            return db.ExamTerms.Count(e => e.ExamTermId == key) > 0;
        }
    }
}
