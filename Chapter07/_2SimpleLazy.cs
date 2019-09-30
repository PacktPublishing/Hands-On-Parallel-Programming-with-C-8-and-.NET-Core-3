using System;
using System.Threading;

namespace Ch07
{
    class _2SimpleLazy
    {
        Data _cachedData;

        public _2SimpleLazy()
        {
            Console.WriteLine("Constructor called");
        }

        public Data GetOrCreate()
        {
            if (_cachedData == null)
            {
                Console.WriteLine("Initializing object");
                _cachedData = GetDataFromDatabase();
            }
            Console.WriteLine("Data returned from cache");
            return _cachedData;
        }

        private Data GetDataFromDatabase()
        {
            //Dummy Delay
            Thread.Sleep(5000);
            return new Data();
        }
    }
}
