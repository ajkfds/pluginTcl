using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl.Tcl
{
    public class Text : IArgument
    {
        public string Value;
        public static Text ParseCreate(WordScanner word, ParsedDocument parsedDocument)
        {
            Text text = new Text();
            text.Value = word.Text;
            word.MoveNext();

            return text;
        }
    }
}