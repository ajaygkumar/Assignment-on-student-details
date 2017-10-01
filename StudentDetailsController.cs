using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI;

namespace WebAPI.Controllers
{
    public class StudentDetailsController : ApiController
    {
        private StudentEntities db = new StudentEntities();

        // GET: api/StudentDetails
        public IQueryable<StudentDetail> GetStudentDetails()
        {
            return db.StudentDetails;
        }

        // GET: api/StudentDetails/5
        [ResponseType(typeof(StudentDetail))]
        public async Task<IHttpActionResult> GetStudentDetail(int id)
        {
            StudentDetail studentDetail = await db.StudentDetails.FindAsync(id);
            if (studentDetail == null)
            {
                return NotFound();
            }

            return Ok(studentDetail);
        }

        // PUT: api/StudentDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStudentDetail(int id, StudentDetail studentDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentDetail.Id)
            {
                return BadRequest();
            }

            db.Entry(studentDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentDetailExists(id))
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

        // POST: api/StudentDetails
        [ResponseType(typeof(StudentDetail))]
        public async Task<IHttpActionResult> PostStudentDetail(StudentDetail studentDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentDetails.Add(studentDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentDetailExists(studentDetail.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = studentDetail.Id }, studentDetail);
        }

        // DELETE: api/StudentDetails/5
        [ResponseType(typeof(StudentDetail))]
        public async Task<IHttpActionResult> DeleteStudentDetail(int id)
        {
            StudentDetail studentDetail = await db.StudentDetails.FindAsync(id);
            if (studentDetail == null)
            {
                return NotFound();
            }

            db.StudentDetails.Remove(studentDetail);
            await db.SaveChangesAsync();

            return Ok(studentDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentDetailExists(int id)
        {
            return db.StudentDetails.Count(e => e.Id == id) > 0;
        }
    }
}