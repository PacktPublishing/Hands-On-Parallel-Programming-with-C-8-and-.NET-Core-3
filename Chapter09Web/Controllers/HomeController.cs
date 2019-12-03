using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ch09Web.Models;
using System.Net;
using System.Net.Http;

namespace Ch09Web.Controllers
{
    public class HomeController : Controller
    {
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

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://www.aspnet.com");
            string content = await response.Content.ReadAsStringAsync();
            return Content(content,"text/html");
        }

        public IActionResult Index2()
        {
            WebClient client = new WebClient();
            string content = client.DownloadString(new Uri("http://www.aspnet.com"));
            return Content(content, "text/html");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
