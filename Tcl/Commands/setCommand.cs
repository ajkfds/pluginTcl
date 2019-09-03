using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl.Tcl.Commands
{
    public class setCommand : Command
    {
        public override string Name { get
            {
                return "set";
            }
        }
        // set varName ?value?
        /*
        Returns the value of variable varName.
        If value is specified, then set the value of varName to value, 
        creating a new variable if one doesn't already exist, and return its value. 

        If varName contains an open parenthesis and ends with a close parenthesis, 
        then it refers to an array element: the characters before the first open parenthesis are the name of the array, and the characters between the parentheses are the index within the array. 
        Otherwise varName refers to a scalar variable. 

        If no procedure is active, then varName refers to a global variable. 
        If a procedure is active, then varName refers to a parameter or local variable of the procedure unless the global command has been invoked to declare varName to be global.
        */
        public new static Command ParseCreate(WordScanner word, ParsedDocument parsedDocument)
        {
            setCommand command = new setCommand();

            word.Color(CodeDrawStyle.ColorType.Keyword);
            word.MoveNext();

            if (word.Eof || word.Text == "\n" || word.Text == ":")
            {
                word.AddError("varName expected");
                return null;
            }

            string varName = word.Text;
            word.Color(CodeDrawStyle.ColorType.Net);
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

    }
}
