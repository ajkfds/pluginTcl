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
//            FileTypes.CSourceFile fileType = new FileTypes.CSourceFile();
//            codeEditor.Global.FileTypes.Add(fileType.ID, fileType);
        }
        public string Id { get { return "Tcl"; } }
    }
}
