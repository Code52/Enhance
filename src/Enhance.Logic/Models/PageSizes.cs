using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enhance.Logic.Models
{
    public static class PageSizes
    {
        public static PageSize A3 = new PageSize("A3", 16.5, 11.7);
        public static PageSize A4 = new PageSize("A4", 11.7, 8.3);
        public static PageSize A5 = new PageSize("A5", 8.3, 5.8);
        public static PageSize A6 = new PageSize("A6", 5.8, 4.1);
        public static PageSize Letter = new PageSize("Letter", 8.5, 11);
        public static PageSize Legal = new PageSize("Legal", 8.5, 14);

        public static List<PageSize> List
        {
            get
            {
                return new List<PageSize> { A3, A4, A5, A6, Letter, Legal };
            }
        }
    }

    public struct PageSize
    {
        public string Name;
        public double Height;
        public double Width;

        public PageSize(string name, double height, double width)
        {
            this.Name = name;
            this.Height = height;
            this.Width = width;
        }
    }


}
