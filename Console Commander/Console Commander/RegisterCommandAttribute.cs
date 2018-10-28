using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Commander
{
    class RegisterCommandAttribute : Attribute
    {
        public string Name { get; set; }
        public string Help { get; set; }

        public RegisterCommandAttribute(string name = "", string help = "")
        {
            Name = name;
            Help = help;
        }
    }
}
