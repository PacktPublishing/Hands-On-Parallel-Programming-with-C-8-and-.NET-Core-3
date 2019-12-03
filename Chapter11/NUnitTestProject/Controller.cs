using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestProject
{
    public interface IService
    {
        Task<string> GetDataAsync();
    }
    class Controller
    {
        public Controller(IService service)
        {
            Service = service;
        }

        public IService Service { get; }

        public async Task DisplayData()
        {
            var data =await Service.GetDataAsync();
            Console.WriteLine(data);
        }
    }
}
