using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ch05
{
    class Program
    {
        private static Mutex mutex = new Mutex();
        private static Mutex namedMutex = new Mutex(false,"ShaktiSinghTanwar");
        static object _locker = new object();


        static void Main(string[] args)
        {
            //  SequentialFileWrite();

            // ParallelFileWrite();

           // ParallelFileWriteWithDumbLock();

            // ParallelFileWriteWithCriticialSectionLock();

           // ParallelFileWriteWithCriticialSectionLockUsingMonitor();

          //  ParallelFileWriteWithCriticialSectionLockUsingMutex();
          //  ParallelFileWriteWithCriticialSectionLockUsingNamedMutex();

            ThrottlerUsingSemaphore();
            
            
                
            Console.ReadLine();
        }

        private static void ThrottlerUsingSemaphore()
        {
            var range = Enumerable.Range(1, 12);
            Semaphore semaphore = new Semaphore(3,3);
            range.AsParallel().AsOrdered().ForAll(i =>
            {
                semaphore.WaitOne();
                Console.WriteLine("Index {0} making service call using Task {1}",i, Task.CurrentId);
                //Simulate Http call
                CallService(i);
                Console.WriteLine("Index {0} releasing semaphore using Task {1}",i, Task.CurrentId);
                semaphore.Release();
            });
        }

        private static void CallService(int i)
        {
            Thread.Sleep(1000);
        }

        private static void ParallelFileWriteWithCriticialSectionLockUsingNamedMutex()
        {
            var range = Enumerable.Range(1, 1000);
            Stopwatch watch = Stopwatch.StartNew();
            range.AsParallel().AsOrdered().ForAll(i =>
            {
                Thread.Sleep(10);
                namedMutex.WaitOne(3000);
                File.AppendAllText("test.txt", i.ToString());
                namedMutex.ReleaseMutex();
            });
            watch.Stop();
            Console.WriteLine("Total time to write file is {0}", watch.ElapsedMilliseconds);
        }

        private static void ParallelFileWriteWithCriticialSectionLockUsingMutex()
        {
            var range = Enumerable.Range(1, 1000);
            Stopwatch watch = Stopwatch.StartNew();
            range.AsParallel().AsOrdered().ForAll(i =>
            {
                Thread.Sleep(10);
                mutex.WaitOne();
                File.AppendAllText("test.txt", i.ToString());
                mutex.ReleaseMutex();    
            });
            watch.Stop();
            Console.WriteLine("Total time to write file is {0}", watch.ElapsedMilliseconds);
        }
        private static void ParallelFileWriteWithCriticialSectionLockUsingMutex2()
        {
            var range = Enumerable.Range(1, 1000);
            Stopwatch watch = Stopwatch.StartNew();
            range.AsParallel().AsOrdered().ForAll(i =>
            {
                Thread.Sleep(10);
                mutex.WaitOne();
                //if (mutex.WaitOne(TimeSpan.FromSeconds(3), false))
                //{
                File.AppendAllText("test.txt", i.ToString());
                // }
                mutex.ReleaseMutex();
            });
            watch.Stop();
            Console.WriteLine("Total time to write file is {0}", watch.ElapsedMilliseconds);
        }
        private static void ParallelFileWriteWithCriticialSectionLockUsingMonitor()
        {
            var range = Enumerable.Range(1, 1000);
            Stopwatch watch = Stopwatch.StartNew();
            range.AsParallel().AsOrdered().ForAll(i =>
            {
                Thread.Sleep(10);
                Monitor.Enter(_locker);
                try
                {
                    File.AppendAllText("test.txt", i.ToString());
                }
                finally
                {
                    Monitor.Exit(_locker);
                }
            });
            watch.Stop();
            Console.WriteLine("Total time to write file is {0}", watch.ElapsedMilliseconds);
        }

        private static void ParallelFileWriteWithCriticialSectionLock()
        {
            var range = Enumerable.Range(1, 1000);
            Stopwatch watch = Stopwatch.StartNew();
            range.AsParallel().AsOrdered().ForAll(i =>
            {
                Thread.Sleep(10);
                lock (_locker)
                {
                    File.AppendAllText("test.txt", i.ToString());
                }
            });
            watch.Stop();
            Console.WriteLine("Total time to write file is {0}", watch.ElapsedMilliseconds);
        }

        private static void ParallelFileWriteWithDumbLock()
        {
            var range = Enumerable.Range(1, 1000);
            Stopwatch watch = Stopwatch.StartNew();
            range.AsParallel().AsOrdered().ForAll(i =>
            {
                lock (_locker)
                {
                    Thread.Sleep(10);
                    File.AppendAllText("test.txt", i.ToString());
                }
            });
            watch.Stop();
            Console.WriteLine("Total time to write file is {0}", watch.ElapsedMilliseconds);
        }


        private static void ParallelFileWrite()
        {
            var range = Enumerable.Range(1, 1000);
            Stopwatch watch = Stopwatch.StartNew();
            range.AsParallel().AsOrdered().ForAll(i =>
            {
                Thread.Sleep(10);
                File.AppendAllText("test.txt", i.ToString());
            });
            watch.Stop();
            Console.WriteLine("Total time to write file is {0}", watch.ElapsedMilliseconds);
        }

        private static void SequentialFileWrite()
        {
            var range = Enumerable.Range(1, 1000);
            Stopwatch watch = Stopwatch.StartNew();
            for (int i = 0; i < range.Count(); i++)
            {
                Thread.Sleep(10);
                File.AppendAllText("test.txt", i.ToString());
            }
            watch.Stop();
            Console.WriteLine("Total time to write file is {0}", watch.ElapsedMilliseconds);
        }
    }
}
