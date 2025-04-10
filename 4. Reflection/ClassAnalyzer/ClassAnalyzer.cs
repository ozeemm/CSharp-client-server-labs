using System.Reflection;
using SomeLibrary.Models;

namespace ConsoleApp
{
    internal class ClassAnalyzer
    {
        static void Main()
        {
            List<Type> types = new() { typeof(TwoNumbers), typeof(Person), typeof(SocketServerAsync), typeof(Student), typeof(Grade), typeof(Course), typeof(Enrollment) };

            foreach(var type in types)
            {
                Console.WriteLine($"\n========= {type.Name} =========");
                PrintTypeInfo(type);
            }
        }

        static void PrintTypeInfo(Type myType)
        {
            Console.WriteLine($"Type: {myType}");
            Console.WriteLine($"Name: {myType.Name}");       
            Console.WriteLine($"Full Name: {myType.FullName}");
            Console.WriteLine($"Namespace: {myType.Namespace}");
            Console.WriteLine($"Is struct: {myType.IsValueType}");
            Console.WriteLine($"Is class: {myType.IsClass}");
            Console.WriteLine($"\nMembers:");

            foreach (MemberInfo member in myType.GetMembers())
            {
                Console.WriteLine($"{member.DeclaringType} {member.MemberType} {member.Name}");
            }
        }
    }
}
