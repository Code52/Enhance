using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enhance.Logic.Models
{
    public static class ColorDepths
    {
        public static ColorDepth BlackAndWhite = new ColorDepth("Black and White", 4, 1);
        public static ColorDepth Greyscale = new ColorDepth("Greyscale", 2, 8);
        public static ColorDepth Color = new ColorDepth("Color", 1, 24);
    }

    public class ColorDepth
    {
        public ColorDepth(string name, int value, int bpp)
        {
            Name = name;
            Value = value;
            BitsPerPixel = bpp;
        }

        public string Name { get; set; }
        public int Value { get; set; }
        public int BitsPerPixel { get; set; }
    }
}
