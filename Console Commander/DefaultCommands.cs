using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCommander
{
    static class DefaultCommands
    {
        [RegisterCommand(help: "lists all available commands")]
        static void Help()
        {
            foreach(var boi in Terminal.RegisteredCommands)
            {
                var help = (Attribute.GetCustomAttribute(boi.Value, typeof(RegisterCommandAttribute)) as RegisterCommandAttribute).Help;
                CC.WriteLine(string.Format("{0}: {1}", boi.Key.PadRight(16, ' '), help), ConsoleColor.Blue);
            }
        }

        [RegisterCommand(help: "exits the program")]
        static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
