using System;

namespace Ch07
{
    class _6_2_ExceptionsWithLazyWithCaching
    {
        static int counter = 0;
        public Data CachedData { get; set; }
        static Data GetDataFromDatabase()
        {
            if (counter == 0)
            {
                Console.WriteLine("Throwing exception");
                throw new Exception("Some Error has occured");
            }
            else
            {
                return new Data();
            }
        }

        public static void Main()
        {
            Console.WriteLine("Creating Lazy object");
            Func<Data> dataFetchLogic = new Func<Data>(() => GetDataFromDatabase());
            Lazy<Data> lazyDataWrapper = new Lazy<Data>(dataFetchLogic,System.Threading.LazyThreadSafetyMode.PublicationOnly);

            Console.WriteLine("Lazy Object Created");
            Console.WriteLine("Now we want to access data");
            Data data = null;
            try
            {
                data = lazyDataWrapper.Value;
                Console.WriteLine("Data Fetched on Attempt 1");
            }
            catch (Exception)
            {
                Console.WriteLine("Exception 1");
            }
            try
            {
                counter++;
                data = lazyDataWrapper.Value;
                Console.WriteLine("Data Fetched on Attempt 2");
            }
            catch (Exception)
            {
                Console.WriteLine("Exception 2");
                //     throw;
            }
            Console.WriteLine("Finishing up");

            Console.ReadLine();
        }
    }
}
