using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static async IAsyncEnumerable<int> GetBigResultsAsync()
        {
            var list = Enumerable.Range(1, 20);


            await foreach (var item in list.AsEnumerable())
            {
                yield return item;
            }
        }
        async static Task Main(string[] args)
        {
            await foreach (var dataPoint in GetBigResultsAsync())
            {
                Console.WriteLine(dataPoint);
            }
            Console.WriteLine("Hello World!");
        }
    }


    class CustomAsyncIntegerCollection : IAsyncEnumerable<int>
    {
        List<int> _numbers;
        public CustomAsyncIntegerCollection(IEnumerable<int> numbers)
        {
            _numbers = numbers.ToList();
        }


        public IAsyncEnumerator<int> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new OddIndexEnumerator(_numbers);
        }
    }
    public static class CollectionExtensions
    {
        public static IAsyncEnumerable<int> AsEnumerable(this IEnumerable<int> source) => new CustomAsyncIntegerCollection(source);
    }
    class OddIndexEnumerator : IAsyncEnumerator<int>
    {
        List<int> _numbers;
        int _currentIndex = -1;

        public OddIndexEnumerator(IEnumerable<int> numbers)
        {
            _numbers = numbers.ToList();
        }
        public int Current
        {
            get
            {
                Task.Delay(2000);

                return _numbers[_currentIndex];

            }
        }

        public ValueTask DisposeAsync()
        {
            return new ValueTask(Task.CompletedTask);
        }

        public ValueTask<bool> MoveNextAsync()
        {
            Task.Delay(2000);
            if (_currentIndex < _numbers.Count() - 2)
            {
                _currentIndex += 2;
                return new ValueTask<bool>(Task.FromResult<bool>(true));

            }

            return new ValueTask<bool>(Task.FromResult<bool>(false));
        }
    }
}
