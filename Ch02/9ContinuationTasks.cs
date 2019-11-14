using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Ch02
{
    class _9ContinuationTasks
    {
        public static void Main()
        {
            
            SimpleChain();
            Console.ReadLine();
            //PartialResponsive();
            ContinueWhenAll();
            ContinueWhenAny();
        }

        private static void ContinueWhenAny()
        {
            int number = 13;
            Task<bool> taskA = Task.Factory.StartNew<bool>(() => number / 2 != 0);
            Task<bool> taskB = Task.Factory.StartNew<bool>(() => (number / 2) * 2 != number);
            Task<bool> taskC = Task.Factory.StartNew<bool>(() => (number & 1) != 0);
            Task.Factory.ContinueWhenAny<bool>(new Task<bool>[] { taskA, taskB, taskC }, (task) =>
            {
                Console.WriteLine((task as Task<bool>).Result);
            }
          );        
        }

        private async static void ContinueWhenAll()
        {
            int a = 2, b = 3;
            Task<int> taskA = Task.Factory.StartNew<int>(() => a * a);
            Task<int> taskB = Task.Factory.StartNew<int>(() => b * b);
            Task<int> taskC = Task.Factory.StartNew<int>(() => 2 * a * b);
            var sum = await Task.Factory.ContinueWhenAll<int>(new Task[] { taskA, taskB, taskC }, (tasks) =>tasks.Sum(t => (t as Task<int>).Result));
            Console.WriteLine(sum);
        }

        private async static void PartialResponsive()
        {
            var data = await FetchDataAsync();
        }

        private static void SimpleChain()
        {
            var task = Task.Factory.StartNew<DataTable>(() => 
            {
                Console.WriteLine("Fetching Data");
                return FetchData();
                }).ContinueWith(
                (e) => {
                    var firstRow = e.Result.Rows[0];
                    Console.WriteLine($"Id is {firstRow["Id"]} and Name is {firstRow["Name"]}" );
                    });
           
        }

        static async Task<DataTable> FetchDataAsync()
        {
            //Simulate long running task
            //.......
            //Some dummy data
            return await Task.FromResult( new DataTable());
        }
        static DataTable FetchData()
        {
            //Simulate long running task
           //.....
            //Some dummy data
            var dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Name");
            dataTable.Rows.Add(new object[] {"1", "Shakti"});
            return dataTable;
        }

        static void Display(DataTable data)
        {
            //Render UI
        }
    }
}
