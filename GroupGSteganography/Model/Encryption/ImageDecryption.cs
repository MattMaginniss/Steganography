﻿using System;
using System.Drawing;

namespace GroupGSteganography.Model.Encryption
{
    /// <summary>
    ///     Class to decrypt an encrypted image
    /// </summary>
    internal class ImageDecryption
    {
        #region Data members

        private readonly Bitmap embeddedImage;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the decrypted image.
        /// </summary>
        /// <value>
        ///     The decrypted image.
        /// </value>
        public Bitmap DecryptedImage { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ImageDecryption" /> class.
        /// </summary>
        /// <param name="embeddedImage">The embedded image.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ImageDecryption(Bitmap embeddedImage)
        {
            if (embeddedImage == null)
            {
                throw new ArgumentNullException();
            }
            this.embeddedImage = embeddedImage;
            this.decryptImage();
        }

        #endregion

        private void decryptImage()
        {
            var scrambledImage = new Bitmap(this.embeddedImage.Width, this.embeddedImage.Height);
            var graphics = Graphics.FromImage(scrambledImage);

            var width = this.embeddedImage.Width;
            var height = this.embeddedImage.Height;

            var quad1 = new Rectangle(width/2, 0, width/2, height/2);
            var quad2 = new Rectangle(0, 0, width/2, height/2);
            var quad3 = new Rectangle(0, height/2, width/2, height/2);
            var quad4 = new Rectangle(width/2, height/2, width/2, height/2);

            graphics.DrawImage(this.embeddedImage, quad1, quad3, GraphicsUnit.Pixel);
            graphics.DrawImage(this.embeddedImage, quad3, quad1, GraphicsUnit.Pixel);
            graphics.DrawImage(this.embeddedImage, quad2, quad4, GraphicsUnit.Pixel);
            graphics.DrawImage(this.embeddedImage, quad4, quad2, GraphicsUnit.Pixel);

            graphics.Dispose();
            this.DecryptedImage = scrambledImage;
        }
    }
}