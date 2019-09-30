using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        private async Task<float> GetDivisionAsync(int number , int divisor)
        {
            if (divisor == 0)
            {
                throw new DivideByZeroException();
            }

            int result = await Task.Run(() =>
            {
                Thread.Sleep(1000);
                return number / divisor;
            });

            return result;
        }
        [Test]
        public async Task GetDivisionAsyncShouldReturnSuccessIfDivisorIsNotZero()
        {
            int number = 20;
            int divisor = 4;
            var result = await GetDivisionAsync(number, divisor);

            Assert.AreEqual(result, 5);
        }

        [Test]
        public void GetDivisionAsyncShouldCheckForExceptionIfDivisorIsNotZero()
        {
            int number = 20;
            int divisor = 0;
            Assert.ThrowsAsync<DivideByZeroException>(async () => await GetDivisionAsync(number, divisor));
        }

     
        private async Task<int> SomeFunction()
        {
            int result = await Task.Run(() =>
            {
                Thread.Sleep(1000);
                return 5;
            });

            return result;
        }

        [Test]
        public void SomeFunctionWillReturn5AsWeUseResultToLetItFinish()
        {
            var result = SomeFunction().Result;
            Assert.AreEqual(5, result);
        }

        [Test]
        public async void SomeFunctionWillReturn5AsCallIsAwaited()
        {
            var result = await SomeFunction();
            Assert.AreEqual(3, result);
        }
    }
}