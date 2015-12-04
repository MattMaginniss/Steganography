using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using GroupGSteganography.Model.Encryption;

namespace GroupGSteganography.Model
{
    internal class ImageEmbeddor : IEmbeddor
    {
        #region Properties

        public Image SourceImage { get; set; }
        public Image MessageImage { get; set; }
        public HeaderPixel HeaderPixel { get; set; }


        #endregion

        #region Constructors

        public ImageEmbeddor(Image sourceImage, Image messageImage, HeaderPixel headerPixel)
        {
            this.SourceImage = sourceImage;
            this.MessageImage = messageImage;
            this.HeaderPixel = headerPixel;
        }

        #endregion

        #region Methods

        public Image Embed()
        {
            this.checkEncryption();
            return this.hideImage((Bitmap)this.SourceImage, (Bitmap)this.MessageImage);
        }

        private void checkEncryption()
        {
            if (!this.HeaderPixel.IsEncrypted)
            {
                return;
            }
            var encrypter = new ImageEncryption((Bitmap)this.SourceImage, (Bitmap)this.MessageImage);
            this.MessageImage = encrypter.EncryptedImage;
        }

        #endregion

        private Bitmap hideImage(Bitmap sourceImage, Bitmap messageImage)
        {
            const int hiddenBits = 1;
            const int shift = (8 - hiddenBits);

            const int sourceMask = 0xFF << hiddenBits;
            const int messageMask = 0xFF >> shift;
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
            combinedImage.SetPixel(0, 0, this.HeaderPixel.GetColor());
            return combinedImage;
        }
    }
}