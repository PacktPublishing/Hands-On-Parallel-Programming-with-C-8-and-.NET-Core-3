using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ch05
{
    class _2Interlocked
    {
        public static void Main()
        {
           // RaceConditionIncrement();
            InterlockedIncrement();
            InterlockedOperations();
            Console.ReadLine();
        }

        private static void InterlockedOperations()
        {
            //_counter becomes 1
            Interlocked.Increment(ref _counter); 
            // _counter becomes 0
            Interlocked.Decrement(ref _counter); 
            // Add: _counter becomes 2                                
            Interlocked.Add(ref _counter, 2); 
            //Subtract: _counter becomes 0
            Interlocked.Add(ref _counter, -2); 
            // Reads 64 bit field                                  
            Console.WriteLine(Interlocked.Read(ref _counter));  
            // Swaps _counter value with 10       
            Console.WriteLine(Interlocked.Exchange(ref _counter, 10)); 
            //Checks if _counter is 10 and if yes replace with 100                                                       
            Console.WriteLine(Interlocked.CompareExchange(ref _counter, 100, 10)); // _counter becomes 100

            Interlocked.MemoryBarrier();

            Interlocked.MemoryBarrierProcessWide();

        }

        private static void InterlockedIncrement()
        {
            Parallel.For(1, 1000, i =>
            {
                Thread.Sleep(100);
                Interlocked.Increment(ref _counter);
            });
            Console.WriteLine("Value for counter should be 999 and is {0}", _counter);
        }

        static long _counter;

        private static void RaceConditionIncrement()
        {
            Parallel.For(1, 1000, i =>
            {
                Thread.Sleep(100);
                _counter++;
            });
            Console.WriteLine("Value for counter should be 999 and is {0}", _counter);
        }
    }
}
