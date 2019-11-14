using System.Threading;

namespace Ch07
{
    class _3ThreadSafeSimpleLazy
    {
        Data _cachedData;

        static object _locker = new object();

        public Data GetOrCreate()
        {
            var data = _cachedData;

            if (data == null)
            {
                lock (_locker)
                {
                    data = _cachedData;
                    if (data == null)
                    {
                        data = GetDataFromDatabase();
                        _cachedData = data;
                    }
                }
            }
            return _cachedData;
        }

        private Data GetDataFromDatabase()
        {
            //Dummy Delay
            Thread.Sleep(5000);
            return new Data();
        }

        public void ResetCache()
        {
            _cachedData = null;
        }
    }
}
