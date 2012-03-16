using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enhance.Logic.Models
{
    public static class Orientations
    {
        public static Orientation Portrait = new Orientation("Portrait", 0);
        public static Orientation Landscape = new Orientation("Landscape", 1);

        public static List<Orientation> List
        {
            get { return new List<Orientation> { Portrait, Landscape };}
        }
    }

    public class Orientation
    {
        public Orientation(string name, int direction)
        {
            Name = name;
            Direction = direction;
        }

        public string Name { get; set; }
        public int Direction { get; set; }
    }
}
