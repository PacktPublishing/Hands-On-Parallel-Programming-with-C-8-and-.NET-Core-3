using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ch02
{
    class _10ParentChildTasks
    {
        public static void Main()
        {
          //  DetachedTask();

            AttachedTasks();
            Console.ReadLine();

        }

        private static void AttachedTasks()
        {
            Task parentTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("     Parent task started");

                Task childTask = Task.Factory.StartNew(() => {
                    Console.WriteLine("     Child task started");
                },TaskCreationOptions.AttachedToParent);
                Console.WriteLine("     Parent task Finish");
            });
            //Wait for parent to finish
            parentTask.Wait();
            Console.WriteLine("     Work Finished");
        }

        private static void DetachedTask()
        {
            Task parentTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("     Parent task started");

                Task childTask = Task.Factory.StartNew(() => {
                    Console.WriteLine("     Child task started");
                });
                Console.WriteLine("     Parent task Finish");
            });
            //Wait for parent to finish
            parentTask.Wait();
            Console.WriteLine("     Work Finished");
        }
    }
}
