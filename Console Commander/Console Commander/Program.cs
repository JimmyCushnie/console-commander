using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;

namespace ConsoleCommander
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterCommands();

            string title = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            Console.Title = title;

            CC.WriteLine($"Welcome to {title}. Type help for a list of commands.");
            CC.LineBreak();

            while (true)
            {
                var command = Console.ReadLine();

                try { RunCommand(command); }
                catch(Exception e)
                {
                    CC.WriteLine("ERROR: " + e.Message, ConsoleColor.Red);
                }

                CC.LineBreak();
            }

        }


        public static Dictionary<string, MethodInfo> RegisteredCommands { get; private set; } = new Dictionary<string, MethodInfo>();
        static void RegisterCommands()
        {
            var methodFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                foreach (var method in type.GetMethods(methodFlags))
                {
                    var attribute = Attribute.GetCustomAttribute(method, typeof(RegisterCommandAttribute)) as RegisterCommandAttribute;

                    if (attribute == null) continue;

                    if (String.IsNullOrEmpty(attribute.Name))
                        attribute.Name = method.Name;

                    RegisteredCommands.Add(attribute.Name.ToLower(), method);
                }
            }
        }

        static void RunCommand(string command)
        {
            var split = new List<string>(command.Split(' '));

            var method = RegisteredCommands[split[0]];
            split.RemoveAt(0);

            var parameters = method.GetParameters();
            var bois = new object[parameters.Length];

            for (int i = 0; i < split.Count; i++)
                bois[i] = Parser.Parse(split[i], parameters[i].ParameterType);

            method.Invoke(null, bois);
        }
    }

 
}
