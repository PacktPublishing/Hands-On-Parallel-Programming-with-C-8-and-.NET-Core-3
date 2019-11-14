using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;


namespace Ch04
{
    class _3MergeOptions
    {
       

       

        public static void Main()
        {
           //AutoBuffered();

       //   AutoBuffered();
       FullyBuffered();
           
        //  NonBuffered();
          
            

            
            Console.ReadLine();
        }

        private static void NonBuffered()
        {
            var range = ParallelEnumerable.Range(1, 100);
            Stopwatch watch = null;
            ParallelQuery<int> notBufferedQuery = range
                .WithMergeOptions(ParallelMergeOptions.NotBuffered).Where(i => i % 10 == 0).Select(x => {
                 Thread.SpinWait(1000);
                 return x;
             });
            watch = Stopwatch.StartNew();
            foreach (var item in notBufferedQuery)
            {
                Console.WriteLine( $"{item}:{watch.ElapsedMilliseconds}");
            }
            Console.WriteLine($"\nNotBuffered Full Result returned in {watch.ElapsedMilliseconds} ms" );
            watch.Stop();
        }

        private static void FullyBuffered()
        {
            var range = ParallelEnumerable.Range(1, 100);
            Stopwatch watch = null;
            ParallelQuery<int> fullyBufferedQuery = range
                .WithMergeOptions(ParallelMergeOptions.NotBuffered).Where(i => i % 10 == 0).Select(x => {
                    Thread.SpinWait(1000);
                    return x;
                });
            watch = Stopwatch.StartNew();
            foreach (var item in fullyBufferedQuery)
            {
                Console.WriteLine($"{item}:{watch.ElapsedMilliseconds}");
            }
            Console.WriteLine($"\nFullyBuffered Full Result returned in {watch.ElapsedMilliseconds} ms" );
            watch.Stop();
        }

        private static void AutoBuffered()
        {
            var range = ParallelEnumerable.Range(1, 100);
            Stopwatch watch = null;
            ParallelQuery<int> query = range
                 .WithMergeOptions(ParallelMergeOptions.AutoBuffered).Where(i => i % 10 == 0).Select(x => {
                     Thread.SpinWait(1000);
                    return x;
                });
            watch = Stopwatch.StartNew();
            foreach (var item in query)
            {
                Console.WriteLine($"{item}:{watch.ElapsedMilliseconds}");
            }
            Console.WriteLine($"\nAutoBuffered Full Result returned in {watch.ElapsedMilliseconds} ms" );
            watch.Stop();
        }
    }
}
