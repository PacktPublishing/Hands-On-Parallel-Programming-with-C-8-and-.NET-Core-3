using System.Threading;

namespace Ch07
{
    class _1Eager
    {
        Data _cachedData;

        public _1Eager()
        {
            _cachedData = GetDataFromDatabase();
        }

        public Data GetOrCreate()
        {           
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
