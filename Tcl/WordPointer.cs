using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl.Tcl
{
    public class WordPointer
    {
        public WordPointer(codeEditor.CodeEditor.CodeDocument document, codeEditor.CodeEditor.ParsedDocument parsedDocument)
        {
            this.Document = document;
            this.ParsedDocument = parsedDocument;
            FetchNext(this.Document,ref index, out length, out nextIndex);
        }

        public void Dispose()
        {
            this.Document = null;
            this.ParsedDocument = null;
        }

        public codeEditor.CodeEditor.CodeDocument Document { get; protected set; }
        public codeEditor.CodeEditor.ParsedDocument ParsedDocument { get; protected set; }

        protected int index = 0;
        protected int length = 0;
        protected int nextIndex;


        public WordPointer Clone()
        {
            WordPointer ret = new WordPointer(Document, ParsedDocument);
            ret.index = index;
            ret.length = length;
            ret.nextIndex = nextIndex;
            return ret;
        }

        public void Color(CodeDrawStyle.ColorType colorType)
        {
            for (int i = index; i < index + length; i++)
            {
                Document.SetColorAt(i, CodeDrawStyle.ColorIndex(colorType));
            }
        }

        public int Index
        {
            get
            {
                return index;
            }
        }

        public void AddError(string message)
        {
            if (ParsedDocument == null) return;

            if (ParsedDocument is Tcl.ParsedDocument && (ParsedDocument as Tcl.ParsedDocument).ErrorCount < 100)
            {
                int lineNo = Document.GetLineAt(index);
                ParsedDocument.Messages.Add(new Tcl.ParsedDocument.Message(message, Tcl.ParsedDocument.Message.MessageType.Error, index, lineNo, length, ParsedDocument.ItemID, ParsedDocument.Project));
            }
            else if (ParsedDocument is Tcl.ParsedDocument && (ParsedDocument as Tcl.ParsedDocument).ErrorCount == 100)
            {
                ParsedDocument.Messages.Add(new Tcl.ParsedDocument.Message(">100 errors", Tcl.ParsedDocument.Message.MessageType.Error, 0, 0, 0, ParsedDocument.ItemID, ParsedDocument.Project));
            }

            for (int i = index; i < index + length; i++)
            {
                Document.SetMarkAt(i, 0);
            }

            if (ParsedDocument is Tcl.ParsedDocument) (ParsedDocument as Tcl.ParsedDocument).ErrorCount++;
        }

        public void AddError(WordReference fromReference, string message)
        {
            if (ParsedDocument == null) return;

            if (ParsedDocument is Tcl.ParsedDocument && (ParsedDocument as Tcl.ParsedDocument).ErrorCount < 100)
            {
                int lineNo = Document.GetLineAt(index);
                ParsedDocument.Messages.Add(new Tcl.ParsedDocument.Message(message, Tcl.ParsedDocument.Message.MessageType.Error, index, lineNo, length, ParsedDocument.ItemID, ParsedDocument.Project));
            }
            else if (ParsedDocument is Tcl.ParsedDocument && (ParsedDocument as Tcl.ParsedDocument).ErrorCount == 100)
            {
                ParsedDocument.Messages.Add(new Tcl.ParsedDocument.Message(">100 errors", Tcl.ParsedDocument.Message.MessageType.Error, 0, 0, 0, ParsedDocument.ItemID, ParsedDocument.Project));
            }

            if (fromReference.Document == Document)
            {
                for (int i = fromReference.Index; i < index + length; i++)
                {
                    Document.SetMarkAt(i, 0);
                }
            }
            else
            {
                for (int i = index; i < index + length; i++)
                {
                    Document.SetMarkAt(i, 0);
                }
            }
            if (ParsedDocument is Tcl.ParsedDocument) (ParsedDocument as Tcl.ParsedDocument).ErrorCount++;
        }

        public void AddWarning(string message)
        {
            if (ParsedDocument == null) return;

            if (ParsedDocument is Tcl.ParsedDocument && (ParsedDocument as Tcl.ParsedDocument).WarningCount < 100)
            {
                int lineNo = Document.GetLineAt(index);
                ParsedDocument.Messages.Add(new Tcl.ParsedDocument.Message(message, Tcl.ParsedDocument.Message.MessageType.Warning, index, lineNo, length, ParsedDocument.ItemID, ParsedDocument.Project));
            }
            else if (ParsedDocument is Tcl.ParsedDocument && (ParsedDocument as Tcl.ParsedDocument).WarningCount == 100)
            {
                ParsedDocument.Messages.Add(new Tcl.ParsedDocument.Message(">100 warnings", Tcl.ParsedDocument.Message.MessageType.Warning, 0, 0, 0, ParsedDocument.ItemID, ParsedDocument.Project));
            }

            for (int i = index; i < index + length; i++)
            {
                Document.SetMarkAt(i, 1);
            }
            if (ParsedDocument is Tcl.ParsedDocument) (ParsedDocument as Tcl.ParsedDocument).WarningCount++;
        }

        public void AddWarning(WordReference fromReference, string message)
        {
            if (ParsedDocument == null) return;

            if (ParsedDocument is Tcl.ParsedDocument && (ParsedDocument as Tcl.ParsedDocument).WarningCount < 100)
            {
                int lineNo = Document.GetLineAt(index);
                ParsedDocument.Messages.Add(new Tcl.ParsedDocument.Message(message, Tcl.ParsedDocument.Message.MessageType.Warning, index, lineNo, length, ParsedDocument.ItemID, ParsedDocument.Project));
            }else if (ParsedDocument is Tcl.ParsedDocument && (ParsedDocument as Tcl.ParsedDocument).WarningCount == 100)
            {
                ParsedDocument.Messages.Add(new Tcl.ParsedDocument.Message(">100 warnings", Tcl.ParsedDocument.Message.MessageType.Warning, 0, 0, 0, ParsedDocument.ItemID, ParsedDocument.Project));
            }

            if (fromReference.Document == Document)
            {
                for (int i = fromReference.Index; i < index + length; i++)
                {
                    Document.SetMarkAt(i, 1);
                }
            }
            else
            {
                for (int i = index; i < index + length; i++)
                {
                    Document.SetMarkAt(i, 1);
                }
            }
            if (ParsedDocument is Tcl.ParsedDocument) (ParsedDocument as Tcl.ParsedDocument).WarningCount++;
        }



        

        public void MoveNext()
        {
            index = nextIndex;
            FetchNext(Document, ref index, out length, out nextIndex);

            while (!Eof)
            {
                index = nextIndex;
                FetchNext(Document, ref index, out length, out nextIndex);
            }
        }

        public bool Eof
        {
            get
            {
                if (nextIndex == Document.Length)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string Text
        {
            get
            {
                return Document.CreateString(index, length);
            }
        }

        public int Length
        {
            get
            {
                return length;
            }
        }

        public char GetCharAt(int wordIndex)
        {
            return Document.GetCharAt(index + wordIndex);
        }

        public static void FetchNext(ajkControls.Document document,ref int index, out int length, out int nextIndex)
        {
            // skip blanks before word
            while (document.Length > index && document.GetCharAt(index) == ' ')
            {
                index++;
            }
            if (index == document.Length)
            {
                length = 0;
                nextIndex = index;
                return;
            }

            length = 0;
            nextIndex = index;
            while (document.Length > nextIndex && document.GetCharAt(nextIndex) != ' ')
            {
                nextIndex++;
            }
            length = nextIndex - index;

            //while (document.Length > nextIndex && document.GetCharAt(nextIndex) < 128 && charClass[document.GetCharAt(nextIndex)] == 0)
            //{
            //    nextIndex++;
            //}
        }


    }
}

