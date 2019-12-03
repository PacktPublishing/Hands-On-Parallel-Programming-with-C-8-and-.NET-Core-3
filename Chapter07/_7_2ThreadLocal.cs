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
                Task.Factory.StartNew(() => Console.WriteLine($"Thread with id {Task.CurrentId} has counter value as {counter.Value}" ));
            }
            Console.ReadLine();
        }
    }
}
