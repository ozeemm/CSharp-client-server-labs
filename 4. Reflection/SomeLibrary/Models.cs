using System.Net.Sockets;

namespace SomeLibrary.Models
{
    public struct TwoNumbers
    {
        public int a;
        public int b;

        public TwoNumbers(int a, int b)
        {
            this.a = a;
            this.b = b;
        }
    }
    public class Person
    {
        string name;
        public int Age { get; set; }

        public Person(string name) => this.name = name;
        public Person(string name, int age)
        {
            this.name = name;
            this.Age = age;
        }
        public void Print() => Console.WriteLine($"Name: {name} Age: {Age}");
    }

    public class SocketServerAsync
    {
        private const int port = 11000;
        private int requestCount = 0;

        public async Task Start()
        {
            await Task.Delay(50);
            Console.WriteLine("Запускаю сервер");
        }

        public async Task HandleClientAsync(Socket clientSocket)
        {
            await Task.Delay(50);
            Console.WriteLine("Отправляю запрос");
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Grade>? Grades { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
    public class Grade
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }

        public int CourseId { get; set; }
        public virtual Course? Course { get; set; }

        public decimal Score { get; set; }
    }
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Grade>? Grades { get; set; } = new List<Grade>();
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
    public class Enrollment
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }

        public int CourseId { get; set; }
        public virtual Course? Course { get; set; }
    }

}
