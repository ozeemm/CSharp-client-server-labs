using Lab1._WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab1._WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private ApplicationContext db;

        public StudentsController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return new JsonResult(db.Students.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            await db.Students.AddAsync(student);
            await db.SaveChangesAsync();

            return new JsonResult(student);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = db.Students.Where(s => s.Id == id).FirstOrDefault();
            if (student == null)
                return BadRequest();

            db.Students.Remove(student);
            await db.SaveChangesAsync();
            
            return Ok();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateStudent(int id, Student newStudent)
        {
            var student = db.Students.Where(s => s.Id == id).FirstOrDefault();
            if (student == null)
                return BadRequest();

            student.CopyFrom(newStudent);
            db.Students.Update(student);

            await db.SaveChangesAsync();

            return new JsonResult(student);
        }
    }
}
