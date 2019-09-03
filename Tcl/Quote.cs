using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl.Tcl
{
    public class Quote : IArgument
    {
        public static Quote ParseCreate(WordScanner word, ParsedDocument parsedDocument)
        {
            Quote quote = new Quote();
            word.Color(CodeDrawStyle.ColorType.Keyword);
            word.MoveNext();

            while (!word.Eof)
            {
                if (word.Text == "\"")
                {
                    word.Color(CodeDrawStyle.ColorType.Keyword);
                    word.MoveNext();
                    break;
                }
                word.MoveNext();
            }
            return quote;
        }
    }
}
