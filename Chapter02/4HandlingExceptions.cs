using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ch02
{
    class _4HandlingExceptionFromSingleTask
    {
        static void Main(string[] args)
        {
            Task task = null;
            try
            {
                task = Task.Factory.StartNew(() =>
                {
                    int num = 0, num2 = 25;
                    var result = num2 / num;
                });
                task.Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine($"Task has finished with exception {ex.InnerException.Message}" );
            }
            Console.ReadLine();
        }
    }
}
