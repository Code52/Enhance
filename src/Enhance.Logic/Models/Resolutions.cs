using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enhance.Logic.Models
{
    public static class Resolutions
    {
        public static Resolution R50 = new Resolution("50", 50);
        public static Resolution R72 = new Resolution("72", 72);
        public static Resolution R96 = new Resolution("96", 96);
        public static Resolution R150 = new Resolution("150", 150);
        public static Resolution R200 = new Resolution("200", 200);
        public static Resolution R240 = new Resolution("240", 240);
        public static Resolution R266 = new Resolution("266", 266);
        public static Resolution R300 = new Resolution("300", 300);
        public static Resolution R350 = new Resolution("350", 350);
        public static Resolution R360 = new Resolution("360", 360);
        public static Resolution R400 = new Resolution("400", 400);
        public static Resolution R600 = new Resolution("600", 600);
        public static Resolution R720 = new Resolution("720", 720);
        public static Resolution R800 = new Resolution("800", 800);
        public static Resolution R1200 = new Resolution("1200", 1200);
        public static Resolution R2400 = new Resolution("2400", 2400);
        public static Resolution R3200 = new Resolution("3200", 3200);
        public static Resolution R4800 = new Resolution("4800", 4800);
        public static Resolution R7200 = new Resolution("7200", 7200);
        public static Resolution R9600 = new Resolution("9600", 9600);

        public static List<Resolution> List
        {
            get
            {
                return new List<Resolution> 
                { 
                    R50, 
                    R72, 
                    R96, 
                    R150, 
                    R200, 
                    R240,
                    R266, 
                    R300, 
                    R350, 
                    R360, 
                    R400, 
                    R600, 
                    R720, 
                    R800, 
                    R1200, 
                    R2400, 
                    R3200, 
                    R4800, 
                    R7200, 
                    R9600 
                };
            }
        }
    }

    public class Resolution
    {
        public Resolution(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public int Value { get; set; }
    }
}
