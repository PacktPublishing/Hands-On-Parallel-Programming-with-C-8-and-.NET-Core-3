using System;
namespace Ch01
{
    class _3ParameterizedThreadStart
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Execution!!!");

            //Using Thread with parameter
            CreateThreadUsingThreadClassWithParameter();

            Console.WriteLine("Finish Execution");
            Console.ReadLine();
        }

        private static void CreateThreadUsingThreadClassWithParameter()
        {
            System.Threading.Thread thread;
            thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(PrintNumberNTimes));
            thread.Start(10);
        }
        private static void PrintNumberNTimes(object times)
        {
            int n = Convert.ToInt32(times);
            for (int i = 0; i < n; i++)
            {
                Console.Write(1);
            }
            Console.WriteLine();
        }
    }
}
