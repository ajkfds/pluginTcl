using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl.Tcl
{
    public class Brace : IArgument
    {
        public static Brace ParseCreate(WordScanner word, ParsedDocument parsedDocument)
        {
            Brace brace = new Brace();
            word.Color(CodeDrawStyle.ColorType.Keyword);
            word.MoveNext();

            while (!word.Eof)
            {
                if (word.Text == "}")
                {
                    word.Color(CodeDrawStyle.ColorType.Keyword);
                    word.MoveNext();
                    break;
                }
                word.MoveNext();
            }
            return brace;
        }

    }
}
