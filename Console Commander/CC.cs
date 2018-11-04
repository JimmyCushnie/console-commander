using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCommander
{
    /// <summary>
    /// Color Console
    /// </summary>
    static class CC
    {
        public static int IndentInterval = 3;

        public static void Write(object value, ConsoleColor color = ConsoleColor.Cyan)
        {
            var original = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(value);
            Console.ForegroundColor = original;
        }

        public static void WriteLine(object value, ConsoleColor color = ConsoleColor.Cyan, int indent = 0)
        {
            var original = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(new string(' ', indent * IndentInterval) + value);
            Console.ForegroundColor = original;
        }

        public static void LineBreak()
        {
            Console.WriteLine();
        }
    }
}
