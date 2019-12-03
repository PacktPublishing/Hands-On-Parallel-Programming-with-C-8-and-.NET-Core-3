using System;
namespace Ch01
{
    class _2ThreadStart
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Start Execution!!!");
                     
            //Using Thread without parameter
            CreateThreadUsingThreadClassWithoutParameter();

            Console.WriteLine("Finish Execution");
            Console.ReadLine();
        }

        private static void CreateThreadUsingThreadClassWithoutParameter()
        {
            System.Threading.Thread thread;
            thread = new System.Threading.Thread(new System.Threading.ThreadStart(PrintNumber10Times));
            thread.Start();
        }

        private static void PrintNumber10Times()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write(1);
            }
            Console.WriteLine();
        }
    }
}
