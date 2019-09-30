using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch03
{
    class _4PartitionerDemo
    {
        public static void Main()
        {
            CustomPartitionDemo();
            Console.ReadLine();
        }

        private static void CustomPartitionDemo()
        {
            var source = Enumerable.Range(1, 100).ToList();
            OrderablePartitioner<Tuple<int,int>> orderablePartitioner= Partitioner.Create(1, 100);
            
            Parallel.ForEach(orderablePartitioner, (range, state) =>
            {
                var startRange = range.Item1;
                var endRange = range.Item2;
                Console.WriteLine("Range execution finished on task {0} with range {1}-{2}", Task.CurrentId,startRange,endRange);
            });
        }
    }
}
