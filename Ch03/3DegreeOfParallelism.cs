using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch03
{
    class _3DegreeOfParallelism
    {
        public static void Main()
        {
           // DegreeOfParallelismWithParallelFor();
            DegreeOfParallelismWithParallelForEach();
            Console.ReadLine();
        }

        private static void DegreeOfParallelismWithParallelForEach()
        {
            var items = Enumerable.Range(1, 20);
            Parallel.ForEach(items, new ParallelOptions { MaxDegreeOfParallelism = 4 }, item =>
           {
               Console.WriteLine("Index {0} executing on Task Id {1}",item, Task.CurrentId);
           });
        }

        private static void DegreeOfParallelismWithParallelFor()
        {
            Parallel.For(1, 20, new ParallelOptions { MaxDegreeOfParallelism = 4 }, index =>
             {
                 Console.WriteLine("Index {0} executing on Task Id {1}",index, Task.CurrentId);
             });
        }
    }
}
