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

        public List<IArgument> Arguments = new List<IArgument>();
        public static Command ParseCreate(WordScanner word, ParsedDocument parsedDocument)
        {
            if (BuiltInCommandParsers.ContainsKey(word.Text))
            {
                Command bcommand = BuiltInCommandParsers[word.Text](word, parsedDocument);
                return bcommand;
            }

            Command command = new Command();
            command.Name = word.Text;
            word.Color(CodeDrawStyle.ColorType.Keyword);
            word.MoveNext();

            while (!word.Eof)
            {
                if (word.Text == "\n" || word.Text == ";")
                {
                    word.MoveNext();
                    break;
                }

                IArgument argument = Argument.ParseCreate(word, parsedDocument);
                if (argument != null) command.Arguments.Add(argument);
            }

            return command;
        }

        public virtual string Name { get; protected set; }

        // https://www.tcl.tk/man/tcl7.5/TclCmd/contents.html
        public static Dictionary<string, Func<WordScanner, ParsedDocument, Command>> BuiltInCommandParsers
            = new Dictionary<string, Func<WordScanner, ParsedDocument, Command>>
                {
                    {"set", new Func<WordScanner, ParsedDocument, Command>((w, p) => {return Commands.setCommand.ParseCreate(w, p); }) }
                };
    }
}
