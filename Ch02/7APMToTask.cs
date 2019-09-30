using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ch02
{
    class _7APMToTask
    {
        public static void Main()
        {
            ReadFileSynchronously();

            ReadFileUsingAPMAsyncWithoutCallback();
                      
            ReadFileUsingTask();

            Console.ReadLine();
        }

        private static void ReadFileSynchronously()
        {
            string path = @"Test.txt";

            //Open the stream and read content.
            using (FileStream fs = File.OpenRead(path))
            {
                byte[] b = new byte[1024];
                UTF8Encoding encoder = new UTF8Encoding(true);
                fs.Read(b, 0, b.Length);
                Console.WriteLine(encoder.GetString(b));
            }
        }

        private static void ReadFileUsingTask()
        {
            string filePath = @"Test.txt";
            
            //Open the stream and read content.
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read,
         FileShare.Read, 1024, FileOptions.Asynchronous))
            {
                byte[] buffer = new byte[1024];
                UTF8Encoding encoder = new UTF8Encoding(true);
                IAsyncResult result =  fs.ReadAsync(buffer, 0, buffer.Length);

                Console.WriteLine("Do Something here");
                //Start task that will read file asynchronously
                var task = Task<int>.Factory.FromAsync(fs.BeginRead, fs.EndRead, buffer, 0, buffer.Length,null);
                Console.WriteLine("Do Something while file is read asynchronously");
                //Wait for task to finish
                task.Wait();
                Console.WriteLine(encoder.GetString(buffer));
            }
        }

        private static void ReadFileUsingAPMAsyncWithoutCallback()
        {
            string filePath = @"Test.txt";

            //Open the stream and read content.
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read,
         FileShare.Read, 1024, FileOptions.Asynchronous))
            {
                byte[] buffer = new byte[1024];
                UTF8Encoding encoder = new UTF8Encoding(true);
                IAsyncResult result = fs.BeginRead(buffer, 0, buffer.Length, null, null);

                Console.WriteLine("Do Something here");

                int numBytes = fs.EndRead(result);
                fs.Close();
                Console.WriteLine(encoder.GetString(buffer));
            }
        }
    }
}
