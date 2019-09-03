using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl.Tcl
{
    public class Comment : Command
    {
        protected Comment() { }
        public static new Comment ParseCreate(WordScanner word,ParsedDocument parsedDocument)
        {
            Comment comment = new Comment();

            word.Color(CodeDrawStyle.ColorType.Comment);
            word.MoveNext();

            while (!word.Eof)
            {
                word.Color(CodeDrawStyle.ColorType.Comment);
                word.MoveNext();
                if (word.Text == "\n" || word.Text == ";")
                {
                    word.MoveNext();
                    break;
                }
            }
            return comment;
        }
    }
}
