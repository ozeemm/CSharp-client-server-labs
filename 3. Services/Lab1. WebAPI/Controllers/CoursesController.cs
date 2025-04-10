using Lab1._WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Lab1._WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private ApplicationContext db;

        public CoursesController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult GetCourses()
        {
            return new JsonResult(db.Courses.ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await db.Courses.FindAsync(id);
            if (course == null)
                return BadRequest();

            return new JsonResult(course);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(Course course)
        {
            await db.Courses.AddAsync(course);
            await db.SaveChangesAsync();

            return new JsonResult(course);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await db.Courses.FindAsync(id);
            if (course == null)
                return BadRequest();

            db.Courses.Remove(course);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, Course newCourse)
        {
            var course = await db.Courses.FindAsync(id);
            if (course == null)
                return BadRequest();

            course.CopyFrom(newCourse);
            db.Courses.Update(course);

            await db.SaveChangesAsync();

            return new JsonResult(course);
        }
    }
}
