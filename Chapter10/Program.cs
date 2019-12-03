using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ch10
{
    class Program
    {
        static void Main(string[] args)
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    Task task = new Task(() =>
            //     {
            //         for (int j = 0; j < 100; j++)
            //         {
            //             Thread.Sleep(100);
            //         }
            //         Console.WriteLine("Thread with Id ", Thread.CurrentThread.ManagedThreadId);
            //     });
            //    task.Start();
            //}
            Action computeAction = () =>
            {
                int i = 0;
                while (true)
                {
                    i = 1 * 1;
                }
            };
            Task.Run(() => computeAction());
            Task.Run(() => computeAction());
            Task.Run(() => computeAction());
            Task.Run(() => computeAction());
            Console.ReadLine();
        }
    }
}
