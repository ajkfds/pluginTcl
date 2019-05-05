using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl.Tcl
{
    public class Command
    {
        protected Command() { }
        public static Command ParseCreate(WordScanner word)
        {
            // skip \n
            while(!word.Eof && word.Text == "\n")
            {
                word.MoveNext();
            }

            if (word.Eof) return null;


            Command command = new Command();

            command.Name = word.Text;
            word.Color(CodeDrawStyle.ColorType.Keyword);
            word.MoveNext();

            while (!word.Eof)
            {
                word.MoveNext();
                if (word.Text == "\n") break;
            }

            return command;
        }

        public string Name { get; protected set; }

    }
}
