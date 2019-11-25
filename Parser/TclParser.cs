using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeEditor.CodeEditor;

namespace pluginTcl.Parser
{
    public class TclParser : codeEditor.CodeEditor.DocumentParser
    {
        public TclParser( Data.TclFile tclFile, codeEditor.CodeEditor.DocumentParser.ParseModeEnum parseMode) : base(tclFile,parseMode)
        {
              parsedDocument = new Tcl.ParsedDocument(tclFile, document.EditID);
              word = new Tcl.WordScanner(this.document, parsedDocument,false);
        }

        public Tcl.WordScanner word;
        private Tcl.ParsedDocument parsedDocument = null;

        public override ParsedDocument ParsedDocument { get { return parsedDocument as codeEditor.CodeEditor.ParsedDocument; } }

        public override void Parse()
        {
            word.GetFirst();
            parsedDocument.Parse(word);
            word.Dispose();
            word = null;
        }
    }
}
