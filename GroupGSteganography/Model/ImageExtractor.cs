using System.Drawing;
using GroupGSteganography.Model.Encryption;

namespace GroupGSteganography.Model
{
    internal class ImageExtractor : IExtractor
    {
        #region Data members

        private const int HiddenBits = 1;
        private const int Shift = 8 - HiddenBits;
        private const int HiddenMask = 0xFF >> Shift;

        #endregion

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
        public Bitmap ExtractedImage { get; private set; }

        #endregion

        #region Constructors

        public ImageExtractor(Image encodedImage)
        {
            this.EncodedImage = (Bitmap) encodedImage;
        }

        #endregion

        #region Methods

        public void Extract()
        {
            this.recoverImage();
            this.decryptImage();
        }

        #endregion

        private void decryptImage()
        {
            var headerPixel = HeaderPixel.From(this.EncodedImage.GetPixel(0, 0));
            if (!headerPixel.IsEncrypted)
            {
                return;
            }
            var decrypter = new ImageDecryption(this.ExtractedImage);
            this.ExtractedImage = decrypter.DecryptedImage;
        }

        private void recoverImage()
        {
            this.ExtractedImage = new Bitmap(this.EncodedImage.Width, this.EncodedImage.Height);
            for (var x = 0; x < this.EncodedImage.Width; x++)
            {
                this.recoverRow(x);
            }
        }

        private void recoverRow(int x)
        {
            for (var y = 0; y < this.EncodedImage.Height; y++)
            {
                this.recoverPixel(x, y);
            }
        }

        private void recoverPixel(int x, int y)
        {
            var clrCombined = this.EncodedImage.GetPixel(x, y);
            var red = (clrCombined.R & HiddenMask) << Shift;
            var green = (clrCombined.G & HiddenMask) << Shift;
            var blue = (clrCombined.B & HiddenMask) << Shift;
            this.ExtractedImage.SetPixel(x, y, Color.FromArgb(255, red, green, blue));
        }
    }
}