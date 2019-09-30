using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ch07
{
    class _7_2ThreadLocal
    {
        static ThreadLocal<int> counter = new ThreadLocal<int>(() => 1);

        public static void Main()
        {

            for (int i = 0; i < 10; i++)
            {
                Task.Factory.StartNew(() => Console.WriteLine("Thread with id {0} has counter value as {1}", Task.CurrentId, counter.Value));
            }
            Console.ReadLine();
        }
    }
}
