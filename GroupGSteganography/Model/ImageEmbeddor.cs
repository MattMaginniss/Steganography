using System.Drawing;

namespace GroupGSteganography.Model
{
    internal class ImageEmbeddor : IEmbeddor
    {
        #region Properties

        public Image SourceImage { get; set; }
        public Image MessageImage { get; set; }

        #endregion

        #region Methods

        public Image Embed()
        {
            return hideImage((Bitmap) this.SourceImage, (Bitmap) this.MessageImage);
        }

        #endregion

        private static Bitmap hideImage(Bitmap sourceImage, Bitmap messageImage)
        {
            const int hiddenBits = 1;

            var shift = (8 - hiddenBits);
            var sourceMask = 0xFF << hiddenBits;
            var messageMask = 0xFF >> shift;
            var combinedImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (var x = 0; x < sourceImage.Width; x++)
            {
                for (var y = 0; y < sourceImage.Height; y++)
                {
                    var clrVisible = sourceImage.GetPixel(x, y);
                    var clrHidden = messageImage.GetPixel(x, y);
                    var r = (clrVisible.R & sourceMask) + ((clrHidden.R >> shift) & messageMask);
                    var g = (clrVisible.G & sourceMask) + ((clrHidden.G >> shift) & messageMask);
                    var b = (clrVisible.B & sourceMask) + ((clrHidden.B >> shift) & messageMask);
                    combinedImage.SetPixel(x, y, Color.FromArgb(255, r, g, b));
                }
            }
            return combinedImage;
        }
    }
}