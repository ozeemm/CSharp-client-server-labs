using Microsoft.EntityFrameworkCore;

namespace Lab2._Relations
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using(var db = new AppContext())
            {
                await db.SeedDataAsync();

                // Курсы и студенты
                var coursesWithStudents = db.Courses
                    .Include(c => c.Enrollments)
                    .ThenInclude(e => e.Student)
                    .ToList();

                Console.WriteLine("Курсы и студенты:");
                foreach(var course in coursesWithStudents)
                {
                    Console.WriteLine($"Курс: {course.Title}");
                    foreach(var enrollment in course.Enrollments)
                    {
                        Console.WriteLine($"- Студент: {enrollment.Student.Fullname}");
                    }
                }
                Console.WriteLine();

                // Студенты и курсы
                var studentsAndCourses = db.Students
                    .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                    .ToList();

                Console.WriteLine("Студенты и курсы:");
                foreach(var student in studentsAndCourses)
                {
                    Console.WriteLine($"Студент: {student.Fullname}");
                    foreach(var enrollment in student.Enrollments)
                    {
                        Console.WriteLine($"- Курс: {enrollment.Course.Title}");
                    }
                }
                Console.WriteLine();

                // Оценки студента по имени "Иван"
                var ivanGrades = db.Grades
                    .Where(g => g.Student.Fullname.StartsWith("Иван "))
                    .Select(g => new
                    {
                        CourseName = g.Course.Title,
                        Score = g.Score
                    })
                    .ToList();

                Console.WriteLine("Оценки Ивана:");
                ivanGrades.ForEach(g => Console.WriteLine($"- {g.CourseName}: {g.Score}"));
                Console.WriteLine();

                // Средний балл по каждому курсу
                var averageScores = db.Grades
                    .GroupBy(g => g.Course.Title)
                    .Select(g => new
                    {
                        CourseName = g.Key,
                        AverageScore = g.Average(gr => gr.Score)
                    })
                    .ToList();

                Console.WriteLine("Средний балл по курсам:");
                averageScores.ForEach(s => Console.WriteLine($"- {s.CourseName}: {s.AverageScore}"));
                Console.WriteLine();
            }

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
