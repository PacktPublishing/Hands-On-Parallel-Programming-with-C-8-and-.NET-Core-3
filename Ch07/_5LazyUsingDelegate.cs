using System;
using System.Threading;

namespace Ch07
{
    class _5LazyUsingDelegate
    {
        public Data CachedData { get; set; }
        static Data GetDataFromDatabase()
        {
            Console.WriteLine("Fetching data");
            //Dummy Delay
            Thread.Sleep(5000);
            return new Data();
        }

        static void Main(string[] args)
        {
            //  DataWrapper dataWrapper = new DataWrapper();
            Console.WriteLine("Creating Lazy object");
            Func<Data> dataFetchLogic = new Func<Data>(()=> GetDataFromDatabase());
            Lazy<Data> lazyDataWrapper = new Lazy<Data>(dataFetchLogic);

            Console.WriteLine("Lazy Object Created");
            Console.WriteLine("Now we want to access data");

            var data = lazyDataWrapper.Value;

            Console.WriteLine("Finishing up");

            Console.ReadLine();
        }
    }


}
