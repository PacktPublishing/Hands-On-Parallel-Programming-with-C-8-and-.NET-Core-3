using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ch02
{
    class _1CreatingAndStartingTasks
    {
        static void Main(string[] args)
        {
            // TaskFactoryUsingLambda();
            //TaskFactoryUsingAction();
            //TaskFactoryUsingDelegate();

            //TaskClassUsingLambda();
            //TaskClassUsingAction();
            //TaskClassUsingDelegate();

            //StaticTaskRunUsingLambda();
            //StaticTaskRunUsingAction();
            //StaticTaskRunUsingDelegate();

             StaticTaskFromResultUsingLambda();

            //TaskDelay();

            //StaticTaskFromResultUsingLambda();

            //CancellationTokenSource source = new CancellationTokenSource();
            //var token = source.Token;
            //source.Cancel();
            //Task task = Task.FromCanceled(token);
            //Task<int> canceledTask = Task.FromCanceled<int>(token);

           // GetResultsFromTasks();
           // TaskYield();
            Console.ReadLine();
        }

        private static void GetResultsFromTasks()
        {
            var sumTaskViaTaskOfInt = new Task<int>(()=> Sum(5));
            sumTaskViaTaskOfInt.Start();
            Console.WriteLine($"Result from sumTask is {sumTaskViaTaskOfInt.Result}" );

            var sumTaskViaFactory = Task.Factory.StartNew<int>(() => Sum(5));
            Console.WriteLine($"Result from sumTask is {sumTaskViaFactory.Result}" );

            var sumTaskViaTaskRun = Task.Run<int>(() => Sum(5));
            Console.WriteLine($"Result from sumTask is {sumTaskViaTaskRun.Result}" );

            var sumTaskViaTaskResult = Task.FromResult<int>( Sum(5));
            Console.WriteLine($"Result from sumTask is {sumTaskViaTaskResult.Result}" );
        }

        private async static void TaskYield()
        {
            for (int i = 0; i < 100000; i++)
            {
                Console.WriteLine(i);
                if (i % 1000 == 0)
                    await Task.Yield();
            }
        }

        private static void TaskDelay()
        {
            Console.WriteLine("What is the output of 20/2. We will show result in 2 seconds.");
            Task.Delay(2000);
            Console.WriteLine("After 2 seconds delay");
            Console.WriteLine("The output is 10");
        }

        private static void StaticTaskFromResultUsingLambda()
        {
           Task<int> resultTask = Task.FromResult<int>( Sum(10));
            Console.WriteLine(resultTask.Result);
        }

        private static int Sum(int n)
        {
            int sum=0;
            for (int i = 0; i < n; i++)
            {
                sum += i;
            }
            return sum;
        }

        private static void StaticTaskRunUsingDelegate()
        {
            Task.Run(delegate { PrintNumber10Times(); });
        }

        private static void StaticTaskRunUsingAction()
        {
            Task.Run(new Action( PrintNumber10Times));
        }

        private static void StaticTaskRunUsingLambda()
        {
            Task.Run(() => PrintNumber10Times());
        }

        private static void TaskFactoryUsingDelegate()
        {
            Task.Factory.StartNew(delegate { PrintNumber10Times(); });
        }

        private static void TaskClassUsingDelegate()
        {
            Task task = new Task(delegate { PrintNumber10Times(); });
            task.Start();
        }

        private static void TaskClassUsingAction()
        {
            Task task = new Task(new Action( PrintNumber10Times));
            task.Start();
        }

        private static void TaskClassUsingLambda()
        {
            Task task = new Task(()=> PrintNumber10Times());
            task.Start();
        }

        private static void TaskFactoryUsingAction()
        {
            Task.Factory.StartNew(new Action( PrintNumber10Times));
        }

        private static void PrintNumber10Times()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write(1);
            }
            Console.WriteLine();
        }

        private static void TaskFactoryUsingLambda()
        {
            Task.Factory.StartNew(() => PrintNumber10Times());
        }

    }
}
