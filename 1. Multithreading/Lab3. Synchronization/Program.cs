namespace Lab3._Synchronization
{
    internal class Program
    {
        private static double value = 0.5;
        private static Mutex mutex = new();

        private static int itersCount = 10;
        static void Main(string[] args)
        {
            var cosThread = new Thread(() => Cos(itersCount)) { Name="Cos-Thread" };
            var arccosThread = new Thread(() => Arccos(itersCount)) { Name="Arccos-Thread" };

            cosThread.Start();
            arccosThread.Start();

            cosThread.Join();
            arccosThread.Join();

            Console.WriteLine("Done!");
        }

        static void Cos(int itersCount)
        {
            for (int i = 0; i < itersCount; i++)
            { 
                mutex.WaitOne();
                value = Math.Cos(value);
                Console.WriteLine($"[{Thread.CurrentThread.Name}]: {value}");
                mutex.ReleaseMutex();
            }
        }

        static void Arccos(int itersCount)
        {
            for (int i = 0; i < itersCount; i++)
            {
                mutex.WaitOne();
                value = Math.Acos(value);
                Console.WriteLine($"[{Thread.CurrentThread.Name}]: {value}");
                mutex.ReleaseMutex();
            }
        }
    }
}
