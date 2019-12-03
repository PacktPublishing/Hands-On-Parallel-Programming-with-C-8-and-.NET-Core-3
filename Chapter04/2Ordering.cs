using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Ch04
{
    class _2Ordering
    {
        public static void Main()
        {
            GeneratingParallelSequence();
            AsOrdered();
            AsUnOrdered();
            
            Console.ReadLine();
        }

        private static void GeneratingParallelSequence()
        {
            Stopwatch watch = Stopwatch.StartNew();

            IEnumerable<int> parallelRange = ParallelEnumerable.Range(0, 5000).Select(i => i * i);
            foreach (var item in parallelRange)  {        }
            watch.Stop();
            Console.WriteLine($"Time elapsed using ParallelEnumerable : {watch.ElapsedMilliseconds}" );

            Stopwatch watch2 = Stopwatch.StartNew();
            IEnumerable<int> range = Enumerable.Range(0, 5000).Select(i=>i *i);
            foreach (var item in range) { }
            watch2.Stop();
            Console.WriteLine($"Time elapsed using Enumerable : {watch2.ElapsedMilliseconds}" );

            Stopwatch watch3 = Stopwatch.StartNew();
            IEnumerable<int> rangeRepeat = ParallelEnumerable.Repeat(1, 5000);
            foreach (var item in rangeRepeat) { }
            watch3.Stop();
            Console.WriteLine($"Time elapsed using Repeat : {watch3.ElapsedMilliseconds}" );

            
            Console.ReadLine();
        }

        private static void AsUnOrdered()
        {
            var range = Enumerable.Range(100, 10000);
            var ordered = range.AsParallel().AsOrdered().Take(100).AsUnordered().Select(i => i * i).ToList();

            ordered.ForEach(i => Console.Write(i + "-"));
        }

        private static void AsOrdered()
        {
            var range = Enumerable.Range(1, 10);

            Console.WriteLine("Sequential Ordered");
            range.ToList().ForEach(i => Console.Write(i + "-"));

            Console.WriteLine("Parallel Ordered");
            var ordered = range.AsParallel().AsOrdered().Select(i => i).ToList();
            ordered.ForEach(i => Console.Write(i + "-"));

            Console.WriteLine("\nUnOrdered");
            var unordered = range.AsParallel().Select(i => i).ToList();
            unordered.ForEach(i => Console.Write(i + "-"));
        }
    }
}
