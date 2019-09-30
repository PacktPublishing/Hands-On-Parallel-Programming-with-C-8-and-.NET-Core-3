using System;
using System.Collections.Generic;
using System.Text;

namespace Ch13
{
    public class LazySingleton<T> where T : class
    {
        static object _syncObj = new object();

        static T _value;
        private LazySingleton()
        {

        }

        public static T Value
        {
            get
            {
                if (_value == null)
                {
                    lock (_syncObj)
                    {
                        if (_value == null)
                            _value = SomeHeavyCompute();
                    }
                }
                return _value;
            }
        }

        private static T SomeHeavyCompute() { return default(T); }
    }

    public class MyLazySingleton<T>
    {
        static Lazy<T> _value = new Lazy<T>(SomeHeavyCompute);
        public T Value { get { return _value.Value; } }
        private static T SomeHeavyCompute() { return default(T); }
    }


}
