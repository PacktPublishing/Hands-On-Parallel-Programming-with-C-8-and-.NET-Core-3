using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Ch09
{
    internal class _4AsyncPerformance
    {
        public  static async Task Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            await Task.WhenAll(Task1(), Task2(), Task3());
            stopwatch.Stop();

            Console.WriteLine($"Total time taken is {stopwatch.ElapsedMilliseconds}");

            Console.ReadLine();
        }
     
        public static async Task MainAsync(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var value1 = await Task1();
            var value2 = await Task2();
            var value3 = await Task3();
            stopwatch.Stop();

            Console.WriteLine($"Total time taken is {stopwatch.ElapsedMilliseconds}");

        }
        public static async Task<int> Task1()
        {
            await Task.Delay(2000);
            return 100;
        }
        public static async Task<int> Task2()
        {
            await Task.Delay(2000);
            return 200;
        }
        public static async Task<int> Task3()
        {
            await Task.Delay(2000);
            return 300;
        }


        private static async Task DelayAsync()
        {
            await Task.Delay(2000);
        }

        public static void Deadlock()
        {
            var task = DelayAsync();
            task.Wait();
        }
    }

   
}
