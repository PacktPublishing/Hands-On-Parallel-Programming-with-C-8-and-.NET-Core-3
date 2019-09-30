using System;
using System.Collections.Generic;
using System.Threading;

namespace Ch07
{
    class DataWrapper
    {
        public DataWrapper()
        {
            CachedData = GetDataFromDatabase();
            Console.WriteLine("Object initialized");
        }

        public Data CachedData { get; set; }

        private Data GetDataFromDatabase()
        {
            //Dummy Delay
            Thread.Sleep(5000);
            return new Data();
        }

        static void Main(string[] args)
        {
            //  DataWrapper dataWrapper = new DataWrapper();
            Console.WriteLine("Creating Lazy object");
            Lazy<DataWrapper> lazyDataWrapper = new Lazy<DataWrapper>();

            Console.WriteLine("Lazy Object Created");
            Console.WriteLine("Now we want to access data");

            var data = lazyDataWrapper.Value.CachedData;

            Console.WriteLine("Finishing up");

            Console.ReadLine();
        }
    }


}
