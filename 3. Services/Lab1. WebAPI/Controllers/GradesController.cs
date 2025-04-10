﻿using Lab1._WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab1._WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GradesController : ControllerBase
    {
        private ApplicationContext db;

        public GradesController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult GetGrades()
        {
            return new JsonResult(db.Grades.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> AddGrade(Grade grade)
        {
            await db.Grades.AddAsync(grade);
            await db.SaveChangesAsync();

            return new JsonResult(grade);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var grade = db.Grades.Where(g => g.Id == id).FirstOrDefault();
            if (grade == null)
                return BadRequest();

            db.Grades.Remove(grade);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateGrade(int id, Grade newGrade)
        {
            var grade = db.Grades.Where(c => c.Id == id).FirstOrDefault();
            if (grade == null)
                return BadRequest();

            grade.CopyFrom(newGrade);
            db.Grades.Update(grade);

            await db.SaveChangesAsync();

            return new JsonResult(grade);
        }
    }
}