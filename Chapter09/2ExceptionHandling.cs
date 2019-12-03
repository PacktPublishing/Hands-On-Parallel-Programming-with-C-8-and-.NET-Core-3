using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ch09
{
    class _2ExceptionHandling
    {
        //public static void Main()
        //{
        //    // AsyncReturningValueExample();
        //    Console.WriteLine("Main Method Startes");
        //    // var task = Scenario1CallAsyncWithoutAwaitFromInsideTryCatch();
        //    var task = Scenario2CallAsyncWithoutAwaitFromInsideTryCatch();
        //    if (task.IsFaulted)
        //        Console.WriteLine(task.Exception.Flatten().Message.ToString());
        //    Console.WriteLine("In Main Method After calling method");
        //    Console.ReadLine();
        //}

        public static void Main()
        {
            Console.WriteLine("Main Method Startes");
            //var task = Scenario1CallAsyncWithoutAwaitFromOutsideTryCatch();
            //if (task.IsFaulted)
            //    Console.WriteLine(task.Exception.Flatten().Message.ToString());

            //var task = Scenario2CallAsyncWithoutAwaitFromInsideTryCatch();
            //if (task.IsFaulted)
            //    Console.WriteLine(task.Exception.Flatten().Message.ToString());

            //var task = Scenario3CallAsyncWithAwaitFromOutsideTryCatch();
            //if (task.IsFaulted)
            //    Console.WriteLine(task.Exception.Flatten().Message.ToString());


            Scenario4CallAsyncWithoutAwaitFromOutsideTryCatch();

            Console.WriteLine("In Main Method After calling method");
            Console.ReadLine();
        }

        private static async void Scenario4CallAsyncWithoutAwaitFromOutsideTryCatch()
        {
            Task task = DoSomethingFaulty();
            Console.WriteLine("This should not execute");          
        }

        private static async Task Scenario3CallAsyncWithAwaitFromOutsideTryCatch()
        {
            await DoSomethingFaulty();
            Console.WriteLine("This should not execute");           
        }
        private static async Task Scenario2CallAsyncWithoutAwaitFromInsideTryCatch()
        {
            try
            {
                var task = DoSomethingFaulty();
                Console.WriteLine("This should not execute");
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                task.ContinueWith((s) =>
                {
                    Console.WriteLine(s);
                });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        private static async Task Scenario2AsyncReturningTaskExample()
        {
            try
            {
                Task task = DoSomethingFaulty();
                Console.WriteLine("This should not execute");
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                task.ContinueWith((s) =>
                {
                    Console.WriteLine(s);
                });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                              // Console.WriteLine(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        private static async Task Scenario1CallAsyncWithoutAwaitFromOutsideTryCatch()
        {
            Task task = DoSomethingFaulty();
            Console.WriteLine("This should not execute");
            try
            {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                task.ContinueWith((s) =>
                {
                    Console.WriteLine(s);
                });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                              // Console.WriteLine(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private static async void AsyncReturningValueExample()
        {
            try
            {
                string data = await GetDummyData();
                Console.WriteLine(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
          
        }

        private static Task DoSomethingFaulty()
        {
            Task.Delay(2000);
            throw new Exception("This is custom exception.");
        }

        private static Task<string> GetDummyData()
        {
            Task.Delay(2000);
            throw new Exception("This is custom exception.");
        }

    }
}
