using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ch05
{
  
    class _5SlimLocks
    {
        public static void Main()
        {
           
            // ReaderWriteLockSlim();
            //ThrottlerUsingSemaphoreSlim();
            // ManualResetEventSlimDemo();

            //BarrierDemoWithStaticParticipants();

            Parallel.For(1, 5, (i) => SpinLock(i));
            
            Console.ReadLine();
        }
        static List<int> _itemsList = new List<int>();
        static SpinLock _spinLock = new SpinLock();
        private static void SpinLock(int number)
        {
            bool lockTaken = false;
            try
            {
                Console.WriteLine($"Task {Task.CurrentId} Waiting for lock" );
                _spinLock.Enter(ref lockTaken);
                Console.WriteLine($"Task {Task.CurrentId} Updting list");
                _itemsList.Add(number);
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"Task {Task.CurrentId} Exiting Update");
                    _spinLock.Exit(false);
                }
            }
        }

        static string _serviceName = string.Empty;
        static Barrier serviceBarrier = new Barrier(5);
        static CountdownEvent serviceHost1CountdownEvent = new CountdownEvent(6);
        static CountdownEvent serviceHost2CountdownEvent = new CountdownEvent(6);
        static CountdownEvent finishCountdownEvent = new CountdownEvent(5);
        //static ManualResetEventSlim manualResetEventSlim = new ManualResetEventSlim();
        private static void BarrierDemoWithStaticParticipants()
        {
            Task[] tasks = new Task[5];

            Task serviceManager = Task.Factory.StartNew(() =>
            {
                //Block until service name is set by any of thread
                while (string.IsNullOrEmpty(_serviceName))
                    Thread.Sleep(1000);

                string serviceName = _serviceName;
                HostService(serviceName);
                //Now signal other threads to proceed
                serviceHost1CountdownEvent.Signal();

                serviceHost1CountdownEvent.Wait();

                //Block until service name is set by any of thread
                while (_serviceName != "Service2")
                    Thread.Sleep(1000);
                //manualResetEventSlim.Set();
                Console.WriteLine($"All tasks completed for service {serviceName}." );
                CloseService(serviceName);
                HostService(_serviceName);
                serviceHost2CountdownEvent.Signal();

                serviceHost2CountdownEvent.Wait();

                finishCountdownEvent.Wait();
                CloseService(_serviceName);
                Console.WriteLine($"All tasks completed for service {_serviceName}." );
            });
            for (int i = 0; i < 5; ++i)
            {
                int j = i;
                tasks[j] = Task.Factory.StartNew(() =>
                {
                    GetDataFromService1And2(j);
                });
            }
            Task.WaitAll(tasks);

            Console.WriteLine("Fetch completed");
        }

        private static void GetDataFromService1And2(int j)
        {
            _serviceName = "Service1";

            serviceHost1CountdownEvent.Signal();
            Console.WriteLine($"Task with id {Task.CurrentId} signaled countdown event and waiting for service to start");

            //Waiting for service to start
            serviceHost1CountdownEvent.Wait();
          
            Console.WriteLine($"Task with id {Task.CurrentId} fetching data from service " );
            serviceBarrier.SignalAndWait();
                  
            //change servicename
            _serviceName = "Service2";

            //Signal Countdown event
            serviceHost2CountdownEvent.Signal();
           
            Console.WriteLine($"Task with id {Task.CurrentId} signaled countdown event and waiting for service to start");
            serviceHost2CountdownEvent.Wait();

            Console.WriteLine($"Task with id {Task.CurrentId} fetching data from service ");
            serviceBarrier.SignalAndWait();
          
            //Signal Countdown event
            finishCountdownEvent.Signal();
        }

        private static void CloseService(string name)
        {
            Console.WriteLine($"Service {name} closed");
        }

        private static void HostService(string name)
        {
            Console.WriteLine($"Service {name} hosted");
        }

        private static void ManualResetEventSlimDemo()
        {
            ManualResetEventSlim manualResetEvent = new ManualResetEventSlim(false);
            Task signalOffTask = Task.Factory.StartNew(() => {
                while (true)
                {
                    Thread.Sleep(3000);
                    Console.WriteLine("Network is down");
                    manualResetEvent.Reset();
                }
            });
            Task signalOnTask = Task.Factory.StartNew(() => {
                while (true)
                {
                    Thread.Sleep(5000);
                    Console.WriteLine("Network is Up");
                    manualResetEvent.Set();
                }
            });
            for (int i = 0; i < 3; i++)
            {
                Parallel.For(0, 5, (j) => {
                    Console.WriteLine($"Task with id {Task.CurrentId} waiting for network to be up");
                    manualResetEvent.Wait();
                    Console.WriteLine($"Task with id {Task.CurrentId} making service call");
                    DummyServiceCall();
                });
                Thread.Sleep(3000);
            }
        }

        private static void DummyServiceCall()
        {
        }

        private static void ThrottlerUsingSemaphoreSlim()
        {
            var range = Enumerable.Range(1, 12);
            SemaphoreSlim semaphore = new SemaphoreSlim(3, 3);
            range.AsParallel().AsOrdered().ForAll(i =>
            {
                try
                {
                    semaphore.Wait();
                    Console.WriteLine("Index {i} making service call using Task {Task.CurrentId}");
                    //Simulate Http call
                    CallService(i);
                    Console.WriteLine("Index {i} releasing semaphore using Task {Task.CurrentId}");
                }
                finally
                {
                    semaphore.Release();
                }

            });
        }

        private static void CallService(int i)
        {
            Thread.Sleep(1000);
        }

        static ReaderWriterLockSlim _readerWriterLockSlim = new ReaderWriterLockSlim();
        static List<int> _list = new List<int>();

        private static void ReaderWriteLockSlim()
        {
            Task writerTask = Task.Factory.StartNew( WriterTask);
            for (int i = 0; i < 3; i++)
            {
                Task readerTask = Task.Factory.StartNew(ReaderTask);
            }
        }
        static void WriterTask()
        {
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    _readerWriterLockSlim.EnterWriteLock();
                    Console.WriteLine($"Entered WriteLock on Task {Task.CurrentId}" );
                    int random = new Random().Next(1, 10);
                    _list.Add(random);
                    Console.WriteLine($"Added {random} to list on Task {Task.CurrentId}");
                    Console.WriteLine($"Exiting WriteLock on Task {Task.CurrentId}");
                }
                finally
                {
                    _readerWriterLockSlim.ExitWriteLock();
                }
                Thread.Sleep(1000);
            }
        }
        static void ReaderTask()
        {
            for (int i = 0; i < 2; i++)
            {
                try
                {
                    _readerWriterLockSlim.EnterReadLock();
                Console.WriteLine($"Entered ReadLock on Task {Task.CurrentId}");

                Console.WriteLine($"Items: { _list.Select(j => j.ToString()).Aggregate((a, b) => a + "," + b)} on Task {Task.CurrentId}");
                Console.WriteLine($"Exiting ReadLock on Task {Task.CurrentId}");
                }
                finally
                {
                    _readerWriterLockSlim.ExitWriteLock();
                }
                Thread.Sleep(1000);
            }
        }
    }
}
