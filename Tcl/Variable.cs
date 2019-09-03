using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl.Tcl
{
    public class Variable
    {
        public virtual string Name { get; protected set; }

        public Variable Create(string varName)
        {
            if(varName.EndsWith(")") && varName.Contains("("))
            {
                string name = varName.Substring(0,varName.IndexOf("("));
                string index = varName.Substring(varName.IndexOf("(") + 1);
                index = index.Substring(0, index.Length);
                index = index.Replace(".", ",");
                string[] indexes = index.Split(new char[] { ',' });
                return new ArrayVariable(varName);
            }
            else
            {
                return new ScalarVariable(varName);
            }
        }
    }
}
