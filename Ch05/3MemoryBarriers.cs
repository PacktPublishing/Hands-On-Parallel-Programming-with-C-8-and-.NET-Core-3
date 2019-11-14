using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Ch05
{
    class _3MemoryBarriers
    {
        static int a = 1, b = 2, c = 0;

        public static void Main()
        {
            BarrierUsingTheadBarrier();
            BarrierUsingInterlockedBarrier();
            BarrierUsingInterlockedProcessWideBarrier();
        }

        private static void BarrierUsingInterlockedProcessWideBarrier()
        {
            b = c;
            Interlocked.MemoryBarrierProcessWide();
            a = 1;
        }

        private static void BarrierUsingInterlockedBarrier()
        {
            b = c;
            Interlocked.MemoryBarrier();
            a = 1;
        }


        private static void BarrierUsingTheadBarrier()
        {
            b = c;
            Thread.MemoryBarrier();
            a = 1;
        }
    }
}
