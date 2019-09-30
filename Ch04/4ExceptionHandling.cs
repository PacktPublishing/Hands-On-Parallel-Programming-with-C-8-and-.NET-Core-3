using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch04
{
    class _4ExceptionHandling
    {
        public static void Main()
        {
            //NormalExceptionHandling();
            ExceptionHandlingInsideDelegate();
            Console.ReadLine();
        }

        private static void ExceptionHandlingInsideDelegate()
        {
            var range = ParallelEnumerable.Range(1, 20);
            Func<int, int> selectDivision = (i) =>
            {
                try
                {
                   return  i / (i - 10);
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine("Divide by zero exception for {0}", i);
                    return -1;
                }
            };
            ParallelQuery<int> query = range.Select(i => selectDivision(i)).WithDegreeOfParallelism(2);
            try
            {
                query.ForAll(i => Console.WriteLine(i));
            }
            catch (AggregateException aggregateException)
            {
                foreach (var ex in aggregateException.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                    if (ex is DivideByZeroException)
                        Console.WriteLine("Attempt to divide by zero. Query stopped.");
                }
            }
        }

        private static void NormalExceptionHandling()
        {
            var range = ParallelEnumerable.Range(1, 20);
            ParallelQuery<int> query = range.Select(i => i / (i - 10)).WithDegreeOfParallelism(2);
            try
            {
                query.ForAll(i => Console.WriteLine(i));
            }
            catch (AggregateException aggregateException)
            {
                foreach (var ex in aggregateException.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                    if (ex is DivideByZeroException)
                        Console.WriteLine("Attempt to divide by zero. Query stopped.");
                }
            }
        }
    }
}
