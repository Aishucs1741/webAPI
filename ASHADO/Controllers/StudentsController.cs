using ADOTest;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ASHADO.Controllers
{
    public class StudentsController : ApiController
    {
        [HttpGet]
        public student Get(int studentId)
        {
            using (WEBFORMEntities entities = new WEBFORMEntities())
            {
                return entities.students.FirstOrDefault(e => e.StudentId == studentId);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] student newStudent)
        {
            try
            {
                using (WEBFORMEntities entities = new WEBFORMEntities())
                {
                    entities.students.Add(newStudent);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, newStudent);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + newStudent.StudentId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpDelete]
        public HttpResponseMessage Delete(int studentId)
        {
            try
            {
                using (WEBFORMEntities entities = new WEBFORMEntities())
                {
                    var students = entities.students.FirstOrDefault(e => e.StudentId == studentId);
                    if (students == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Student with Id=" + studentId + "not found.");
                    }
                    else
                    {
                        entities.students.Remove(students);
                        entities.SaveChanges();
                        return Request.CreateErrorResponse(HttpStatusCode.OK, "DONE");
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put(int studentId, [FromBody] student updatedStudent)
        {
            try
            {
                using (WEBFORMEntities entities = new WEBFORMEntities())
                {
                    var existingStudent = entities.students.FirstOrDefault(e => e.StudentId == studentId);
                    if (existingStudent == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Student with Id=" + studentId + " not found.");
                    }
                    else
                    {
                        existingStudent.StudentName = updatedStudent.StudentName;
                        existingStudent.Age = updatedStudent.Age;
                        existingStudent.Address = updatedStudent.Address;
                        existingStudent.Phone = updatedStudent.Phone;

                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, existingStudent);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}



