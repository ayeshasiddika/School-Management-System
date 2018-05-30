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
    builder.EntitySet<Student>("Students");
    builder.EntitySet<Class>("Classes"); 
    builder.EntitySet<Result>("Results"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    

    [EnableCors("*", "*", "*")]
    public class StudentsController : ODataController
    {
        private SchoolDbContext db = new SchoolDbContext();

        // GET: odata/Students
        [EnableQuery]
        public IQueryable<Student> GetStudents()
        {
            return db.Students;
        }

        // GET: odata/Students(5)
        [EnableQuery]
        public SingleResult<Student> GetStudent([FromODataUri] int key)
        {
            return SingleResult.Create(db.Students.Where(student => student.StudentId == key));
        }

        [Authorize]
        // PUT: odata/Students(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Student> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Student student = db.Students.Find(key);
            if (student == null)
            {
                return NotFound();
            }

            patch.Put(student);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(student);
        }

        [Authorize]
        // POST: odata/Students
        public IHttpActionResult Post(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Students.Add(student);
            db.SaveChanges();

            return Created(student);
        }

        // PATCH: odata/Students(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Student> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Student student = db.Students.Find(key);
            if (student == null)
            {
                return NotFound();
            }

            patch.Patch(student);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(student);
        }

        [Authorize]
        // DELETE: odata/Students(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Student student = db.Students.Find(key);
            if (student == null)
            {
                return NotFound();
            }

            db.Students.Remove(student);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Students(5)/Class
        [EnableQuery]
        public SingleResult<Class> GetClass([FromODataUri] int key)
        {
            return SingleResult.Create(db.Students.Where(m => m.StudentId == key).Select(m => m.Class));
        }

        // GET: odata/Students(5)/Results
        [EnableQuery]
        public IQueryable<Result> GetResults([FromODataUri] int key)
        {
            return db.Students.Where(m => m.StudentId == key).SelectMany(m => m.Results);
        }
        //For StudentMark View Model

        [HttpPost]

        public IList<StudentMarkVM> GetStudentMark(ODataActionParameters parameters)
        {
            int cid = (int)parameters["ClassId"];
            int eid = (int)parameters["ExamId"];
            var subid = db.Exams.First(x => x.ExamId == eid).SubjectId;


            List<StudentMarkVM> Smlist = new List<StudentMarkVM>();
            var students = db.Students.Where(x => x.ClassId == cid);
            foreach (var st in students)
            {
                StudentMarkVM mv = new StudentMarkVM { StudentId = st.StudentId, StudentName = st.StudentName, ExamId = eid, SubjectId = subid, ClassId = cid };
                var m = db.Results.FirstOrDefault(x => x.ExamId == eid && x.StudentId == st.StudentId);
                if (m != null)
                {
                    mv.Mark = m.Mark;
                    mv.ResultId = m.ResultId;
                }
                Smlist.Add(mv);

            }
            return Smlist;
        }
        [HttpPost]
        public IHttpActionResult NextRoll(ODataActionParameters parameters)
        {
            int clsid = (int)parameters["ClassId"];
            var classRolls = db.Students.Where(x => x.ClassId == clsid).Select(s => s.ClassRoll).ToList();
            var LRoll = classRolls.Max();
            int NRoll = 0;
            if (LRoll == null)
            {
                if (clsid == 1)
                {
                    NRoll = 60001;
                    return Ok(NRoll);
                }
                else if (clsid == 2)
                {
                    NRoll = 70001;
                    return Ok(NRoll);
                }
                else if (clsid == 3)
                {
                    NRoll = 80001;
                    return Ok(NRoll);
                }
                else if (clsid == 4)
                {
                    NRoll = 90001;
                    return Ok(NRoll);
                }
                else if (clsid == 5)
                {
                    NRoll = 100001;
                    return Ok(NRoll);
                }

            }
            else
            {
                NRoll = LRoll + 1;
            }
            return Ok(NRoll);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(int key)
        {
            return db.Students.Count(e => e.StudentId == key) > 0;
        }
    }
}
