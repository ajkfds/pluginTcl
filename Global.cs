﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl
{
    public static class Global
    {
        public static CodeDrawStyle CodeDrawStyle = new CodeDrawStyle();

        public static class Icons
        {
            public static ajkControls.IconImage Exclamation = new ajkControls.IconImage(Properties.Resources.exclamation);
            public static ajkControls.IconImage ExclamationBox = new ajkControls.IconImage(Properties.Resources.exclamationBox);
            //            public static ajkControls.IconImage Play = new ajkControls.IconImage(Properties.Resources.play);
            //            public static ajkControls.IconImage Pause = new ajkControls.IconImage(Properties.Resources.pause);
            public static ajkControls.IconImage Tcl = new ajkControls.IconImage(Properties.Resources.tcl);
        }
    }
}
