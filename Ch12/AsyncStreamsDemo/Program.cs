using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace AsyncStreamsDemo
{
    class Program
    {
        static async IAsyncEnumerable<int> FetchIOTData()
        {
            for (int i = 1; i <= 10; i++)
            {
                await Task.Delay(1000);//Simulate waiting for data to come through. 
                yield return i;
            }
        }
        //static async IAsyncEnumerable<int> GetBigResultsAsync()
        //{
        //    var list = Enumerable.Range(1, 20);


        //    await foreach (var item in list.AsEnumerable())
        //    {
        //        yield return item;
        //    }
        //}
        static async Task Main(string[] args)
        {
        //(Stream stream, long checksum) = CreateStream();

        //long c = 0L;

        //await foreach(byte b in stream.AsEnumerable())
        //{
        //    unchecked { c += b; }
        //}

        //if (c == checksum)
        //{
        //    Console.WriteLine("Checksums match!");
        //}
               

            //await foreach (var item in values)
            //{
            //    Console.WriteLine($"Thread Id {Task.CurrentId}  is {Thread.CurrentThread.ManagedThreadId}");

            //    Console.WriteLine(item);
            //}
                
           Console.WriteLine("Hello World!");

            //(Stream stream, long checksum) = CreateStream();

            //  long c = 0L;

            //await foreach (byte b in stream.AsEnumerable())
            //{
            //    unchecked { c += b; }
            //}

            //if (c == checksum)
            //{
            //    Console.WriteLine("Checksums match!");
            //}
            Console.ReadLine();
        }

        static IEnumerable<string> ProcessFile()
        {
            foreach (var line in File.ReadAllLines("data.txt"))
            {
                ProcessLine(line);
                yield return line;
            }
        }
        private static (Stream stream, long checkSum) CreateStream()
        {
            long checksum = 0L;

            byte[] bytes = new byte[20000];

            for (int i = 0; i < bytes.Length; i++)
            {
                byte value = (byte)(i % byte.MaxValue);
                bytes[i] = value;

                unchecked { checksum += value; }
            }

            MemoryStream stream = new MemoryStream(bytes);
            return (stream, checksum);
        }

        //static async IAsyncEnumerable<string> ProcessFileAsync()
        //{
        //    await foreach (var line in File.ReadAllLines("data.txt").AsyncEnumerable())
        //    {
        //        ProcessLine(line);
        //        yield return line;
        //    }
        //}

        private static void ProcessLine(string line)
        {
            Console.WriteLine(line);
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
        int _currentIndex = 1;

        public OddIndexEnumerator(IEnumerable<int> numbers)
        {
            _numbers = numbers.ToList() ;
        }
        public int Current
        {
            get
            {
                Console.WriteLine($"Thread Id is {Thread.CurrentThread.ManagedThreadId}");
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
            if (_currentIndex < _numbers.Count() -2)
            {
                _currentIndex+=2;
                return new ValueTask<bool>(Task.FromResult<bool>(true));

            }

            return new ValueTask<bool>(Task.FromResult<bool>(false));
        }
    }

     
}
