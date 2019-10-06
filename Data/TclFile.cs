using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeEditor.CodeEditor;
using codeEditor.Data;

namespace pluginTcl.Data
{
    public class TclFile : codeEditor.Data.File, codeEditor.Data.ITextFile
    {
        public new static TclFile Create(string relativePath, codeEditor.Data.Project project)
        {
            string id = GetID(relativePath, project);
            if (project.IsRegistered(id))
            {
                TclFile item = project.GetRegisterdItem(id) as TclFile;
                project.RegisterProjectItem(item);
                return item;
            }

            TclFile fileItem = new TclFile();
            fileItem.Project = project;
            fileItem.ID = id;
            fileItem.RelativePath = relativePath;
            if (relativePath.Contains('\\'))
            {
                fileItem.Name = relativePath.Substring(relativePath.LastIndexOf('\\') + 1);
            }
            else
            {
                fileItem.Name = relativePath;
            }
            fileItem.ParseRequested = true;

            project.RegisterProjectItem(fileItem);
            return fileItem;
        }
        private volatile bool parseRequested = false;
        public bool ParseRequested { get { return parseRequested; } set { parseRequested = value; } }

        private volatile bool reloadRequested = false;
        public bool ReloadRequested { get { return reloadRequested; } set { reloadRequested = value; } }
        public bool IsCodeDocumentCashed
        {
            get { if (document == null) return false; else return true; }
        }
        public void Reload()
        {
            CodeDocument = null;
        }

        public codeEditor.CodeEditor.ParsedDocument ParsedDocument { get; set; }

        public Tcl.ParsedDocument TclParsedDocument
        {
            get
            {
                return ParsedDocument as Tcl.ParsedDocument;
            }
        }

        public ProjectProperty ProjectProperty
        {
            get
            {
                return Project.GetProjectProperty(Plugin.StaticID) as ProjectProperty;
            }
        }

        private codeEditor.CodeEditor.CodeDocument document = null;
        public codeEditor.CodeEditor.CodeDocument CodeDocument
        {
            get
            {
                if (document == null)
                {
                    try
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(Project.GetAbsolutePath(RelativePath)))
                        {
                            document = new CodeEditor.CodeDocument();
                            string text = sr.ReadToEnd();
                            document.Replace(0, 0, 0, text);
                            document.ParentID = ID;
                            document.ClearHistory();
                        }
                    }
                    catch
                    {
                        document = null;
                    }
                }
                return document;
            }
            protected set
            {
                document = value;
            }
        }

        public ajkControls.CodeDrawStyle DrawStyle
        {
            get
            {
                return Global.CodeDrawStyle;
            }
        }

        public override codeEditor.NavigatePanel.NavigatePanelNode CreateNode()
        {
            return new NavigatePanel.TclFileNode(ID, Project);
        }

        public virtual codeEditor.CodeEditor.DocumentParser CreateDocumentParser(codeEditor.CodeEditor.CodeDocument document, string id, codeEditor.Data.Project project, DocumentParser.ParseModeEnum parseMode)
        {
            return new Parser.TclParser(document, id, project,parseMode);
        }


        public override void Update()
        {
            if (TclParsedDocument == null)
            {
                items.Clear();
                return;
            }

            List<string> ids = new List<string>();

            // update

            // remove unused items
            List<codeEditor.Data.Item> removeItems = new List<codeEditor.Data.Item>();
            foreach (codeEditor.Data.Item item in items.Values)
            {
                if (!ids.Contains(item.ID)) removeItems.Add(item);
            }
            foreach (codeEditor.Data.Item item in removeItems)
            {
                items.Remove(item.ID);
            }

            // add new items
            foreach (string id in ids)
            {
                if (items.ContainsKey(id)) continue;
                if (!Project.IsRegistered(id))
                {
                    System.Diagnostics.Debugger.Break();
                    return;
                }
                codeEditor.Data.Item item = Project.GetRegisterdItem(id);
                items.Add(item.ID, item);
            }
        }

        public virtual void AfterKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            if (TclParsedDocument == null) return;
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Return:
                    applyAutoInput();
                    break;
                case System.Windows.Forms.Keys.Space:
                    break;
                default:
                    break;
            }
        }

        public virtual void AfterKeyPressed(System.Windows.Forms.KeyPressEventArgs e)
        {
            if (TclParsedDocument == null) return;
        }

        public virtual void BeforeKeyPressed(System.Windows.Forms.KeyPressEventArgs e)
        {
        }

        public virtual void BeforeKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
        }

        public List<codeEditor.CodeEditor.PopupItem> GetPopupItems(int editId, int index)
        {
            if (TclParsedDocument == null) return null;
            if (TclParsedDocument.EditID != editId) return null;

            int headIndex, length;
            CodeDocument.GetWord(index, out headIndex, out length);
            string text = CodeDocument.CreateString(headIndex, length);
            return TclParsedDocument.GetPopupItems(index, text);
        }


        public List<codeEditor.CodeEditor.ToolItem> GetToolItems(int index)
        {
            List<codeEditor.CodeEditor.ToolItem> toolItems = new List<codeEditor.CodeEditor.ToolItem>();
//            toolItems.Add(new Tcl.Snippets.AlwaysFFSnippet());
            return toolItems;
        }

        public List<codeEditor.CodeEditor.AutocompleteItem> GetAutoCompleteItems(int index, out string cantidateWord)
        {
            cantidateWord = null;

            if (TclParsedDocument == null) return null;
            int line = CodeDocument.GetLineAt(index);
            int lineStartIndex = CodeDocument.GetLineStartIndex(line);

            List<codeEditor.CodeEditor.AutocompleteItem> items = TclParsedDocument.GetAutoCompleteItems(index, lineStartIndex, line, (CodeEditor.CodeDocument)CodeDocument, out cantidateWord);
            return items;
        }



        private void applyAutoInput()
        {
            int index = CodeDocument.CaretIndex;
            int line = CodeDocument.GetLineAt(index);
            if (line == 0) return;
            int prevLine = line - 1;

            int lineHeadIndex = CodeDocument.GetLineStartIndex(line);
            int prevLineHeadIndex = CodeDocument.GetLineStartIndex(prevLine);

            int prevTabs = 0;
            for (int i = prevLineHeadIndex; i < lineHeadIndex; i++)
            {
                char ch = CodeDocument.GetCharAt(i);
                if (ch == '\t')
                {
                    prevTabs++;
                }
                else
                {
                    break;
                }
            }
            int indentLength = 0;
            for (int i = lineHeadIndex; i < CodeDocument.Length; i++)
            {
                char ch = CodeDocument.GetCharAt(i);
                if (ch == '\t')
                {
                    indentLength++;
                }
                else if (ch == ' ')
                {
                    indentLength++;
                }
                else
                {
                    break;
                }
            }


            bool prevBegin = isPrevBegin(lineHeadIndex);
            bool nextEnd = isNextEnd(lineHeadIndex);

            if (prevBegin)
            {
                if (nextEnd) // caret is sandwiched beteen begin and end
                {
                    // BEFORE
                    // begin[enter] end

                    // AFTER
                    // begin
                    //     [caret]
                    // end
                    CodeDocument.Replace(lineHeadIndex, indentLength, 0, new String('\t', prevTabs + 1) + "\r\n" + new String('\t', prevTabs));
                    CodeDocument.CaretIndex = CodeDocument.CaretIndex + prevTabs + 1 + 1 - indentLength;
                    return;
                }
                else
                {   // add indent
                    prevTabs++;
                }
            }

            CodeDocument.Replace(lineHeadIndex, indentLength, 0, new String('\t', prevTabs));
            CodeDocument.CaretIndex = CodeDocument.CaretIndex + prevTabs - indentLength;
        }

        private bool isPrevBegin(int index)
        {
            int prevInex = index;
            if (prevInex > 0) prevInex--;

            if (prevInex > 0 && CodeDocument.GetCharAt(prevInex) == '\n') prevInex--;
            if (prevInex > 0 && CodeDocument.GetCharAt(prevInex) == '\r') prevInex--;

            if (prevInex == 0 || CodeDocument.GetCharAt(prevInex) != 'n') return false;
            prevInex--;
            if (prevInex == 0 || CodeDocument.GetCharAt(prevInex) != 'i') return false;
            prevInex--;
            if (prevInex == 0 || CodeDocument.GetCharAt(prevInex) != 'g') return false;
            prevInex--;
            if (prevInex == 0 || CodeDocument.GetCharAt(prevInex) != 'e') return false;
            prevInex--;
            if (CodeDocument.GetCharAt(prevInex) != 'b') return false;
            return true;
        }

        private bool isNextEnd(int index)
        {
            int prevInex = index;
            if (prevInex < CodeDocument.Length &&
                (
                    CodeDocument.GetCharAt(prevInex) == ' ' || CodeDocument.GetCharAt(prevInex) == '\t'
                )
            ) prevInex++;

            if (prevInex >= CodeDocument.Length || CodeDocument.GetCharAt(prevInex) != 'e') return false;
            prevInex++;
            if (prevInex >= CodeDocument.Length || CodeDocument.GetCharAt(prevInex) != 'n') return false;
            prevInex++;
            if (CodeDocument.GetCharAt(prevInex) != 'd') return false;
            return true;
        }
    }
}


