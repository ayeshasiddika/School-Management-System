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
    builder.EntitySet<Result>("Results");
    builder.EntitySet<Exam>("Exams"); 
    builder.EntitySet<ExamTerm>("ExamTerms"); 
    builder.EntitySet<ExamYear>("ExamYears"); 
    builder.EntitySet<Student>("Students"); 
    builder.EntitySet<Subject>("Subjects"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*")]
    public class ResultsController : ODataController
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: odata/Results
        [EnableQuery]
        public IQueryable<Result> GetResults()
        {
            return db.Results;
        }

        // GET: odata/Results(5)
        [EnableQuery]
        public SingleResult<Result> GetResult([FromODataUri] int key)
        {
            return SingleResult.Create(db.Results.Where(result => result.ResultId == key));
        }

        [Authorize]
        // PUT: odata/Results(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Result> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Result result = db.Results.Find(key);
            if (result == null)
            {
                return NotFound();
            }

            patch.Put(result);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(result);
        }

        [Authorize]
        // POST: odata/Results
        public IHttpActionResult Post(Result result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Results.Add(result);
            db.SaveChanges();

            return Created(result);
        }

        // PATCH: odata/Results(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Result> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Result result = db.Results.Find(key);
            if (result == null)
            {
                return NotFound();
            }

            patch.Patch(result);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(result);
        }

        [Authorize]
        // DELETE: odata/Results(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Result result = db.Results.Find(key);
            if (result == null)
            {
                return NotFound();
            }

            db.Results.Remove(result);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Results(5)/Exam
        [EnableQuery]
        public SingleResult<Exam> GetExam([FromODataUri] int key)
        {
            return SingleResult.Create(db.Results.Where(m => m.ResultId == key).Select(m => m.Exam));
        }

        // GET: odata/Results(5)/ExamTerm
        [EnableQuery]
        public SingleResult<ExamTerm> GetExamTerm([FromODataUri] int key)
        {
            return SingleResult.Create(db.Results.Where(m => m.ResultId == key).Select(m => m.ExamTerm));
        }

        // GET: odata/Results(5)/ExamYear
        [EnableQuery]
        public SingleResult<ExamYear> GetExamYear([FromODataUri] int key)
        {
            return SingleResult.Create(db.Results.Where(m => m.ResultId == key).Select(m => m.ExamYear));
        }

        // GET: odata/Results(5)/Student
        [EnableQuery]
        public SingleResult<Student> GetStudent([FromODataUri] int key)
        {
            return SingleResult.Create(db.Results.Where(m => m.ResultId == key).Select(m => m.Student));
        }

        // GET: odata/Results(5)/Subject
        [EnableQuery]
        public SingleResult<Subject> GetSubject([FromODataUri] int key)
        {
            return SingleResult.Create(db.Results.Where(m => m.ResultId == key).Select(m => m.Subject));
        }
        [HttpPost]
        //public IHttpActionResult ExResultsSave(ODataActionParameters parameters)
        //{
        //    IEnumerable<ResultVM> results = parameters["results"] as IEnumerable<ResultVM>;
        //    foreach (var r in results)
        //    {
        //        if (r.ResultId == 0)
        //        {
        //            db.Results.Add(new Result { ExamId = r.ExamId, StudentId = r.StudentId, Mark = r.Mark });
        //        }
        //        else
        //        {
        //            var result = db.Results.First(re => re.ResultId == r.ResultId);
        //            result.Mark = r.Mark;
        //            result.StudentId = r.StudentId;
        //        }
        //        db.SaveChanges();
        //    }
        //    return Ok(HttpStatusCode.NoContent);
        //}
        public IHttpActionResult ResultSave(ODataActionParameters parameters)
        {
            IEnumerable<ResultVM> results = parameters["results"] as IEnumerable<ResultVM>;
            foreach (var r in results)
            {
                if (r.ResultId == 0)
                {
                    //db.Results.Add(new Result { ExamId = r.ExamId, StudentId = r.StudentId, Mark = r.Mark });
                    db.Results.Add(new Result { ExamId = r.ExamId, StudentId = r.StudentId, Mark = r.Mark });
                }
                else
                {
                    var result = db.Results.First(re => re.ResultId == r.ResultId);
                    result.Mark = r.Mark;
                    result.StudentId = r.StudentId;
                }
                db.SaveChanges();
            }
            return Ok(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResultExists(int key)
        {
            return db.Results.Count(e => e.ResultId == key) > 0;
        }
    }
}
