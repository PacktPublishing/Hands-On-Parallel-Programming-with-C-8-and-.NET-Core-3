using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ch05
{
    class _4SignalingPrimitives
    {
        public static void Main()
        {
            //JoinThreads();
            // AutoResetEventDemo();
            // ManualResetEventDemo();

            WaitAll();

            // AlgoSolverWaitAny();

            //SignalAndWait();

            Console.ReadLine();
        }

        private static void SignalAndWait()
        {

        }

        static int findIndex = -1;
        static string winnerAlgo = string.Empty;
        private static void AlgoSolverWaitAny()
        {
            WaitHandle[] waitHandles = new WaitHandle[]
              {
                    new AutoResetEvent(false),
                    new AutoResetEvent(false)
              };
            var itemToSearch = 15000;
            var range = Enumerable.Range(1, 100000).ToArray();
            ThreadPool.QueueUserWorkItem(new WaitCallback(LinearSearch), new { Range = range, ItemToFind = itemToSearch, WaitHandle = waitHandles[0] });

            ThreadPool.QueueUserWorkItem(new WaitCallback(BinarySearch), new { Range = range, ItemToFind = itemToSearch, WaitHandle = waitHandles[1] });

            WaitHandle.WaitAny(waitHandles);

            Console.WriteLine("Item found at index {0} and faster algo is {1}", findIndex, winnerAlgo);
        }

        private static void BinarySearch(object state)
        {
            dynamic data = state;
            int[] x = data.Range;
            int valueToFind = data.ItemToFind;
            AutoResetEvent autoResetEvent = data.WaitHandle as AutoResetEvent;

            int foundIndex = Array.BinarySearch(x, valueToFind);

            Interlocked.CompareExchange(ref findIndex, foundIndex, -1);
            Interlocked.CompareExchange(ref winnerAlgo, "BinarySearch", string.Empty);

            autoResetEvent.Set();
        }

        public static void LinearSearch(object state)
        {
            dynamic data = state;
            int[] x = data.Range;
            int valueToFind = data.ItemToFind;
            AutoResetEvent autoResetEvent = data.WaitHandle as AutoResetEvent;
            int foundIndex = -1;
            for (int i = 0; i < x.Length; i++)
            {
                if (valueToFind == x[i])
                {
                    foundIndex = i;
                }
            }
            Interlocked.CompareExchange(ref findIndex, foundIndex, -1);
            Interlocked.CompareExchange(ref winnerAlgo, "LinearSearch", string.Empty);

            autoResetEvent.Set();
        }

        private static void ManualResetEventDemo()
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);
            Task signalOffTask = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(3000);
                    Console.WriteLine("Network is down");
                    manualResetEvent.Reset();
                }
            });
            Task signalOnTask = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(5000);
                    Console.WriteLine("Network is Up");
                    manualResetEvent.Set();
                }
            });
            for (int i = 0; i < 3; i++)
            {
                Parallel.For(0, 5, (j) =>
                {
                    Console.WriteLine("Task with id {0} waiting for network to be up", Task.CurrentId);
                    manualResetEvent.WaitOne();
                    Console.WriteLine("Task with id {0} making service call", Task.CurrentId);
                    DummyServiceCall();
                });
                Thread.Sleep(3000);
            }
        }

        private static void DummyServiceCall()
        {
        }

        private static void AutoResetEventDemo()
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            Task signallingTask = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    autoResetEvent.Set();
                }
            });
            int sum = 0;
            Parallel.For(1, 10, (i) =>
            {
                Console.WriteLine("Task with id {0} waiting for signal to enter", Task.CurrentId);
                autoResetEvent.WaitOne();
                Console.WriteLine("Task with id {0} received signal to enter", Task.CurrentId);
                sum += i;
            });
        }
        static int _dataFromService1 = 0;
        static int _dataFromService2 = 0;
        private static void WaitAll()
        {
            List<WaitHandle> waitHandles = new List<WaitHandle>
               {
                    new AutoResetEvent(false),
                    new AutoResetEvent(false)
               };

            ThreadPool.QueueUserWorkItem(new WaitCallback(FetchDataFromService1), waitHandles.First());

            ThreadPool.QueueUserWorkItem(new WaitCallback(FetchDataFromService2), waitHandles.Last());

            //Waits for all the threads (waitHandles) to call the .Set() method 
            //i.e. wait for data to be returned from both service
            WaitHandle.WaitAll(waitHandles.ToArray());

            Console.WriteLine($"The Sum is {_dataFromService1 + _dataFromService2}");
        }
        private static void FetchDataFromService1(object state)
        {
            Thread.Sleep(1000);
            _dataFromService1 = 890;
            var autoResetEvent = state as AutoResetEvent;
            autoResetEvent.Set();
        }
        private static void FetchDataFromService2(object state)
        {
            Thread.Sleep(1000);
            _dataFromService2 = 3;
            var autoResetEvent = state as AutoResetEvent;
            autoResetEvent.Set();
        }

        private static void JoinThreads()
        {
            int result = 0;
            Thread childThread = new Thread(() =>
            {
                Thread.Sleep(5000);
                result = 10;
            });
            childThread.Start();
            childThread.Join();
            Console.WriteLine("Result is {0}", result);
        }
    }
}
