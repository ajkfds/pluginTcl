using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ajkControls.Primitive;

namespace pluginTcl
{
    public static class Global
    {
        public static CodeDrawStyle CodeDrawStyle = new CodeDrawStyle();

        public static class Icons
        {
            public static IconImage Exclamation = new IconImage(Properties.Resources.exclamation);
            public static IconImage ExclamationBox = new IconImage(Properties.Resources.exclamationBox);
            //            public static ajkControls.IconImage Play = new ajkControls.IconImage(Properties.Resources.play);
            //            public static ajkControls.IconImage Pause = new ajkControls.IconImage(Properties.Resources.pause);
            public static IconImage Tcl = new IconImage(Properties.Resources.tcl);
        }
    }
}
