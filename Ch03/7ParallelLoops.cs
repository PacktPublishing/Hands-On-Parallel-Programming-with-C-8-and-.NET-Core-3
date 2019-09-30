using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ch03
{
    class _7ParallelLoops
    {
        public static void Main()
        {
            ParallelFor();
            //ParallelForEach();
            Console.ReadLine();
        }

        private static void ParallelForEach()
        {
            List<string> urls = new List<string>() {"www.google.com" , "www.yahoo.com","www.bing.com" };
            Parallel.ForEach(urls, url =>
            {
                Ping pinger = new Ping();
                Console.WriteLine("Ping Url {0} status is {1} by Task {2}",url, pinger.Send(url).Status ,Task.CurrentId);
            });


        }

        private static void ParallelFor()
        {
            int totalFiles = 0;
            var files = Directory.GetFiles("C:\\");
            Parallel.For(0, files.Length, (i) =>
                 {
                     FileInfo fileInfo = new FileInfo(files[i]);
                     if (fileInfo.CreationTime.Day == DateTime.Now.Day)
                         Interlocked.Increment(ref totalFiles);
                 });
            Console.WriteLine(totalFiles);
        }
    }
}
