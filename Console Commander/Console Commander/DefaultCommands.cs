using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCommander
{
    static class DefaultCommands
    {
        [RegisterCommand(help: "Lists all available commands")]
        static void Help()
        {
            throw new NotImplementedException();
        }

        [RegisterCommand(help: "Exits the program")]
        static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
