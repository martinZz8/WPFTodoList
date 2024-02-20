using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Static
{
    internal static class ConsoleLogCollectionItems
    {
        public static void Log(IEnumerable<object> objects)
        {
            int idx = 0;
            foreach (var it in objects)
            {
                Console.Write($"{idx + 1})\n{it}");

                if (idx != objects.Count() - 1)
                {
                    Console.Write("\n\n");
                }
                else
                {
                    Console.Write("\n");
                }

                idx++;
            }
        }
    }
}
