using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using AForge.Imaging.Filters;

namespace Enhance.Imaging
{
    ///<summary>
    /// Contains extension methods for System.Drawing.Bitmap classes
    ///</summary>
    public static class BitmapExtensions
    {
        ///<summary>
        /// Converts a System.Drawing.Bitmap to a System.Windows.Media.Imaging.BitmapSource
        ///</summary>
        ///<param name="source">The bitmap to convert</param>
        ///<returns>The converted bitmap</returns>
        public static BitmapSource ToBitmapSource(this System.Drawing.Bitmap source)
        {
            var bitmapHandle = IntPtr.Zero;
            try
            {
                bitmapHandle = source.GetHbitmap();
                var result = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmapHandle, IntPtr.Zero, Int32Rect.Empty,
                                                                                          BitmapSizeOptions.FromEmptyOptions());
                result.Freeze();
                source.Dispose();
                return result;
            }
            catch (Win32Exception ex)
            {
                throw new ImagingException("Failed to create BitmapSource", ex);
            }
            finally
            {
                if (bitmapHandle != IntPtr.Zero)
                {
                    NativeMethods.DeleteObject(bitmapHandle);
                }
            }
        }

        /// <summary>
        /// Adjust the contrast of the image
        /// </summary>
        /// <param name="source">the bitmap source</param>
        /// <param name="contrastValue">contrast value in the range of -127 to 127 </param>
        public static void Contrast(this Bitmap source, Int32 contrastValue)
        {
            if (contrastValue < -127 || contrastValue > 127)
                throw new ArgumentOutOfRangeException("contrastValue", "should be between -127 and 127");
            var filter = new ContrastCorrection(contrastValue);
            filter.ApplyInPlace(source);
        }

        /// <summary>
        /// Adjust the brightness of the image
        /// </summary>
        /// <param name="source">the source bitmap</param>
        /// <param name="brightnessValue">brightness adjustment value in the range of -255 to 255</param>
        public static void Brightness(this Bitmap source, Int32 brightnessValue)
        {
            if (brightnessValue < -255 || brightnessValue > 255)
                throw new ArgumentOutOfRangeException("brightnessValue", "should be between -255 and 255");
            var filter = new BrightnessCorrection(brightnessValue);
            filter.ApplyInPlace(source);
        }

        /// <summary>
        /// Restore Colours on a faded image
        /// </summary>
        /// <remarks>
        /// This a simple image enhancement technique tht attempts to restore the original 
        /// colours of a faded image by improving the contrasts of an image
        /// </remarks>
        /// <param name="source">the bitmap that has faded colours</param>
        public static void FixColours(this Bitmap source)
        {
            var filter = new ContrastStretch();
            filter.ApplyInPlace(source);
        }
    }
}