using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ch07
{
    //class _8_LazyInitializer
    //{
    //    static Data _data;
    //    public static void Main()
    //    {
    //        for (int i = 0; i < 10; i++)
    //        {
    //            Console.WriteLine("Iteration {0}", i);
    //            // Lazily initialize _data 
    //            LazyInitializer.EnsureInitialized(ref _data, () =>
    //            {
    //                Console.WriteLine("Initializing data");
    //                // Returns value that will be assigned in the ref parameter.
    //                return new Data();
    //            });
    //        }

    //        Console.ReadLine();
    //    }
    //}
    class _8_LazyInitializer
    {
        static Data _data;
        static bool _initialized;

        static object _locker = new object();

        static void Initializer()
        {
            Console.WriteLine("Task with id {0}", Task.CurrentId);

            LazyInitializer.EnsureInitialized(ref _data,ref _initialized, ref _locker, () =>
            {
                Console.WriteLine("Task with id {0} is Initializing data", Task.CurrentId);
                // Returns value that will be assigned in the ref parameter.
                return new Data();
            });
        }

        public static void Main()
        {
            Parallel.For(0, 10, (i) => Initializer());

            Console.ReadLine();
        }
    }
}
