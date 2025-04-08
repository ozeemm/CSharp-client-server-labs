using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Lab1._SimpleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using(var db = new AppContext())
            {
                var mathCourse = new Course { Title = "Математика", Duration = 90, Description = "Тут про цифры" };
                var russianCourse = new Course { Title = "Русский язык", Duration = 60, Description = "Тут про буквы" };
                var historyCourse = new Course { Title = "История", Duration = 30 };

                // Добавление
                await db.Courses.AddAsync(mathCourse);
                await db.Courses.AddAsync(russianCourse);
                await db.Courses.AddAsync(historyCourse);
                await db.SaveChangesAsync();
                Console.WriteLine("Добавлены новые курсы");

                PrintAllCourses(db);

                // Изменение
                var courseToUpdate = db.Courses.OrderBy(c => c.Id).Last();
                courseToUpdate.Title = "Обществознание";
                await db.SaveChangesAsync();
                Console.WriteLine("Данные изменены");

                PrintAllCourses(db);

                // Удаление
                foreach (var course in db.Courses)
                {
                    db.Courses.Remove(course);
                }
                await db.SaveChangesAsync();
                Console.WriteLine("Курсы удалены");

                PrintAllCourses(db);
            }

            Console.WriteLine("Done!");
        }

        static void PrintAllCourses(AppContext db)
        {
            var coursesCount = db.Courses.Count();
            if (coursesCount > 0)
            {
                Console.WriteLine($"Список курсов ({db.Courses.Count()}):");
                db.Courses.ToList().ForEach(course => Console.WriteLine(course));
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Курсов нет");
            }
        }
    }
}
