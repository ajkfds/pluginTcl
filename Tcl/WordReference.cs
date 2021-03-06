﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl.Tcl
{
    public class WordReference
    {
        public WordReference(codeEditor.CodeEditor.CodeDocument document, codeEditor.CodeEditor.ParsedDocument parsedDocument,int index,int length)
        {
            Document = document;
            ParsedDocument = parsedDocument;
            Index = index;
            Length = length;
        }

        public codeEditor.CodeEditor.CodeDocument Document { get; protected set; }
        public codeEditor.CodeEditor.ParsedDocument ParsedDocument { get; protected set; }
        public int Index { get; protected set; }
        public int Length { get; protected set; }

        public void AddError(string message)
        {
            if (ParsedDocument == null) return;
            int lineNo = Document.GetLineAt(Index);

//            ParsedDocument.Messages.Add(new Tcl.ParsedDocument.Message(message, Tcl.ParsedDocument.Message.MessageType.Error, Index, lineNo, Length, ParsedDocument.ItemID, ParsedDocument.Project));
            for (int i = Index; i < Index + Length; i++)
            {
                Document.SetMarkAt(i, 0);
            }
        }

        public void AddWarning(string message)
        {
            if (ParsedDocument == null) return;
            int lineNo = Document.GetLineAt(Index);

//            ParsedDocument.Messages.Add(new Tcl.ParsedDocument.Message(message, Tcl.ParsedDocument.Message.MessageType.Warning, Index, lineNo, Length, ParsedDocument.ItemID, ParsedDocument.Project));
            for (int i = Index; i < Index + Length; i++)
            {
                Document.SetMarkAt(i, 1);
            }
        }
    }
}
