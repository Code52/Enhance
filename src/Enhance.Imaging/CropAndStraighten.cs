using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace Enhance.Imaging
{
    /// <summary>
    /// Extracts, crops, and straightens multiple images in a single scan and returns each as separate bitmaps
    /// </summary>
    public class CropAndStraighten
    {
        public IEnumerable<Bitmap> Apply(Bitmap bitmap)
        {
            // assuming scanned background is white we need to invert for the algo to work
            var copy = new Invert().Apply(bitmap);

            copy = EnsureGrayscale(copy);
            new Threshold { ThresholdValue = 25 }.ApplyInPlace(copy);
            new FillHoles().ApplyInPlace(copy);

            var blobCounter = new BlobCounter
            {
                // set filtering options
                FilterBlobs = true,
                MinWidth = 50,
                MinHeight = 50,
            };

            blobCounter.ProcessImage(copy);
            var blobs = blobCounter.GetObjectsInformation();

            if (blobs.Any())
            {
                var invertedOriginal = new Invert().Apply(bitmap);
                foreach (var blob in blobs)
                {
                    // use inverted source to ensure correct edge colors
                    blobCounter.ExtractBlobsImage(invertedOriginal, blob, false);
                    var blobImage = blob.Image.ToManagedImage();

                    // straighten
                    var angle = new DocumentSkewChecker().GetSkewAngle(EnsureGrayscale(blobImage));
                    var rotationFilter = new RotateBilinear(-angle) { FillColor = Color.Black };
                    blobImage = rotationFilter.Apply(blobImage);

                    // crop
                    blobImage = new ExtractBiggestBlob().Apply(blobImage);

                    new Invert().ApplyInPlace(blobImage);
                    yield return blobImage;
                }
            }
            else
            {
                yield return bitmap;
            }

        }

        /// <summary>
        /// Make image grayscale if it isn't already. Does not ensure you get a copy
        /// </summary>
        private static Bitmap EnsureGrayscale(Bitmap source)
        {
            return source.PixelFormat == PixelFormat.Format8bppIndexed ? source : Grayscale.CommonAlgorithms.BT709.Apply(source);
        }
    }
}