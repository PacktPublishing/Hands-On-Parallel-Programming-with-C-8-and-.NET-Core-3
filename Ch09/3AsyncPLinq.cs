using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ch09
{
    class _3AsyncPLinq
    {
        public static async Task Main(string[] args)
        {
            //Create 100 dummy urls
            var urls = Enumerable.Repeat("http://www.dummyurl.com", 100);
            foreach (var url in urls)
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine();
            }

            Parallel.ForEach(urls, async url =>
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
                string content = await response.Content.ReadAsStringAsync();
            });

            Console.ReadKey();
        }

    }
}
