using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ch02
{
    class _11WaitingOnTasks
    {
        public static void Main()
        {
            var normalWaitTask = Task.Factory.StartNew(() => Console.WriteLine("Inside Thread"));
            //Blocks the current thread until task finishes.
            normalWaitTask.Wait();

            Console.WriteLine("After Task Finishes");

            WaitAllDemo();
            WaitAnyDemo();
            WhenAllDemo();
            WhenAnyDemo();

            Console.ReadLine();
        }

        private static void WhenAnyDemo()
        {
            Task taskA = Task.Factory.StartNew(() => Console.WriteLine("TaskA finished"));
            Task taskB = Task.Factory.StartNew(() => Console.WriteLine("TaskB finished"));
            Task.WhenAny(taskA, taskB);
            Console.WriteLine("Calling method finishes");
        }

        private static void WhenAllDemo()
        {
            Task taskA = Task.Factory.StartNew(() => Console.WriteLine("TaskA finished"));
            Task taskB = Task.Factory.StartNew(() => Console.WriteLine("TaskB finished"));
            Task.WhenAll(taskA, taskB);
            Console.WriteLine("Calling method finishes");
        }

        private static void WaitAnyDemo()
        {
            Task taskA = Task.Factory.StartNew(() => Console.WriteLine("TaskA finished"));
            Task taskB = Task.Factory.StartNew(() => Console.WriteLine("TaskB finished"));
            Task.WaitAny(taskA, taskB);
            Console.WriteLine("Calling method finishes");
        }

        private static void WaitAllDemo()
        {
            Task taskA = Task.Factory.StartNew(() => Console.WriteLine("TaskA finished"));
            Task taskB = Task.Factory.StartNew(() => Console.WriteLine("TaskB finished"));
            Task.WaitAll(taskA, taskB);
            Console.WriteLine("Calling method finishes");
        }
    }
}
