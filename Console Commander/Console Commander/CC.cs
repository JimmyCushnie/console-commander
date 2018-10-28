using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Commander
{
    /// <summary>
    /// Color Console
    /// </summary>
    static class CC
    {
        static void Write(object value, ConsoleColor color = ConsoleColor.Cyan)
        {
            var original = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(value);
            Console.ForegroundColor = original;
        }

        static void WriteLine(object value, ConsoleColor color = ConsoleColor.Cyan)
        {
            var original = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ForegroundColor = original;
        }
    }
}
