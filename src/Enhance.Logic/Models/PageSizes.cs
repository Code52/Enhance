using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enhance.Logic.Models
{
    public static class PageSizes
    {
        public static PageSize A3 = new PageSize("A3", 16.54, 11.69);
        public static PageSize A4 = new PageSize("A4", 11.69, 8.27);
        public static PageSize A5 = new PageSize("A5", 8.27, 5.83);
        public static PageSize A6 = new PageSize("A6", 5.83, 4.13);
        public static PageSize Letter = new PageSize("Letter", 11, 8.5);
        public static PageSize Legal = new PageSize("Legal", 14, 8.5);

        public static List<PageSize> List
        {
            get
            {
                return new List<PageSize> { A3, A4, A5, A6, Letter, Legal };
            }
        }
    }

    public class PageSize
    {
        public string Name { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }

        public PageSize(string name, double height, double width)
        {
            Name = name;
            Height = height;
            Width = width;
        }
    }


}
