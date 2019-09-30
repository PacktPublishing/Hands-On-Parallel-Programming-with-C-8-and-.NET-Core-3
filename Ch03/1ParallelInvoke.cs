using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ch03
{
    class _1ParallelInvoke
    {
        public static void Main()
        {
            try
            {
                Parallel.Invoke(() => Console.WriteLine("Action 1"),
               new Action(() => Console.WriteLine("Action 2")
               ));
            }
            catch(AggregateException aggregateException)
            {
                foreach (var ex in aggregateException.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }
           
            Console.WriteLine("Unblocked");
            Console.ReadLine();
           
            Task.Factory.StartNew(
                () => {
                    Task.Factory.StartNew(() => Console.WriteLine("Action 1"),
                        TaskCreationOptions.AttachedToParent);
                    Task.Factory.StartNew(new Action(() => Console.WriteLine("Action 2"))
                        , TaskCreationOptions.AttachedToParent);
                        }
                );
            
        }
    }
}
