using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl.Tcl.Commands
{
    public interface CommandParser
    {
        string Name { get; }
        void Parse(WordScanner word, ParsedDocument parsedDocument, Command command);
    }
}
