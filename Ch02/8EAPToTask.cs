using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ch02
{
    class _8EAPToTask
    {
        public static void Main()
        {
            EAPImplementation();
            EAPToTask();
        }

        private static Task<string> EAPToTask()
        {
            var taskCompletionSource = new TaskCompletionSource<string>();
            var webClient = new WebClient(); 
            webClient.DownloadStringCompleted += (s, e) =>
            {
                if (e.Error != null)
                    taskCompletionSource.TrySetException(e.Error);
                else if (e.Cancelled)
                    taskCompletionSource.TrySetCanceled();
                else
                    taskCompletionSource.TrySetResult(e.Result);
            };
            webClient.DownloadStringAsync(new Uri("http://www.someurl.com"));
            return taskCompletionSource.Task;
        }

        private static void EAPImplementation()
        {
            var webClient = new WebClient();
            webClient.DownloadStringCompleted += (s, e) =>
            {
                if (e.Error != null)
                    Console.WriteLine(e.Error.Message);
                else if (e.Cancelled)
                    Console.WriteLine("Download Cancel");
                else
                    Console.WriteLine(e.Result);
            };
            webClient.DownloadStringAsync(new Uri("http://www.someurl.com"));
        }
    }
}
