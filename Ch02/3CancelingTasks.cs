using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ch02
{
    class _3CancelingTasks
    {
        //static void Main(string[] args)
        //{
        //    CreateTasksWithCancellationToken();

        //    CancelTaskViaPoll();

        //    DownloadFileWithoutToken();
        //    CancelTaskViaRegisterDelegate();

        //    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        //    CancellationToken token = cancellationTokenSource.Token;

        //    DownloadFileWithToken(token);

        //    Task.Delay(2000);

        //    cancellationTokenSource.Cancel();
        //    Console.ReadLine();
        //}
        static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            DownloadFileWithToken(token);
            //Random delay before we cancel token
            Task.Delay(2000);
            cancellationTokenSource.Cancel();

            Console.ReadLine();
        }
        private static void DownloadFileWithToken(CancellationToken token)
        {
            WebClient webClient = new WebClient();
            //Here we are registering callback delegate that will get called as soon as user cancels token
            token.Register(() => webClient.CancelAsync());

            webClient.DownloadStringAsync(new Uri("http://www.google.com"));
            webClient.DownloadStringCompleted += (sender, e) => {
                if (!e.Cancelled)
                    Console.WriteLine("Download Complete.");
                else
                    Console.WriteLine("Download Cancelled.");
            };

        }

        private static void DownloadFileWithoutToken()
        {
            WebClient webClient = new WebClient();

            webClient.DownloadStringAsync(new Uri("http://www.google.com"));
            webClient.DownloadStringCompleted += (sender ,e )=> {
                if (!e.Cancelled)
                    Console.WriteLine("Download Complete.");
                else
                    Console.WriteLine("Download Cancelled.");
            };
        }

       

        private static void CancelTaskViaRegisterDelegate()
        {
            WebClient httpClient = new WebClient();

          //  httpClient.c
        }

        private static void CreateTasksWithCancellationToken()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            var sumTaskViaTaskOfInt = new Task<int>(() => Sum(5), token);
            sumTaskViaTaskOfInt.Start();
            Console.WriteLine($"Result from sumTask is {sumTaskViaTaskOfInt.Result}" );

            var sumTaskViaFactory = Task.Factory.StartNew<int>(() => Sum(5), token);
            Console.WriteLine($"Result from sumTask is {sumTaskViaFactory.Result}" );

            var sumTaskViaTaskRun = Task.Run<int>(() => Sum(5), token);
            Console.WriteLine($"Result from sumTask is {sumTaskViaTaskRun.Result}" );
        }
        private static int Sum(int n)
        {
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += i;
            }
            return sum;
        }

        private static void CancelTaskViaPoll()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            var sumTaskViaTaskOfInt = new Task(() => LongRunningSum(token), token);
            sumTaskViaTaskOfInt.Start();
            //Wait for user to press key to cancel task
            Console.ReadLine();

            cancellationTokenSource.Cancel();
        }

        private static void LongRunningSum(CancellationToken token)
        {
            for (int i = 0; i < 1000; i++)
            {
                //Simulate long running operation
                Task.Delay(100);

                if (token.IsCancellationRequested)
                    token.ThrowIfCancellationRequested();
            }
        }
    }
}
