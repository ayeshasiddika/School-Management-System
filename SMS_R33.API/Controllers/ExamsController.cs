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
    builder.EntitySet<Exam>("Exams");
    builder.EntitySet<Class>("Classes"); 
    builder.EntitySet<ExamTerm>("ExamTerms"); 
    builder.EntitySet<ExamYear>("ExamYears"); 
    builder.EntitySet<Subject>("Subjects"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*")]
    public class ExamsController : ODataController
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: odata/Exams
        [EnableQuery]
        public IQueryable<Exam> GetExams()
        {
            return db.Exams;
        }

        // GET: odata/Exams(5)
        [EnableQuery]
        public SingleResult<Exam> GetExam([FromODataUri] int key)
        {
            return SingleResult.Create(db.Exams.Where(exam => exam.ExamId == key));
        }

        // PUT: odata/Exams(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Exam> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Exam exam = db.Exams.Find(key);
            if (exam == null)
            {
                return NotFound();
            }

            patch.Put(exam);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(exam);
        }

        // POST: odata/Exams
        public IHttpActionResult Post(Exam exam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Exams.Add(exam);
            db.SaveChanges();

            return Created(exam);
        }

        // PATCH: odata/Exams(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Exam> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Exam exam = db.Exams.Find(key);
            if (exam == null)
            {
                return NotFound();
            }

            patch.Patch(exam);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(exam);
        }

        // DELETE: odata/Exams(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Exam exam = db.Exams.Find(key);
            if (exam == null)
            {
                return NotFound();
            }

            db.Exams.Remove(exam);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Exams(5)/Class
        [EnableQuery]
        public SingleResult<Class> GetClass([FromODataUri] int key)
        {
            return SingleResult.Create(db.Exams.Where(m => m.ExamId == key).Select(m => m.Class));
        }

        // GET: odata/Exams(5)/ExamTerm
        [EnableQuery]
        public SingleResult<ExamTerm> GetExamTerm([FromODataUri] int key)
        {
            return SingleResult.Create(db.Exams.Where(m => m.ExamId == key).Select(m => m.ExamTerm));
        }

        // GET: odata/Exams(5)/ExamYear
        [EnableQuery]
        public SingleResult<ExamYear> GetExamYear([FromODataUri] int key)
        {
            return SingleResult.Create(db.Exams.Where(m => m.ExamId == key).Select(m => m.ExamYear));
        }

        // GET: odata/Exams(5)/Subject
        [EnableQuery]
        public SingleResult<Subject> GetSubject([FromODataUri] int key)
        {
            return SingleResult.Create(db.Exams.Where(m => m.ExamId == key).Select(m => m.Subject));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExamExists(int key)
        {
            return db.Exams.Count(e => e.ExamId == key) > 0;
        }
    }
}
