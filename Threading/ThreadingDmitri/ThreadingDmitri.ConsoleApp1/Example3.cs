using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingDmitri.ConsoleApp1
{
    // PLINQ
    public class Example3
    {
        public static void First()
        {
            const int count = 50;
            var results = new int[count];

            var items = Enumerable.Range(1, count).ToArray();

            items.AsParallel().ForAll(x =>
            {
                int newValue = x * x * x;
                Console.Write($"{newValue} ({Task.CurrentId})\t");
                results[x - 1] = newValue;
            });
        }
    }
}
