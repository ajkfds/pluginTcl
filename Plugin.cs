using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl
{
    public class Plugin : codeEditorPlugin.IPlugin
    {
        public bool Register()
        {
            // register filetype
            {
                FileTypes.TclFile fileType = new FileTypes.TclFile();
                codeEditor.Global.FileTypes.Add(fileType.ID, fileType);
            }

            // append menu items
            System.Windows.Forms.ContextMenuStrip menu = codeEditor.Controller.NavigatePanel.GetContextMenuStrip();
            //            menu.Items.Add(Global.SetupForm.icarusVerilogSimulationToolStripMenuItem);
            return true;
        }
        public bool Initialize()
        {
            return true;
        }
        public string Id { get { return StaticID; } }

        public static string StaticID = "Tcl";
    }
}
