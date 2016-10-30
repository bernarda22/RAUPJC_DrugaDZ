using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SestiZadatak
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Parallel.For(0, 1000, (i) =>
            {
                var x = 2;
                var y = 2;
                var sum = x + y;
            });
            stopwatch.Stop();
            Console.WriteLine(" Parallel calls finished {0} ms.",
            stopwatch.Elapsed.TotalMilliseconds);

            stopwatch.Restart();
            for (int i = 0; i < 1000; i++)
            {
                int x = 2;
                int y = 2;
                int sum = x + y;
            }
            stopwatch.Stop();
            Console.WriteLine(" Sync operation calls finished {0} ms.",
            stopwatch.Elapsed.TotalMilliseconds);

            Console.ReadLine();
        }

        public static void LongOperation(string taskName)
        {
            Thread.Sleep(1000);
            Console.WriteLine("{0} Finished . Executing Thread : {1}", taskName,
            Thread.CurrentThread.ManagedThreadId);
        }
    }
}
