using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ch03
{
    class _5CancellingLoop
    {
        public static void Main()
        {
           // BreakParallelLoop();

           // LowestBreakIteration();
            //ParallelLoopStateStop();
            CancelParallelLoops();
           Console.ReadLine();
        }

        private static void ParallelLoopStateStop()
        {
            var numbers = Enumerable.Range(1, 1000);
            Parallel.ForEach(numbers, (i, parallelLoopState) =>
            {
                Console.Write(i + " ");
                if (i % 4 == 0)
                {
                    Console.WriteLine("Loop Stopped on {0}", i);
                    parallelLoopState.Stop();
                }
            });
        }

        static object locker = new object();
        private static void LowestBreakIteration()
        {
            var numbers = Enumerable.Range(1, 1000);
            Parallel.ForEach(numbers, (i, parallelLoopState) =>
            {
                lock (locker)
                {
                    Console.WriteLine(string.Format(
                   $"For i={i} LowestBreakIteration={parallelLoopState.LowestBreakIteration} and Task id ={Task.CurrentId}"));
               
                if (i >= 10)
                {
                    parallelLoopState.Break();
                }
                }
            });
        }

        private static void CancelParallelLoops()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
                cancellationTokenSource.Cancel();
                Console.WriteLine("Token has been cancelled");
            });

            ParallelOptions loopOptions = new ParallelOptions()
            {
                CancellationToken = cancellationTokenSource.Token
            };
            try
            {
                Parallel.For(0, Int64.MaxValue, loopOptions, index =>
                {
                    Thread.Sleep(3000);
                    double result = Math.Sqrt(index);
                    Console.WriteLine($"Index {index}, result {result}");
                });
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Cancellation exception caught!");
            }
        }

        private static void BreakParallelLoop()
        {
            var numbers = Enumerable.Range(1, 1000);
            int numToFind = 2;
            Parallel.ForEach(numbers, (number, parallelLoopState) =>
            {
                Console.Write(number + "-");
                
                if (number == numToFind)
                {
                    Console.WriteLine($"Calling Break at {number}");
                    parallelLoopState.Break();
                }

            });           
        }

    }
}
