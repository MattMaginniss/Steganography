using System;
using System.Drawing;

namespace GroupGSteganography.Model.Encryption
{
    internal class ImageEncryption
    {
        #region Data members

        private readonly Bitmap hiderImage;
        private readonly Bitmap embeddedImage;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the encrypted image.
        /// </summary>
        /// <value>
        /// The encrypted image.
        /// </value>
        public Bitmap EncryptedImage { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageEncryption"/> class.
        /// </summary>
        /// <param name="hiderImage">The hider image.</param>
        /// <param name="embeddedImage">The embedded image.</param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public ImageEncryption(Bitmap hiderImage, Bitmap embeddedImage)
        {
            if (hiderImage == null)
            {
                throw new ArgumentNullException();
            }
            if (embeddedImage == null)
            {
                throw new ArgumentNullException();
            }
            this.hiderImage = hiderImage;
            this.embeddedImage = embeddedImage;
            this.encryptImage();
        }

        #endregion

        private void encryptImage()
        {
            this.fitEmbeddedImage();
            this.scrambleImage();
        }

        private void fitEmbeddedImage()
        {
            var resizedCanvas = new Bitmap(this.hiderImage.Width, this.hiderImage.Height);
            var gfx = Graphics.FromImage(resizedCanvas);

            gfx.DrawImageUnscaled(this.embeddedImage, 0, 0);
            this.EncryptedImage = resizedCanvas;
        }

        private void scrambleImage()
        {
            var scrambledImage = new Bitmap(this.EncryptedImage.Width, this.EncryptedImage.Height);
            var graphics = Graphics.FromImage(scrambledImage);

            var width = this.EncryptedImage.Width;
            var height = this.EncryptedImage.Height;

            var quad1 = new Rectangle(width/2, 0, width/2, height/2);
            var quad2 = new Rectangle(0, 0, width/2, height/2);
            var quad3 = new Rectangle(0, height/2, width/2, height/2);
            var quad4 = new Rectangle(width/2, height/2, width/2, height/2);

            graphics.DrawImage(this.EncryptedImage, quad1, quad3, GraphicsUnit.Pixel);
            graphics.DrawImage(this.EncryptedImage, quad3, quad1, GraphicsUnit.Pixel);
            graphics.DrawImage(this.EncryptedImage, quad2, quad4, GraphicsUnit.Pixel);
            graphics.DrawImage(this.EncryptedImage, quad4, quad2, GraphicsUnit.Pixel);

            graphics.Dispose();
            this.EncryptedImage = scrambledImage;
        }
    }
}