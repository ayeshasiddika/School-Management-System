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
    builder.EntitySet<Subject>("Subjects");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*")]
    public class SubjectsController : ODataController
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: odata/Subjects
        [EnableQuery]
        public IQueryable<Subject> GetSubjects()
        {
            return db.Subjects;
        }

        // GET: odata/Subjects(5)
        [EnableQuery]
        public SingleResult<Subject> GetSubject([FromODataUri] int key)
        {
            return SingleResult.Create(db.Subjects.Where(subject => subject.SubjectId == key));
        }

        // PUT: odata/Subjects(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Subject> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Subject subject = db.Subjects.Find(key);
            if (subject == null)
            {
                return NotFound();
            }

            patch.Put(subject);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(subject);
        }

        // POST: odata/Subjects
        public IHttpActionResult Post(Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Subjects.Add(subject);
            db.SaveChanges();

            return Created(subject);
        }

        // PATCH: odata/Subjects(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Subject> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Subject subject = db.Subjects.Find(key);
            if (subject == null)
            {
                return NotFound();
            }

            patch.Patch(subject);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(subject);
        }

        // DELETE: odata/Subjects(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Subject subject = db.Subjects.Find(key);
            if (subject == null)
            {
                return NotFound();
            }

            db.Subjects.Remove(subject);
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

        private bool SubjectExists(int key)
        {
            return db.Subjects.Count(e => e.SubjectId == key) > 0;
        }
    }
}
