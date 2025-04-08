using Lab2._Relations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Lab2._Relations
{
    public class AppContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        public AppContext() : base()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();

            optionsBuilder.LogTo(WriteColoredLine);
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlite(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>().HasKey(e => new { e.StudentId, e.CourseId });

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);
        }

        public async Task SeedDataAsync()
        {
            if (!Students.Any() && !Courses.Any() && !Grades.Any())
            {
                var student1 = new Student { Fullname = "Вася Иванов", Age = 20, Address = "Общежитие №1" };
                var student2 = new Student { Fullname = "Иван Васильев", Age = 19, Address = "Общежитие №2" };
                var student3 = new Student { Fullname = "Пётр Петров", Age = 22, Address = "Общежитие №1" };

                await Students.AddAsync(student1);
                await Students.AddAsync(student2);
                await Students.AddAsync(student3);
                await SaveChangesAsync();

                var course1 = new Course { Title = "Математика" };
                var course2 = new Course { Title = "Физика" };

                await Courses.AddAsync(course1);
                await Courses.AddAsync(course2);
                await SaveChangesAsync();

                await Grades.AddAsync(new Grade { Student = student1, Course = course1, Score = 4 });
                await Grades.AddAsync(new Grade { Student = student2, Course = course2, Score = 3 });

                await SaveChangesAsync();

                await Enrollments.AddAsync(new Enrollment { Student = student1, Course = course1 });
                await Enrollments.AddAsync(new Enrollment { Student = student2, Course = course2 });
                await SaveChangesAsync();
            }
        }

        private static void WriteColoredLine(string? text)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
