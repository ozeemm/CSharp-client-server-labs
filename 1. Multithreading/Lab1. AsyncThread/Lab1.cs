namespace Lab1._AsyncThread
{
    internal struct TwoNumbers
    {
        public int a;
        public int b;

        public TwoNumbers(int a, int b)
        {
            this.a = a;
            this.b = b;
        }
    }

    internal class Lab1
    {
        static void Main(string[] args)
        {
            // ParametrizedThreadStart
            Thread thread1 = new Thread((numbers) => PrintNumbers(numbers));
            Thread thread2 = new Thread((numbers) => PrintNumbers(numbers));

            // Имена потоков
            thread1.Name = "Thread-1";
            thread2.Name = "Thread-2";
           
            // Передача параметра
            thread1.Start(new TwoNumbers(1, 10));
            thread2.Start(new TwoNumbers(11, 20));

            // Ожидаем завершения потоков
            thread1.Join();
            thread2.Join();

            Console.WriteLine("Done!");
        }

        static void PrintNumbers(object? obj)
        {
            // Проверка типа параметра
            if(obj is TwoNumbers numbers)
            {
                for (int i = numbers.a; i <= numbers.b; i++)
                {
                    Console.WriteLine($"[{Thread.CurrentThread.Name}]: {i}");
                }
            }
            else
                throw new ArgumentException();
        }
    }
}