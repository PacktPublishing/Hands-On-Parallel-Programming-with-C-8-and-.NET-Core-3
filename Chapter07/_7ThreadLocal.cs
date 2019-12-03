using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ch07
{
    class _7ThreadLocal
    {
        [ThreadStatic]
        static int counter = 1;

        public static void Main()
        {
            for (int i = 0; i < 10; i++)
            {
                Task.Factory.StartNew(() => Console.WriteLine(counter));
            }
            Console.ReadLine();
        }
        
    }
}
