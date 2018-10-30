﻿using System;
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
        public static void Write(object value, ConsoleColor color = ConsoleColor.Cyan)
        {
            var original = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(value);
            Console.ForegroundColor = original;
        }

        public static void WriteLine(object value, ConsoleColor color = ConsoleColor.Cyan)
        {
            var original = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ForegroundColor = original;
        }

        public static void LineBreak()
        {
            Console.WriteLine();
        }
    }
}