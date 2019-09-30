using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;

namespace IAsyncResultOldFramework
{
    class Program
    {
        public delegate int SumDelegate(int x, int y);

        static void Main(string[] args)
        {
            AsyncCallback callback = new AsyncCallback(MyCallback);
            int state = 1000;
            SumDelegate d = new SumDelegate(Add);
            d.BeginInvoke(100, 200, callback, state);
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }

        public static int Add(int a, int b)
        {
            return a + b;
        }

        public static void MyCallback(IAsyncResult result)
        {
            AsyncResult ar = (AsyncResult)result;
            SumDelegate d = (SumDelegate)ar.AsyncDelegate;
            int state = (int)ar.AsyncState;
            int i = d.EndInvoke(result);
            Console.WriteLine(i);
            Console.WriteLine(state);
            Console.ReadLine();
        }
    }
}