using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ch13
{
    class Program
    {
        static void Main(string[] args)
        {
            //MapReduceTest();
            //AggregationTestSimple();
            //AggregationTestParallel();
            //AggregationTestPLinq();
            //AggregationTestConcurrent();
          //  SpeculativeForEachTest();

            LazyDemo();
            Console.ReadLine();
        }

        private static void LazyDemo()
        {
            Lazy<Task<string>> lazy = new Lazy<Task<string>>(Task.Factory.StartNew(GetDataFromService));
            lazy.Value.ContinueWith((s)=> SaveToText(s.Result));
            lazy.Value.ContinueWith((s) => SaveToCsv(s.Result));
        }
        public static string GetDataFromService()
        {
            Console.WriteLine("Service called");
            return "Some Dummy Data";
        }
        public static void SaveToText(string data)
        {
            Console.WriteLine("Save to Text called");

            //Save to Text
        }
        public static void SaveToCsv(string data)
        {
            Console.WriteLine("Save to CSV called");

            //Save to CSV
        }

        private static void SpeculativeForEachTest()
        {
            Func<string> Square = () => {
                Console.WriteLine("Square Called");
                return $"Result From Square is {5 * 5}";
                };
            Func<string> Square2 = () =>
             {
                 Console.WriteLine("Square2 Called");

                 var square = 0;
                 for (int j = 0; j < 5; j++)
                 {
                     square += 5;
                 }
                 return $"Result From Square2 is {square}";
             };

           string result = SpeculativeInvoke(Square, Square2);
            Console.WriteLine(result);
        }
        public static T SpeculativeInvoke<T>(params Func<T>[] functions)
        {
            return SpeculativeForEach(functions, function => function());
        }
        public static TResult SpeculativeForEach<TSource, TResult>(
                        IEnumerable<TSource> source,
                        Func<TSource, TResult> body)
        {
            object result = null;
            Parallel.ForEach(source, (item, loopState) =>
            {
                result = body(item);
                loopState.Stop();
            });
            return (TResult)result;
        }
        private static void AggregationTestConcurrent()
        {
                var input = Enumerable.Range(1, 50);
                Func<int, int> action = (i) => i * i;
                var output = new ConcurrentBag<int>();
                Parallel.ForEach(input, item =>
                {
                    var result = action(item);
                    output.Add(result);
                });
        }

        private static void AggregationTestPLinq()
        {
            var input = Enumerable.Range(1, 50);
            Func<int, int> action = (i) => i * i;
            var output = input.AsParallel()
                                .Select(item => action(item))
                                .ToList();
        }

        private static void AggregationTestParallel()
        {
            var output = new List<int>();
            var input = Enumerable.Range(1, 50);
            Func<int, int> action = (i) => i * i;

            Parallel.ForEach(input, item =>
            {
                var result = action(item);
                lock (output) output.Add(result);
            });
        }

        private static void AggregationTestSimple()
        {
            var output = new List<int>();
            var input = Enumerable.Range(1, 50);
            Func<int,int> action = (i) => i * i;
            foreach (var item in input)
            {
                var result = action(item);
                output.Add(result);
            }

            
        }

        private static void MapReduceTest()
        {
            //Maps only positive number from list
            Func<int, IEnumerable<int>> mapPositiveNumbers = number =>
            {
                IList<int> positiveNumbers = new List<int>();
                if (number > 0)
                    positiveNumbers.Add( number);
                return positiveNumbers;
            };

            // Group results together
            Func<int, int> groupNumbers = value => value;
            //Reduce function that counts the occurence of each number
            Func<IGrouping<int, int>,IEnumerable<KeyValuePair<int, int>>> reduceNumbers =
                grouping => new[] { new KeyValuePair<int, int>( grouping.Key, grouping.Count()) };

            // Generate a list of random numbers between -10 and 10
            IList<int> sourceData = new List<int>();
            var rand = new Random();
            for (int i = 0; i < 1000; i++)
            {
                sourceData.Add(rand.Next(-10, 10));
            }

            // Use MapReduce function
            var result = sourceData.AsParallel().MapReduce(           
                                                mapPositiveNumbers,
                                                groupNumbers,
                                                reduceNumbers);
            // process the results
            foreach (var item in result)
            {
                Console.WriteLine("{0} came {1} times",
                item.Key,
                item.Value);
            }
                     
        }
    }
}
