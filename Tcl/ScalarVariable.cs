using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl.Tcl
{
    public class ScalarVariable : Variable
    {
        public ScalarVariable(string name)
        {
            Name = name;
        }

        public string Value { get; set; }
    }
}
