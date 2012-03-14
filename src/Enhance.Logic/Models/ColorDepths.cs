using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enhance.Logic.Models
{
    public static class ColorDepths
    {
        public static ColorDepth BlackAndWhite = new ColorDepth("Black and White", 4);
        public static ColorDepth Grey = new ColorDepth("Grey", 2);
        public static ColorDepth Color = new ColorDepth("Color", 1);
    }

    public class ColorDepth
    {
        public ColorDepth(string name, int value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; set; }
        public int Value { get; set; }
    }
}
