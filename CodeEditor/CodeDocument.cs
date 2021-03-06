﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl.CodeEditor
{
    public class CodeDocument : codeEditor.CodeEditor.CodeDocument
    {
        public CodeDocument(Data.TclFile tclFile) : base(tclFile) { }

        // get word boundery for editor word selection

        public override void GetWord(int index, out int headIndex, out int length)
        {
            headIndex = index;
            length = 0;
            char ch = GetCharAt(index);
            if (ch == ' ' || ch == '\r' || ch == '\n' || ch == '\t') return;

            while (headIndex >= 0)
            {
                ch = GetCharAt(headIndex);
                if (ch == ' ' || ch == '\r' || ch == '\n' || ch == '\t')
                {
                    break;
                }
                headIndex--;
            }
            headIndex++;
            if (index < headIndex) headIndex = index;

            int nextIndex;
            Tcl.WordPointer.FetchNext(this, ref headIndex, out length, out nextIndex);

            while(nextIndex <= index && index < Length)
            {
                headIndex = nextIndex;
                Tcl.WordPointer.FetchNext(this, ref headIndex, out length, out nextIndex);
            }
        }

        public List<string> GetHierWords(int index)
        {
            List<string> ret = new List<string>();
            int headIndex = GetLineStartIndex(GetLineAt(index));
            int length;
            int nextIndex = headIndex;

            // return blank if on space char
            char ch = GetCharAt(index);
            if( ch == ' ' || ch == '\t') return new List<string>();

            // get words on the index line until index
            while (headIndex < Length)
            {
                Tcl.WordPointer.FetchNext(this, ref headIndex, out length, out nextIndex);
                if (length == 0) break;
                if (headIndex > index) break;
                ret.Add(CreateString(headIndex, length));
                headIndex = nextIndex;
            }

            // search wors from end
            int i= ret.Count - 1;
            if (i >= 0 && ret[i] != ".")
            {
                i--; // skip last non . word
            }

            while (i>=0)
            {
                if (ret[i] != ".") break; // end if not .
                ret.RemoveAt(i);
                i--;

                if (i == 0) break;
                i--;
            }

            for(int j = 0; j <= i; j++) // remove before heir description
            {
                ret.RemoveAt(0);
            }

            return ret;
        }

        public string GetIndentString(int index)
        {
            StringBuilder sb = new StringBuilder();
            int line = GetLineAt(index);
            int headIndex = GetLineStartIndex(GetLineAt(index));
            int lineLength = GetLineLength(line);

            int i = headIndex;
            while( i < headIndex + lineLength)
            {
                if (GetCharAt(i) != '\t' && GetCharAt(i) != ' ') break;
                sb.Append(GetCharAt(i));
                i++;
            }
            return sb.ToString();
        }

    }
}
