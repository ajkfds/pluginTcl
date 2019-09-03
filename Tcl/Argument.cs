using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl.Tcl
{
    public static class Argument
    {
        public static IArgument ParseCreate(WordScanner word, ParsedDocument parsedDocument)
        {
            if (word.Text == "\n" || word.Text == ";")
            {
                return null;
            }
            switch (word.Text)
            {
                case "[": //  bracket
                    Bracket bracket = Bracket.ParseCreate(word, parsedDocument);
                    return bracket;
                case "{": // brace
                    Brace brace = Brace.ParseCreate(word, parsedDocument);
                    return brace;
                case "\"": // quote
                    Quote quote = Quote.ParseCreate(word, parsedDocument);
                    return quote;
                default:
                    Text text = Text.ParseCreate(word, parsedDocument);
                    return text;
            }
        }
    }
}
