using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl.Tcl
{
    public class ParsedDocument : codeEditor.CodeEditor.ParsedDocument
    {
        public ParsedDocument(Data.TclFile tclFile, int editID) : base(tclFile, editID)
        {

        }

        public ProjectProperty ProjectProperty
        {
            get
            {
                // ProjectProperty is initialized in TclFile.ProjectProperty
                // so ParsedDocument.ProjectPrperty must be called via TclFile.ProjectProperty
                Data.TclFile tclFile = Item as Data.TclFile;
                return tclFile.ProjectProperty;
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            Data.TclFile TclFile = Item as Data.TclFile;
        }

        public int ErrorCount = 0;
        public int WarningCount = 0;


        // https://www.tcl.tk/man/tcl7.5/
        /*

        [] : brackets
        {} : braces
        " : quote

        commands ( delimited newlines or semicolons): script
        words delimited by whitespace : command

        \a,b,f,n,t,r,v
        alart backspace feed newline carriage return vertical tab
        oxU

        */
        public List<Tcl.Command> Commands = new List<Command>();

        public void Parse(WordScanner word)
        {
            while (!word.Eof)
            {
                if(word.Text == "\n" || word.Text == ";")
                {
                    word.MoveNext();
                }
                if (word.Text.StartsWith("#"))
                {
                    Tcl.Comment comment = Tcl.Comment.ParseCreate(word, this);
                }
                else
                {
                    Tcl.Command command = Tcl.Command.ParseCreate(word, this);
                    if (command != null) Commands.Add(command);
                }
            }
        }

        public Dictionary<string, Variable> Variables = new Dictionary<string, Variable>();

        public List<codeEditor.CodeEditor.PopupItem> GetPopupItems(int index, string text)
        {
            return null;
        }


        private static List<codeEditor.CodeEditor.AutocompleteItem> TclKeywords = new List<codeEditor.CodeEditor.AutocompleteItem>()
        {
        };

        public List<codeEditor.CodeEditor.AutocompleteItem> GetAutoCompleteItems(int index, int lineStartIndex, int line, CodeEditor.CodeDocument document, out string cantidateWord)
        {
            cantidateWord = "";
            return null;
        }


        private codeEditor.CodeEditor.AutocompleteItem newItem(string text, CodeDrawStyle.ColorType colorType)
        {
            return new codeEditor.CodeEditor.AutocompleteItem(text, CodeDrawStyle.ColorIndex(colorType), CodeDrawStyle.Color(colorType));
        }


        public new class Message : codeEditor.CodeEditor.ParsedDocument.Message
        {
            public Message(string text, MessageType type, int index, int lineNo, int length, string itemID, codeEditor.Data.Project project)
            {
                this.Text = text;
                this.Length = length;
                this.Index = index;
                this.LineNo = lineNo;
                this.Type = type;
                this.Project = project;
            }
            public int LineNo { get; protected set; }

            public enum MessageType
            {
                Error,
                Warning,
                Notice,
                Hint
            }
            public MessageType Type { get; protected set; }

            public override codeEditor.MessageView.MessageNode CreateMessageNode()
            {
                return null;
//                MessageView.MessageNode node = new MessageView.MessageNode(this);

//                return node;
            }

        }
    }

}
