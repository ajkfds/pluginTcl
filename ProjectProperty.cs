using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl
{
    public class ProjectProperty : codeEditor.Data.ProjectProperty
    {
        public ProjectProperty(codeEditor.Data.Project project)
        {
            this.project = project;
        }
        private codeEditor.Data.Project project;

//        public Tcl.Snippets.Setup SnippetSetup = new Tcl.Snippets.Setup();

        public override void SaveSetup(ajkControls.Json.JsonWriter writer)
        {
        }

    }
}
