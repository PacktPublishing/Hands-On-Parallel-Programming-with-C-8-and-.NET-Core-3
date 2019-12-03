using System;

namespace Ch01
{
    /// <summary>
    /// Performing Synchronous execution
    /// </summary>
    class _1Synchronous
    {
        /// <summary>
        /// Program execution starts and print given value 10 times
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Start Execution!!!");

            PrintNumber10Times();

            Console.WriteLine("Finish Execution");
            Console.ReadLine();
        }
   
        /// <summary>
        /// Logic to print number 10 times
        /// </summary>
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
