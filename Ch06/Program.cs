using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ch06
{
    class Program
    {
        static void Main(string[] args)
        {

            // ProducerConsumerUsingQueues();
            //ProducerConsumerUsingQueuesWithLock();
            //ProducerConsumerUsingConcurrentQueues();
            // ProducerConsumerUsingConcurrentStack();

            // ConcurrentBackDemo();
            //BlockingCollectionDemo();
            //BlockingCollectionMultipleProducerConsumer();
            ConcurrentDictionaryDemo();
            Console.ReadLine();
            Console.ReadLine();
        }

        private static void BlockingCollectionMultipleProducerConsumer()
        {
            BlockingCollection<int>[] produceCollections = new BlockingCollection<int>[2];
            produceCollections[0] = new BlockingCollection<int>(5);
            produceCollections[1] = new BlockingCollection<int>(5);

            Task producerTask1 = Task.Factory.StartNew(() =>
            {
                for (int i = 1; i <= 5; ++i)
                {
                    produceCollections[0].Add(i);
                    Thread.Sleep(100);
                }
                produceCollections[0].CompleteAdding();
            });

            Task producerTask2 = Task.Factory.StartNew(() =>
            {
                for (int i = 6; i <= 10; ++i)
                {
                    produceCollections[1].Add(i);
                    Thread.Sleep(200);
                }
                produceCollections[1].CompleteAdding();
            });

            while (!produceCollections[0].IsCompleted ||
                !produceCollections[1].IsCompleted)
            {
                int item;
                BlockingCollection<int>.TryTakeFromAny(produceCollections, out item, TimeSpan.FromSeconds(1));
                if (item != default(int))
                {
                    Console.WriteLine("Item fetched is {0}", item);
                }
            }

        }

        private static void BlockingCollectionDemo()
        {
            BlockingCollection<int> blockingCollection = new BlockingCollection<int>(10);

            Task producerTask = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 5; ++i)
                {
                    blockingCollection.Add(i);
                }

                blockingCollection.CompleteAdding();
            });

            Task consumerTask = Task.Factory.StartNew(() =>
            {
                while (!blockingCollection.IsCompleted)
                {
                    int item = blockingCollection.Take();
                    Console.WriteLine("Item retrieved is {0}" , item);
                }
            });

            Task.WaitAll(producerTask, consumerTask);


        }

        private static void ConcurrentDictionaryDemo()
        {
            ConcurrentDictionary<int, string> concurrentDictionary = new ConcurrentDictionary<int, string>();

            Task producerTask1 = Task.Factory.StartNew(() => {
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(100);
                    concurrentDictionary.TryAdd(i, (i * i).ToString());
                }
            });
            Task producerTask2 = Task.Factory.StartNew(() => {
                for (int i = 10; i < 25; i++)
                {
                    Thread.Sleep(100);
                    concurrentDictionary.TryAdd(i, (i * i).ToString());
                }
            });
            Task producerTask3 = Task.Factory.StartNew(() => {
                for (int i = 15; i < 20; i++)
                {
                    Thread.Sleep(100);
                    concurrentDictionary.AddOrUpdate(i, (i * i).ToString(),(key, value) => (key * key).ToString());
                }
            });

            Task.WaitAll(producerTask1, producerTask2);

            Console.WriteLine("Keys are {0} ", string.Join(",", concurrentDictionary.Keys.Select(c => c.ToString()).ToArray()));
        }

        static ConcurrentBag<int> concurrentBag = new ConcurrentBag<int>();

        private static void ConcurrentBackDemo()
        {
            ManualResetEventSlim manualResetEvent = new ManualResetEventSlim(false);

            Task producerAndConsumerTask = Task.Factory.StartNew(() =>
            {
                for (int i = 1; i <= 3; ++i)
                {
                    concurrentBag.Add(i);
                }
                //Allow second thread to add items
                manualResetEvent.Wait();

                while (concurrentBag.IsEmpty == false)
                {
                    int item;
                    if (concurrentBag.TryTake(out item))
                    {
                        Console.WriteLine("Item is {0}", item);
                    }
                }
            });


            Task producerTask = Task.Factory.StartNew(() =>
            {
                for (int i = 4; i <= 6; ++i)
                {
                    concurrentBag.Add(i);
                }
                manualResetEvent.Set();
            });

          //  Task.WaitAll(producerAndConsumerTask, producerTask);
        }

        static Queue<int> _queue = new Queue<int>();
        static object _locker = new object();
        private static void ProducerConsumerUsingQueuesWithLock()
        {
            // Create a Queue.
            Queue<int> cq = new Queue<int>();

            // Populate the queue.
            for (int i = 0; i < 500; i++) cq.Enqueue(i);

            int sum = 0;

            Parallel.For(0, 500, (i) =>
            {
                int localSum = 0;
                int localValue;
                Monitor.Enter(_locker);
                while (cq.TryDequeue(out localValue))
                {
                    Thread.Sleep(10);
                    localSum += localValue;
                }
                Monitor.Exit(_locker);
                Interlocked.Add(ref sum, localSum);
            });


            Console.WriteLine("Calculated Sum is {0} and should be {1}", sum, Enumerable.Range(0, 500).Sum());
        }

        private static void ProducerConsumerUsingQueues()
        {
            // Create a Queue.
            Queue<int> cq = new Queue<int>();

            // Populate the queue.
            for (int i = 0; i < 500; i++) cq.Enqueue(i);

            int sum = 0;
           
            Parallel.For(0, 500, (i) =>
            {
                int localSum = 0;
                int localValue;
                while (cq.TryDequeue(out localValue))
                {
                    Thread.Sleep(10);
                    localSum += localValue;
                }
                Interlocked.Add(ref sum, localSum);
            });
          

            Console.WriteLine("Calculated Sum is {0} and should be {1}", sum, Enumerable.Range(0, 500).Sum());
        }

        private static void ProducerConsumerUsingConcurrentQueues()
        {
            // Create a Queue.
            ConcurrentQueue<int> cq = new ConcurrentQueue<int>();
           
            // Populate the queue.
            for (int i = 0; i < 500; i++) cq.Enqueue(i);

            int sum = 0;

            Parallel.For(0, 500, (i) =>
            {
                int localSum = 0;
                int localValue;
                while (cq.TryDequeue(out localValue))
                {
                    Thread.Sleep(10);
                    localSum += localValue;
                }
                Interlocked.Add(ref sum, localSum);
            });


            Console.WriteLine("outerSum = {0}, should be {1}", sum, Enumerable.Range(0, 500).Sum());
        }

        private static void ProducerConsumerUsingConcurrentStack()
        {
            // Create a Queue.
            ConcurrentStack<int> concurrentStack = new ConcurrentStack<int>();

            // Populate the queue.
            for (int i = 0; i < 500; i++) concurrentStack.Push(i);
            concurrentStack.PushRange(new[] { 1,2,3,4,5});

            int sum = 0;

            Parallel.For(0, 500, (i) =>
            {
                int localSum = 0;
                int localValue;
                while (concurrentStack.TryPop(out localValue))
                {
                    Thread.Sleep(10);
                    localSum += localValue;
                }
                Interlocked.Add(ref sum, localSum);
            });


            Console.WriteLine("outerSum = {0}, should be 124765", sum);
        }
    }
}
