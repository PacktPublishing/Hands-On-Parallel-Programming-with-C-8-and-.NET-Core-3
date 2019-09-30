using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ch02
{
    class _6HandlingExceptionUsingCallbacks
    {
        static void Main(string[] args)
        {
            Task taskA = Task.Factory.StartNew(() => throw new DivideByZeroException());
            Task taskB = Task.Factory.StartNew(() => throw new ArithmeticException());
            Task taskC = Task.Factory.StartNew(() => throw new NullReferenceException());
            try
            {
                Task.WaitAll(taskA, taskB, taskC);
            }
            catch (AggregateException ex)
            {
                ex.Handle(innerException =>
                {
                    Console.WriteLine(innerException.Message);
                    return true;  
                }
                );
            }

            Console.ReadLine();
        }
    }
}
