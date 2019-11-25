using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace pluginTcl.NavigatePanel
{
    public class TclFileNode : codeEditor.NavigatePanel.FileNode
    {
        public TclFileNode(Data.TclFile tclFile, codeEditor.Data.Project project) : base(tclFile)
        {

        }

        public codeEditor.Data.TextFile TextFile
        {
            get { return Item as codeEditor.Data.TextFile; }
        }

        public virtual Data.TclFile TclFile
        {
            get { return Item as Data.TclFile; }
        }

        public override string Text
        {
            get { return FileItem.Name; }
        }

        private static ajkControls.IconImage icon = new ajkControls.IconImage(Properties.Resources.tcl);
        public override void DrawNode(Graphics graphics, int x, int y, Font font, Color color, Color backgroundColor, Color selectedColor, int lineHeight, bool selected)
        {
            graphics.DrawImage(icon.GetImage(lineHeight, ajkControls.IconImage.ColorStyle.White), new Point(x, y));
            Color bgColor = backgroundColor;
            if (selected) bgColor = selectedColor;
            System.Windows.Forms.TextRenderer.DrawText(
                graphics,
                Text,
                font,
                new Point(x + lineHeight + (lineHeight >> 2), y),
                color,
                bgColor,
                System.Windows.Forms.TextFormatFlags.NoPadding
                );

            if(TclFile != null && TclFile.ParsedDocument != null && TclFile.TclParsedDocument.ErrorCount != 0)
            {
                graphics.DrawImage(Global.Icons.Exclamation.GetImage(lineHeight, ajkControls.IconImage.ColorStyle.Red), new Point(x, y));
            }
        }

        public override void Selected()
        {
            codeEditor.Controller.CodeEditor.SetTextFile(TextFile);
        }

        public override void Update()
        {
            TclFile.Update();

            List<string> currentDataIds = new List<string>();
            foreach (string key in TclFile.Items.Keys)
            {
                currentDataIds.Add(key);
            }

            List<codeEditor.NavigatePanel.NavigatePanelNode> removeNodes = new List<codeEditor.NavigatePanel.NavigatePanelNode>();
            //foreach (codeEditor.NavigatePanel.NavigatePanelNode node in TreeNodes)
            //{
            //    if (currentDataIds.Contains(node.ID))
            //    {
            //        currentDataIds.Remove(node.ID);
            //    }
            //    else
            //    {
            //        removeNodes.Add(node);
            //    }
            //}

            foreach (codeEditor.NavigatePanel.NavigatePanelNode nodes in removeNodes)
            {
                TreeNodes.Remove(nodes);
            }

            //foreach (string id in currentDataIds)
            //{
            //    codeEditor.Data.Item item = Project.GetRegisterdItem(id);
            //    if (item == null) continue;
            //    TreeNodes.Add(item.CreateNode());
            //}
        }

    }

}
