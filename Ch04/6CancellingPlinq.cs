using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Ch04
{
    class _6CancellingPlinq
    {        
        public static void Main()
        {
            var range = Enumerable.Range(1,Int32.MaxValue);
            CancellationTokenSource cs = new CancellationTokenSource();
            Task cancellationTask = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(400);
                cs.Cancel();
            });
            
            try
            {
                var result = range.AsParallel()
                  .WithCancellation(cs.Token)
                  .Select(number => number)
                  .ToList();
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (AggregateException ex)
            {
                foreach (var inner in ex.InnerExceptions)
                {
                    Console.WriteLine(inner.Message);
                }
            }
            Console.ReadLine();
        }
    }
}
