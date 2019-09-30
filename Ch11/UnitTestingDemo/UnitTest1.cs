using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestingDemo
{
    public class Chapter11UnitTest
    {
        private async Task<int> SomeFunction()
        {
            int result =await Task.Run(() =>
            {
                Thread.Sleep(1000);
                return 5;
            });
            
            return result;
        }
      
        [Fact]
        public void SomeFunctionWillReturn5AsWeUseResultToLetItFinish()
        {
            var result = SomeFunction().Result;
            Assert.Equal(5, result);
        }

        [Fact]
        public async void SomeFunctionWillReturn5AsCallIsAwaited()
        {
            var result = await SomeFunction();
            Assert.Equal(5, result);
        }
    }
}
