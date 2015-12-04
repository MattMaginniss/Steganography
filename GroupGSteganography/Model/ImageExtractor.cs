using System;
using System.Drawing;
using GroupGSteganography.Model.Encryption;

namespace GroupGSteganography.Model
{
    /// <summary>
    /// Handles the extraction of an image from another image
    /// </summary>
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
        public Bitmap EncodedImage { get; private set; }

        /// <summary>
        ///     Gets or sets the extracted image.
        /// </summary>
        /// <value>
        ///     The extracted image.
        /// </value>
        public Bitmap ExtractedImage { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageExtractor"/> class.
        /// </summary>
        /// <param name="encodedImage">The encoded image.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ImageExtractor(Image encodedImage)
        {
            if (encodedImage == null)
            {
                throw new ArgumentNullException();
            }
            this.EncodedImage = (Bitmap) encodedImage;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Extracts a hidden image from a hider image
        ///     Precondition: none
        ///     Postcontition: a new image is extracted
        /// </summary>
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