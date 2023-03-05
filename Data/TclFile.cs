using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeEditor.CodeEditor;
using codeEditor.Data;

namespace pluginTcl.Data
{
    public class TclFile : codeEditor.Data.TextFile
    {
        public static new TclFile Create(string relativePath, codeEditor.Data.Project project)
        {
            //string id = GetID(relativePath, project);
            //if (project.IsRegistered(id))
            //{
            //    TclFile item = project.GetRegisterdItem(id) as TclFile;
            //    project.RegisterProjectItem(item);
            //    return item;
            //}

            TclFile fileItem = new TclFile();
            fileItem.Project = project;
            fileItem.RelativePath = relativePath;
            if (relativePath.Contains('\\'))
            {
                fileItem.Name = relativePath.Substring(relativePath.LastIndexOf('\\') + 1);
            }
            else
            {
                fileItem.Name = relativePath;
            }
//            fileItem.ParseRequested = true;

            //project.RegisterProjectItem(fileItem);
            return fileItem;
        }
        private volatile bool parseRequested = false;
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
                return Project.ProjectProperties[Plugin.StaticID] as ProjectProperty;
            }
        }


        public new ajkControls.CodeTextbox.CodeDrawStyle DrawStyle
        {
            get
            {
                return Global.CodeDrawStyle;
            }
        }

        protected override codeEditor.NavigatePanel.NavigatePanelNode createNode()
        {
            return new NavigatePanel.TclFileNode(this, Project);
        }

        public new virtual codeEditor.CodeEditor.DocumentParser CreateDocumentParser(DocumentParser.ParseModeEnum parseMode)
        {
            return new Parser.TclParser(this,parseMode);
        }


        public override void Update()
        {
            //if (TclParsedDocument == null)
            //{
            //    items.Clear();
            //    return;
            //}

            //List<string> ids = new List<string>();

            //// update

            //// remove unused items
            //List<codeEditor.Data.Item> removeItems = new List<codeEditor.Data.Item>();
            //foreach (codeEditor.Data.Item item in items.Values)
            //{
            //    if (!ids.Contains(item.ID)) removeItems.Add(item);
            //}
            //foreach (codeEditor.Data.Item item in removeItems)
            //{
            //    items.Remove(item.ID);
            //}

            //// add new items
            //foreach (string id in ids)
            //{
            //    if (items.ContainsKey(id)) continue;
            //    if (!Project.IsRegistered(id))
            //    {
            //        System.Diagnostics.Debugger.Break();
            //        return;
            //    }
            //    codeEditor.Data.Item item = Project.GetRegisterdItem(id);
            //    items.Add(item.ID, item);
            //}
        }

        public new virtual void AfterKeyDown(System.Windows.Forms.KeyEventArgs e)
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

        public new virtual void AfterKeyPressed(System.Windows.Forms.KeyPressEventArgs e)
        {
            if (TclParsedDocument == null) return;
        }

        public new virtual void BeforeKeyPressed(System.Windows.Forms.KeyPressEventArgs e)
        {
        }

        public new virtual void BeforeKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
        }

        public new List<codeEditor.CodeEditor.PopupItem> GetPopupItems(ulong version, int index)
        {
            if (TclParsedDocument == null) return null;
            if (TclParsedDocument.Version != version) return null;

            int headIndex, length;
            CodeDocument.GetWord(index, out headIndex, out length);
            string text = CodeDocument.CreateString(headIndex, length);
            return TclParsedDocument.GetPopupItems(index, text);
        }


        public new List<codeEditor.CodeEditor.ToolItem> GetToolItems(int index)
        {
            List<codeEditor.CodeEditor.ToolItem> toolItems = new List<codeEditor.CodeEditor.ToolItem>();
//            toolItems.Add(new Tcl.Snippets.AlwaysFFSnippet());
            return toolItems;
        }

        public new List<codeEditor.CodeEditor.AutocompleteItem> GetAutoCompleteItems(int index, out string cantidateWord)
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


