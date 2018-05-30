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
    builder.EntitySet<Teacher>("Teachers");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*")]
    public class TeachersController : ODataController
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: odata/Teachers
        [EnableQuery]
        public IQueryable<Teacher> GetTeachers()
        {
            return db.Teachers;
        }

        // GET: odata/Teachers(5)
        [EnableQuery]
        public SingleResult<Teacher> GetTeacher([FromODataUri] int key)
        {
            return SingleResult.Create(db.Teachers.Where(teacher => teacher.TeacherId == key));
        }

        // PUT: odata/Teachers(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Teacher> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Teacher teacher = db.Teachers.Find(key);
            if (teacher == null)
            {
                return NotFound();
            }

            patch.Put(teacher);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(teacher);
        }

        // POST: odata/Teachers
        public IHttpActionResult Post(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teachers.Add(teacher);
            db.SaveChanges();

            return Created(teacher);
        }

        // PATCH: odata/Teachers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Teacher> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Teacher teacher = db.Teachers.Find(key);
            if (teacher == null)
            {
                return NotFound();
            }

            patch.Patch(teacher);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(teacher);
        }

        // DELETE: odata/Teachers(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Teacher teacher = db.Teachers.Find(key);
            if (teacher == null)
            {
                return NotFound();
            }

            db.Teachers.Remove(teacher);
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

        private bool TeacherExists(int key)
        {
            return db.Teachers.Count(e => e.TeacherId == key) > 0;
        }
    }

}
