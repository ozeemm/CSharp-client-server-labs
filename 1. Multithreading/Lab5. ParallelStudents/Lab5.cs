using System.Diagnostics;

namespace Lab5._ParallelStudents
{ 
    class LaunchParams
    {
        public string Name { get; set; }
        public Action? Method { get; set; }
        public Func<Task>? AsyncMethod { get; set; }
        public long Result { get; set; } = -1;
    }

    internal class Lab5
    {
        private static List<Grade> grades = new()
        {
            new Grade { StudentName = "Вася", Subject = "Математика", Score = 90 },
            new Grade { StudentName = "Вася", Subject = "Физика", Score = 85 },
            new Grade { StudentName = "Петя", Subject = "Математика", Score = 75 },
            new Grade { StudentName = "Петя", Subject = "Физика", Score = 80 },
            new Grade { StudentName = "Коля", Subject = "Математика", Score = 95 },
            new Grade { StudentName = "Коля", Subject = "Физика", Score = 90 }
        };

        private static List<string> students = new() { "Вася", "Петя", "Коля", "Иван" };

        private static List<LaunchParams> launchParams = new()
        {
            new LaunchParams{ Name = "Sync", Method = RunSync },
            new LaunchParams{ Name = "Tasks", AsyncMethod = RunAsync },
            new LaunchParams{ Name = "Threads", Method = RunThreads },
            new LaunchParams{ Name = "PLINQ", Method = RunPLINQ },
            new LaunchParams{ Name = "ParallelForEach", Method = RunParallelForEach },
        };

        static async Task Main(string[] args)
        {
            var watch = Stopwatch.StartNew();

            foreach(var param in launchParams)
            {
                Console.WriteLine($"\n=========== {param.Name} ===========");

                watch.Restart();
                if (param.Method != null)
                    param.Method();
                else if (param.AsyncMethod != null)
                    await param.AsyncMethod();
                else
                {
                    Console.WriteLine($"Не удалось выполнить {param.Name}");
                    continue;
                }

                watch.Stop();

                param.Result = watch.ElapsedMilliseconds;
                Console.WriteLine($"Выполнено за: {param.Result} мс");
            }

            var minTime = launchParams.Min(p => p.Result);
            var winner = launchParams.Find(p => p.Result == minTime);

            Console.WriteLine($"\nПобедитель: {winner.Name}");        }

        static void RunSync()
        {
            foreach (var student in students)
            {
                double averageScore = GradeSystem.CalculateAverageScore(grades, student);
                Console.WriteLine($"Для студента {student} средняя оценка: {averageScore}");
            }
        }

        static async Task RunAsync()
        {
            var tasks = students.Select(s => Task.Run(() => GradeSystem.CalculateAverageScore(grades, s)));
            var results = await Task.WhenAll(tasks);

            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"Для студента {students[i]} средняя оценка: {results[i]}");
            }
        }

        static void RunThreads()
        {
            var threads = new List<Thread>();
            foreach(var student in students)
            {
                var thread = new Thread(() =>
                {
                    var averageScore = GradeSystem.CalculateAverageScore(grades, student);
                    Console.WriteLine($"Для студента {student} средняя оценка: {averageScore}");
                });
                thread.Start();
                threads.Add(thread);
            }

            foreach(var thread in threads)
            {
                thread.Join();
            }
        }

        static void RunPLINQ()
        {
            students.AsParallel().ForAll(student =>
            {
                var averageScore = GradeSystem.CalculateAverageScore(grades, student);
                Console.WriteLine($"Для студента {student} средняя оценка: {averageScore}");
            });
        }

        static void RunParallelForEach()
        {
            Parallel.ForEach(students, student =>
            {
                var averageScore = GradeSystem.CalculateAverageScore(grades, student);
                Console.WriteLine($"Для студента {student} средняя оценка: {averageScore}");
            });
        }
    }
}
