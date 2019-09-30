using System;

namespace Ch07
{
    class Program
    {
        static void Main(string[] args)
        {
            _2SimpleLazy lazy = new _2SimpleLazy();
            var data = lazy.GetOrCreate();
            data = lazy.GetOrCreate();

            DataWrapper dataWrapper = new DataWrapper();

            Console.ReadLine();
        }

      
    }
}
