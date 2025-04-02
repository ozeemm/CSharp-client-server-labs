using System.Net.Http.Headers;

namespace Lab2._Synchronization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunThreadsWithJoin();
            //RunThreadsWithMonitor(false);
            //RunThreadsWithAutoResetEvent(false);
            //RunThreadsWithMutex(false);
            //RunThreadsWithSemaphore(false);

            Console.WriteLine("Done!");
        }

        static void RunThreadsWithJoin()
        {
            Console.WriteLine("Running with join");

            var thread1 = new Thread(PrintNumbers) { Name = "Thread-1" };
            var thread2 = new Thread(PrintNumbers) { Name = "Thread-2" };

            thread1.Start();
            thread1.Join();
            
            thread2.Start();
            thread2.Join();
        }

        static void RunThreadsWithMonitor(bool swapThreadsWithSleep = false)
        {
            Console.WriteLine("Running with monitor and lock");

            var locker = new object();
            var isWorkDone = true;

            var thread1 = new Thread(() => PrintNumbers(locker, ref isWorkDone)) { Name = "Thread-1" };
            var thread2 = new Thread(() => PrintNumbers(locker, ref isWorkDone)) { Name = "Thread-2" };

            RunThreads(thread1, thread2, swapThreadsWithSleep);
        }

        static void RunThreadsWithAutoResetEvent(bool swapThreadsWithSleep = false)
        {
            Console.WriteLine("Running with AutoResetEvent");

            var autoResetEvent = new AutoResetEvent(true);

            var thread1 = new Thread(() => PrintNumbers(autoResetEvent)) { Name = "Thread-1" };
            var thread2 = new Thread(() => PrintNumbers(autoResetEvent)) { Name = "Thread-2" };

            RunThreads(thread1, thread2, swapThreadsWithSleep);
        }

        static void RunThreadsWithMutex(bool swapThreadsWithSleep = false)
        {
            Console.WriteLine("Running with mutex");

            var mutex = new Mutex();

            var thread1 = new Thread(() => PrintNumbers(mutex)) { Name = "Thread-1" };
            var thread2 = new Thread(() => PrintNumbers(mutex)) { Name = "Thread-2" };

            RunThreads(thread1, thread2, swapThreadsWithSleep);
        }

        static void RunThreadsWithSemaphore(bool swapThreadsWithSleep = false)
        {
            Console.WriteLine("Running with semaphore");

            var semaphore = new Semaphore(1, 2);

            var thread1 = new Thread(() => PrintNumbers(semaphore)) { Name = "Thread-1" };
            var thread2 = new Thread(() => PrintNumbers(semaphore)) { Name = "Thread-2" };

            RunThreads(thread1, thread2, swapThreadsWithSleep);
        }

        static void RunThreads(Thread thread1, Thread thread2, bool swapThreadsWithSleep = false)
        {
            if (swapThreadsWithSleep)
            {
                thread2.Start();
                Thread.Sleep(1000);
                thread1.Start();
            }
            else
            {
                thread1.Start();
                thread2.Start();
            }

            thread1.Join();
            thread2.Join();
        }

        static void PrintNumbers()
        {
            for(int i = 1; i <= 100; i++)
            {
                Console.WriteLine($"[{Thread.CurrentThread.Name}]: {i}");
            }
        }

        static void PrintNumbers(object locker, ref bool isWorkDone)
        {
            lock (locker)
            {
                while (!isWorkDone)
                {
                    Monitor.Wait(locker);
                }
                isWorkDone = false;
                PrintNumbers();
                Monitor.Pulse(locker);
                isWorkDone = true;
            }
        }

        static void PrintNumbers(AutoResetEvent autoResetEvent)
        {
            autoResetEvent.WaitOne();
            PrintNumbers();
            autoResetEvent.Set();
        }

        static void PrintNumbers(Mutex mutex)
        {
            mutex.WaitOne();
            PrintNumbers();
            mutex.ReleaseMutex();
        }

        static void PrintNumbers(Semaphore semaphore)
        {
            semaphore.WaitOne();
            PrintNumbers();
            semaphore.Release();
        }
    }
}
