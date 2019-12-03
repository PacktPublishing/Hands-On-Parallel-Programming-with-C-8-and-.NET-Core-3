using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch04
{
    class _5CombiningSequential
    {
        public static void Main()
        {
            var range = Enumerable.Range(1, 1000);
            range.AsParallel().Where(i => i % 2 == 0).
                AsSequential().Where(i => i % 8 == 0).
                AsParallel().OrderBy(i => i);
        }
    }
}
