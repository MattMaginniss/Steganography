using System.Drawing;
using GroupGSteganography.Model.Encryption;

namespace GroupGSteganography.Model
{
    internal class ImageEmbeddor : IEmbeddor
    {
        #region Data members

        private const int HiddenBits = 1;
        private const int Shift = 8 - HiddenBits;
        private const int SourceMask = 0xFF << HiddenBits;
        private const int MessageMask = 0xFF >> Shift;
        private Bitmap combinedImage;

        #endregion

        #region Properties

        /// <summary>
        ///     The Hider image
        /// </summary>
        /// <value>
        ///     The source image.
        /// </value>
        public Bitmap HiderImage { get; }

        /// <summary>
        ///     Gets the Embedded image.
        /// </summary>
        /// <value>
        ///     The message image.
        /// </value>
        public Bitmap EmbeddedImage { get; private set; }

        /// <summary>
        ///     Gets the header pixel.
        /// </summary>
        /// <value>
        ///     The header pixel.
        /// </value>
        public HeaderPixel HeaderPixel { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ImageEmbeddor" /> class.
        /// </summary>
        /// <param name="hiderImage">The hider image.</param>
        /// <param name="embeddedImage">The embedded image.</param>
        /// <param name="headerPixel">The header pixel.</param>
        public ImageEmbeddor(Image hiderImage, Image embeddedImage, HeaderPixel headerPixel)
        {
            this.HiderImage = (Bitmap) hiderImage;
            this.EmbeddedImage = (Bitmap) embeddedImage;
            this.HeaderPixel = headerPixel;
            this.Embed();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Embeds the EmbdeddedImage into the HiderImage.
        /// </summary>
        /// <returns>The Hider image containing the Embedded Image</returns>
        public Image Embed()
        {
            this.checkEncryption();
            this.hideImage();
            return this.combinedImage;
        }

        #endregion

        private void checkEncryption()
        {
            if (!this.HeaderPixel.IsEncrypted)
            {
                return;
            }
            var encrypter = new ImageEncryption(this.HiderImage, this.EmbeddedImage);
            this.EmbeddedImage = encrypter.EncryptedImage;
        }

        private void hideImage()
        {
            this.combinedImage = new Bitmap(this.HiderImage.Width, this.HiderImage.Height);

            for (var x = 0; x < this.HiderImage.Width; x++)
            {
                this.maskRow(x);
            }
            this.combinedImage.SetPixel(0, 0, this.HeaderPixel.GetColor());
        }

        private void maskRow(int x)
        {
            for (var y = 0; y < this.HiderImage.Height; y++)
            {
                this.maskPixel(x, y);
            }
        }

        private void maskPixel(int x, int y)
        {
            var clrVisible = this.HiderImage.GetPixel(x, y);
            int r;
            int g;
            int b;
            if (this.EmbeddedImage.Width > x && this.EmbeddedImage.Height > y)
            {
                var clrHidden = this.EmbeddedImage.GetPixel(x, y);
                r = (clrVisible.R & SourceMask) + ((clrHidden.R >> Shift) & MessageMask);
                g = (clrVisible.G & SourceMask) + ((clrHidden.G >> Shift) & MessageMask);
                b = (clrVisible.B & SourceMask) + ((clrHidden.B >> Shift) & MessageMask);
            }
            else
            {
                r = (clrVisible.R & SourceMask) + MessageMask;
                g = (clrVisible.G & SourceMask) + MessageMask;
                b = (clrVisible.B & SourceMask) + MessageMask;
            }

            this.combinedImage.SetPixel(x, y, Color.FromArgb(255, r, g, b));
        }
    }
}