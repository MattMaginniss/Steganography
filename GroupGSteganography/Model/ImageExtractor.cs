using System.Drawing;
using GroupGSteganography.Model.Encryption;

namespace GroupGSteganography.Model
{
    internal class ImageExtractor : IExtractor
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the encoded image.
        /// </summary>
        /// <value>
        ///     The encoded image.
        /// </value>
        public Bitmap EncodedImage { get; }

        /// <summary>
        ///     Gets or sets the extracted image.
        /// </summary>
        /// <value>
        ///     The extracted image.
        /// </value>
        public Image ExtractedImage { get; private set; }

        #endregion

        #region Constructors

        public ImageExtractor(Image encodedImage)
        {
            this.EncodedImage =(Bitmap) encodedImage;
        }

        #endregion

        #region Methods

        public void Extract()
        {
            this.ExtractedImage = this.recoverImage((Bitmap) this.EncodedImage);
            this.decryptImage();
        }

        private void decryptImage()
        {
            var headerPixel = HeaderPixel.From(this.EncodedImage.GetPixel(0, 0));
            if (!headerPixel.IsEncrypted)
            {
                return;
            }
            var decrypter = new ImageDecryption((Bitmap)this.ExtractedImage);
            this.ExtractedImage = decrypter.DecryptedImage;
        }

        #endregion

        private Bitmap recoverImage(Bitmap embeddedImage)
        {
            const int hiddenBits = 1;
            const int shift = (8 - hiddenBits);

            var hiddenMask = 0xFF >> shift;
            var extractedImage = new Bitmap(embeddedImage.Width, embeddedImage.Height);
            for (var x = 0; x < embeddedImage.Width; x++)
            {
                for (var y = 0; y < embeddedImage.Height; y++)
                {
                    var clrCombined = embeddedImage.GetPixel(x, y);
                    var red = (clrCombined.R & hiddenMask) << shift;
                    var green = (clrCombined.G & hiddenMask) << shift;
                    var blue = (clrCombined.B & hiddenMask) << shift;
                    extractedImage.SetPixel(x, y, Color.FromArgb(255, red, green, blue));
                }
            }
            return extractedImage;
        }
    }
}