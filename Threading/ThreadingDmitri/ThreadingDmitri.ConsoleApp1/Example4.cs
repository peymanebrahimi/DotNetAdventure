using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingDmitri.ConsoleApp1
{
    public class Example4
    {
        // ManualResetEvent
        private static ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        private static Stopwatch sw = new Stopwatch();

        public static void DoMain()
        {
            Method1();
            Method2();

            sw.Start();
            while (true)
            {
                Thread.Sleep(5000);
                manualResetEvent.Set();
            }
        }



        private async static Task Method1()
        {
            await new TaskFactory().StartNew(() =>
            {
                while (true)
                {
                    manualResetEvent.WaitOne();
                    Console.WriteLine($"Method 1 has done its operation in {sw.Elapsed.Seconds} seconds ");
                    manualResetEvent.Reset();
                }
            });
        }

        private async static Task Method2()
        {
            await new TaskFactory().StartNew(() =>
            {
                while (true)
                {
                    manualResetEvent.WaitOne();
                    Console.WriteLine($"Method 2 has done its operation in {sw.Elapsed.Seconds} seconds ");
                    manualResetEvent.Reset();
                }
            });
        }
    }

}