using Lab1._WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab1._WebAPI
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.StudentId, e.CourseId });

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);
        }

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

                Enrollments.Add(new Enrollment { Student = student1, Course = course1 });
                Enrollments.Add(new Enrollment { Student = student2, Course = course2 });
                SaveChanges();

                Grades.Add(new Grade { Student = student1, Course = course1, Score = 4 });
                Grades.Add(new Grade { Student = student2, Course = course2, Score = 3 });
                SaveChanges();
            }
        }
    }
}
