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
    builder.EntitySet<Class>("Classes");
    builder.EntitySet<Student>("Students"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
   [EnableCors("*", "*", "*")]
    public class ClassesController : ODataController
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: odata/Classes
        [EnableQuery]
        public IQueryable<Class> GetClasses()
        {
            return db.Classes;
        }

        // GET: odata/Classes(5)
        [EnableQuery]
        public SingleResult<Class> GetClass([FromODataUri] int key)
        {
            return SingleResult.Create(db.Classes.Where(@class => @class.ClassId == key));
        }

        // PUT: odata/Classes(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Class> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Class @class = db.Classes.Find(key);
            if (@class == null)
            {
                return NotFound();
            }

            patch.Put(@class);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(@class);
        }

        // POST: odata/Classes
        public IHttpActionResult Post(Class @class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Classes.Add(@class);
            db.SaveChanges();

            return Created(@class);
        }

        // PATCH: odata/Classes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Class> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Class @class = db.Classes.Find(key);
            if (@class == null)
            {
                return NotFound();
            }

            patch.Patch(@class);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(@class);
        }

        // DELETE: odata/Classes(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Class @class = db.Classes.Find(key);
            if (@class == null)
            {
                return NotFound();
            }

            db.Classes.Remove(@class);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Classes(5)/Students
        [EnableQuery]
        public IQueryable<Student> GetStudents([FromODataUri] int key)
        {
            return db.Classes.Where(m => m.ClassId == key).SelectMany(m => m.Students);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClassExists(int key)
        {
            return db.Classes.Count(e => e.ClassId == key) > 0;
        }
    }
}
