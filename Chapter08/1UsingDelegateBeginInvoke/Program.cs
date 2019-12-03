using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace _1UsingDelegateBeginInvoke
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting program");

           // Log("this information need to be logged");
            Task.Factory.StartNew(()=> Log("this information need to be logged"));
            FileInfo fi = new FileInfo("test.txt");
            byte[] data = new byte[fi.Length];
            FileStream fs = new FileStream("test.txt", FileMode.Open, FileAccess.Read, FileShare.Read, data.Length, true);
            // We still pass null for the last parameter because
            // the state variable is visible to the continuation delegate.
            Task<int> task = Task<int>.Factory.FromAsync(
                    fs.BeginRead, fs.EndRead, data, 0, data.Length, null);
            int result = task.Result;
            Console.WriteLine(result);
            //Action logAction = new Action(()=> Log("this information need to be logged"));
            //logAction.BeginInvoke(null,null);
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }

        private static void Log(string message)
        {
            //Simulate long running method
            Thread.Sleep(5000);
            //Log to file or database
            Console.WriteLine("Logging done");
        }
    }
}
