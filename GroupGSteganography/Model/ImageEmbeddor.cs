using System.Drawing;

namespace GroupGSteganography.Model
{
    internal class ImageEmbeddor : IEmbeddor
    {
        #region Properties

        public Image SourceImage { get; set; }
        public Image MessageImage { get; set; }

        #endregion

        public ImageEmbeddor(Image sourceImage, Image messageImage)
        {
            this.SourceImage = sourceImage;
            this.MessageImage = messageImage;
        }

        #region Methods

        public Image Embed()
        {
            return this.hideImage((Bitmap) this.SourceImage, (Bitmap) this.MessageImage);
        }

        #endregion

        private Bitmap hideImage(Bitmap sourceImage, Bitmap messageImage)
        {
            const int hiddenBits = 1;
            const int shift = (8 - hiddenBits);

            var sourceMask = 0xFF << hiddenBits;
            var messageMask = 0xFF >> shift;
            var combinedImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (var x = 0; x < sourceImage.Width; x++)
            {
                for (var y = 0; y < sourceImage.Height; y++)
                {
                    var clrVisible = sourceImage.GetPixel(x, y);
                    int r;
                    int g;
                    int b;
                    if (messageImage.Width > x && messageImage.Height > y)
                    {
                        var clrHidden = messageImage.GetPixel(x, y);
                        r = (clrVisible.R & sourceMask) + ((clrHidden.R >> shift) & messageMask);
                        g = (clrVisible.G & sourceMask) + ((clrHidden.G >> shift) & messageMask);
                        b = (clrVisible.B & sourceMask) + ((clrHidden.B >> shift) & messageMask);
                    }
                    else
                    {
                        r = (clrVisible.R & sourceMask) + messageMask;
                        g = (clrVisible.G & sourceMask) + messageMask;
                        b = (clrVisible.B & sourceMask) + messageMask;
                    }
                   
                    combinedImage.SetPixel(x, y, Color.FromArgb(255, r, g, b));
                }
            }
            return combinedImage;
        }
    }
}