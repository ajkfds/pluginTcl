﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl.Tcl
{
    public class Bracket : IArgument
    {
        public static Bracket ParseCreate(WordScanner word, ParsedDocument parsedDocument)
        {
            Bracket bracket = new Bracket();
            word.Color(CodeDrawStyle.ColorType.Keyword);
            word.MoveNext();

            while (!word.Eof)
            {
                if (word.Text == "]")
                {
                    word.Color(CodeDrawStyle.ColorType.Keyword);
                    word.MoveNext();
                    break;
                }
                word.MoveNext();
            }
            return bracket;

        }
    }
}
