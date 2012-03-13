using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enhance.Logic.Models
{
    public static class PageSizes
    {
        public static PageSize A3 = new PageSize(16.5, 11.7);
        public static PageSize A4 = new PageSize(11.7, 8.3);
        public static PageSize A5 = new PageSize(8.3, 5.8);
        public static PageSize A6 = new PageSize(5.8, 4.1);

    }

    public struct PageSize
    {
        public double Height;
        public double Width;

        public PageSize(double height, double width)
        {
            this.Height = height;
            this.Width = width;
        }
    }


}
