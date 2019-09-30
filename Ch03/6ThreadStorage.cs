using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ch03
{
    class _6ThreadStorage
    {
        public static void Main()
        {
           // ThreadLocalVariableForEach();
            ThreadLocalVariableFor();
            Console.ReadLine();
        }

        private static void ThreadLocalVariableFor()
        {
            var numbers = Enumerable.Range(1, 60);
            long sumOfNumbers = 0;

            Action<long> taskFinishedMethod = (taskResult) => {
                Console.WriteLine("Sum at the end of all task iterations for task {0} is {1}", Task.CurrentId, taskResult);
                Interlocked.Add(ref sumOfNumbers, taskResult);
            };

            Parallel.For(0,numbers.Count(), // collection of 60 number with each number having value equal to index
                                     () => 0, // method to initialize the local variable
                                     (j, loop, subtotal) => // Action performed on each iteration
                                     {
                                         subtotal += j; //Subtotal is Thread local variable
                                         return subtotal; // value to be passed to next iteration
                                     },
                                     taskFinishedMethod
                                     );


            Console.WriteLine("The total of 60 numbers is {0}", sumOfNumbers);
        }

        private static void ThreadLocalVariableForEach()
        {
            var numbers = Enumerable.Range(1, 60);
            long sumOfNumbers = 0;
            
            Action<long> taskFinishedMethod = (taskResult) => {
                Console.WriteLine("Sum at the end of all task iterations for task {0} is {1}", Task.CurrentId, taskResult);
                Interlocked.Add(ref sumOfNumbers, taskResult);
                };

            Parallel.ForEach<int, long>(numbers, // collection of 60 number with each number having value equal to index
                                     () => 0, // method to initialize the local variable
                                     (j, loop, subtotal) => // Action performed on each iteration
                                     {
                                         subtotal += j; //Subtotal is Thread local variable
                                         return subtotal; // value to be passed to next iteration
                                     },
                                     taskFinishedMethod
                                     );
         

            Console.WriteLine("The total of 60 numbers is {0}", sumOfNumbers);
        }
    }
}
