﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl
{
    public class CodeDrawStyle : ajkControls.CodeTextbox.CodeDrawStyle
    {
        public override System.Drawing.Color[] ColorPallet
        {
            get
            {
                return colorPallet;
            }
        }

        private static System.Drawing.Color[] colorPallet = new Color[16]
        {
                System.Drawing.Color.FromArgb(212,212,212),     // default
                System.Drawing.Color.LightGray,                 // inactivated
                System.Drawing.Color.DarkGray,                  // 2
                System.Drawing.Color.Crimson,                   // variable-heavy
                System.Drawing.Color.MediumBlue,                // keyword
                System.Drawing.Color.ForestGreen,               // comment
                System.Drawing.Color.CadetBlue,                 // identifier
                System.Drawing.Color.Orchid,                    // variable-fixed
                System.Drawing.Color.SandyBrown,                // number
                System.Drawing.Color.Salmon,                    // variable-light
                System.Drawing.Color.Green,                     // highlighted comment
                System.Drawing.Color.Pink,                      // 11
                System.Drawing.Color.Black,                     // 12
                System.Drawing.Color.Black,                     // 13
                System.Drawing.Color.Black,                     // 14
                System.Drawing.Color.Black                      // 15
        };

        public static byte ColorIndex(ColorType colorType)
        {
            return (byte)colorType;
        }

        public static System.Drawing.Color Color(ColorType colorType)
        {
            return colorPallet[ColorIndex(colorType)];
        }

        public enum ColorType : byte
        {
            Normal = 0,
            Comment = 5,
            Register = 3,
            Net = 9,
            Variable = 11,
            Paramater = 7,
            Keyword = 4,
            Identifier = 6,
            Number = 8,
            Inactivated = 1
        }

        public override Color[] MarkColor
        {
            get
            {
                return new System.Drawing.Color[8]
                    {
                        System.Drawing.Color.FromArgb(128,System.Drawing.Color.Red),    // 0
                        System.Drawing.Color.FromArgb(128,System.Drawing.Color.Orange), // 1
                        System.Drawing.Color.Red, // 2
                        System.Drawing.Color.Red, // 3
                        System.Drawing.Color.Red, // 4
                        System.Drawing.Color.Red, // 5
                        System.Drawing.Color.Red, // 6
                        System.Drawing.Color.Red  // 7
                    };
            }
        }

        public override ajkControls.CodeTextbox.CodeTextbox.MarkStyleEnum[] MarkStyle
        {
            get
            {
                return new ajkControls.CodeTextbox.CodeTextbox.MarkStyleEnum[8]
                    {
                        ajkControls.CodeTextbox.CodeTextbox.MarkStyleEnum.wave,    // 0
                        ajkControls.CodeTextbox.CodeTextbox.MarkStyleEnum.wave,    // 1
                        ajkControls.CodeTextbox.CodeTextbox.MarkStyleEnum.underLine,
                        ajkControls.CodeTextbox.CodeTextbox.MarkStyleEnum.underLine,
                        ajkControls.CodeTextbox.CodeTextbox.MarkStyleEnum.underLine,
                        ajkControls.CodeTextbox.CodeTextbox.MarkStyleEnum.underLine,
                        ajkControls.CodeTextbox.CodeTextbox.MarkStyleEnum.underLine,
                        ajkControls.CodeTextbox.CodeTextbox.MarkStyleEnum.underLine
                    };
            }
        }

    }
}
