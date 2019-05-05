using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl
{
    public class Plugin : codeEditorPlugin.IPlugin
    {
        public void Initialize()
        {
            // register filetype
            {
                FileTypes.TclFile fileType = new FileTypes.TclFile();
                codeEditor.Global.FileTypes.Add(fileType.ID, fileType);
            }

            // append menu items
            System.Windows.Forms.ContextMenuStrip menu = codeEditor.Global.Controller.NavigatePanel.GetContextMenuStrip();
//            menu.Items.Add(Global.SetupForm.icarusVerilogSimulationToolStripMenuItem);
        }
        public string Id { get { return StaticID; } }

        public static string StaticID = "Tcl";
    }
}
