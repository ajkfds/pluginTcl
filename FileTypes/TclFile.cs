using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl.FileTypes
{
    public class TclFile : codeEditor.FileTypes.FileType
    {
        public override string ID { get { return "TclFile"; } }

        public override bool IsThisFileType(string relativeFilePath, codeEditor.Data.Project project)
        {
            if (
                relativeFilePath.ToLower().EndsWith(".tcl")
            )
            {
                return true;
            }
            return false;
        }

        public override codeEditor.Data.File CreateFile(string relativeFilePath, codeEditor.Data.Project project)
        {
            return Data.TclFile.Create(relativeFilePath, project);
        }
    }
}
