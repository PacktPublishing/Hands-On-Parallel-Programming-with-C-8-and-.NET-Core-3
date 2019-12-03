using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ch09
{
    class _1AsyncAndAwait
    {
        public static void Main()
        {
            //Func<int, Task<int>> square =async (x) => { return x * x; };

            //Func<int, Task<int>> square = async (x) => x * x;
            DownloadSynchronously();
            DownloadAsynchronously();
            Console.ReadLine();
        }

        private static void DownloadAsynchronously()
        {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadComplete);
            client.DownloadStringAsync(new Uri("http://www.aspnet.com"));
        }

        private static void DownloadComplete(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Console.WriteLine("Some error has occured.");
                return;
            }
            Console.WriteLine(e.Result);
        }

        private static void DownloadSynchronously()
        {
            WebClient client = new WebClient();
            string reply = client.DownloadString("http://www.aspnet.com");

            Console.WriteLine(reply);
        }
    }
}
