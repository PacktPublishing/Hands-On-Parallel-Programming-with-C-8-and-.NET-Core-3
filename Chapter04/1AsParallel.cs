using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch04
{
    class _1AsParallel
    {
        public static void Main()
        {
            var range = Enumerable.Range(1, 100000);
            //Here is sequential version
            var resultList = range.Where(i => i % 3 == 0).ToList();
            Console.WriteLine("Sequential: Total items are {0}", resultList.Count);

            //Here is Parallel Version using .AsParallel method
            resultList = range.AsParallel().Where(i => i % 3 == 0).ToList();
            Console.WriteLine("Parallel: Total items are {0}", resultList.Count);
            resultList = (from i in range.AsParallel()
                          where i % 3 == 0
                          select i).ToList();
            
            Console.WriteLine("Parallel: Total items are {0}", resultList.Count);

            Sequential();


            Console.ReadLine();
        }

        private static void Sequential()
        {
            var range = Enumerable.Range(1, 100000);
            var successResultList = range.AsParallel().Select(i => i % 3 == 0).ToList();
            var failedRequests = range.AsParallel().AsOrdered().Where((r, index) =>
            {
                Console.WriteLine("Task id is {0}", Task.CurrentId);
                return !successResultList[index];
            }
            ).ToList();
        }
    }
}
