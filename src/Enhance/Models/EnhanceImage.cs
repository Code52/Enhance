using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using Phoenix;

namespace Enhance.Models
{
    public class EnhanceImage: ViewModelBase
    {
        public Bitmap Bitmap { get; set; }
        public BitmapImage Image { get { return ConvertImageToBitmapImage(Bitmap); } }

        public string Filename { get; set; }

        private BitmapImage ConvertImageToBitmapImage(Image bitmap)
        {
            if (bitmap == null) return null;

            string fileName = Path.GetTempFileName();
            File.Delete(fileName);

            bitmap.Save(fileName, ImageFormat.Bmp);

            var bitmapImage = new BitmapImage();

            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            bitmapImage.EndInit();

            return bitmapImage;
        }
    }
}
