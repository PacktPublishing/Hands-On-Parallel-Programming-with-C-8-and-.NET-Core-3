using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace MsTestUnitTestCaseProject
{
    [TestClass]
    public class UnitTest1
    {
        private async Task<int> SomeFunction()
        {
            int result = await Task.Run(() =>
            {
                Thread.Sleep(1000);
                return 5;
            });

            return result;
        }

        [TestMethod]

        public void SomeFunctionWillReturn5AsWeUseResultToLetItFinish()
        {
            var result = SomeFunction().Result;
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public async void SomeFunctionShouldFailAsExpectedValueShouldBe5AndNot3()
        {
            var result = await SomeFunction();
            Assert.AreEqual(3, result);
        }
    }
}
