using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;

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
    }
}