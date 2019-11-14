using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace NUnitTestProject
{
    class ControllerTest
    {
        [Test]
        public async System.Threading.Tasks.Task DisplayDataTestAsync()
        {
            var serviceMock = new Mock<IService>();

            serviceMock.Setup(s => s.GetDataAsync()).Returns(
                Task.FromResult("Some Dummy Value"));

            var controller = new Controller(serviceMock.Object);

            await controller.DisplayData();
        }

        [Test]
        public async System.Threading.Tasks.Task DisplayDataTestAsyncUsingTaskCompletionSource()
        {
            var serviceMock = new Mock<IService>();
            string data = "Some Dummy Value";
            var tcs = new TaskCompletionSource<string>();
            tcs.SetResult(data);

            serviceMock.Setup(s => s.GetDataAsync()).Returns(tcs.Task);

            var controller = new Controller(serviceMock.Object);

            await controller.DisplayData();
        }
        
    }
}
