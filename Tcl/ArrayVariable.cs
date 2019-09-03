using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl.Tcl
{
    public class ArrayVariable : Variable
    {
        public ArrayVariable(string name)
        {
            Name = name;
        }

        public string GetVariable(List<string> indexList)
        {
            if (Variables.ContainsKey(indexList))
            {
                return Variables[indexList];
            }
            else
            {
                return "";
            }
        }

        public Dictionary<List<string>, string> Variables = new Dictionary<List<string>, string>();

    }
}
