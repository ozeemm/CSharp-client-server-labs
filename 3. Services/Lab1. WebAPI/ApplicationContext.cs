using Lab1._WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab1._WebAPI
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public void SeedData()
        {
            if (!Students.Any() && !Courses.Any() && !Grades.Any())
            {
                var student1 = new Student { Fullname = "Вася Иванов", Age = 20 };
                var student2 = new Student { Fullname = "Иван Васильев", Age = 19 };
                var student3 = new Student { Fullname = "Пётр Петров", Age = 22 };

                Students.Add(student1);
                Students.Add(student2);
                Students.Add(student3);
                SaveChanges();

                var course1 = new Course { Name = "Математика" };
                var course2 = new Course { Name = "Физика" };

                Courses.Add(course1);
                Courses.Add(course2);
                SaveChanges();

                Grades.Add(new Grade { Student = student1, Course = course1, Score = 4 });
                Grades.Add(new Grade { Student = student2, Course = course2, Score = 3 });
                SaveChanges();
            }
        }
    }
}
