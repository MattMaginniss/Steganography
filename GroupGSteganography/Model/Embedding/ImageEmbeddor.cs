﻿using System;
using System.Drawing;
using GroupGSteganography.Model.Encryption;

namespace GroupGSteganography.Model.Embedding
{
    /// <summary>
    ///     Handles the embedding of an image in another image
    /// </summary>
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
        ///     Gets the hider image.
        /// </summary>
        /// <value>
        ///     The hider image.
        /// </value>
        public Bitmap HiderImage { get; }

        /// <summary>
        ///     Gets the embed image.
        /// </summary>
        /// <value>
        ///     The embed image.
        /// </value>
        public Bitmap EmbedImage { get; private set; }

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
        /// <param name="embedImage">The embed image.</param>
        /// <param name="headerPixel">The header pixel.</param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public ImageEmbeddor(Image hiderImage, Image embedImage, HeaderPixel headerPixel)
        {
            if (hiderImage == null)
            {
                throw new ArgumentNullException();
            }
            if (embedImage == null)
            {
                throw new ArgumentNullException();
            }
            if (headerPixel == null)
            {
                throw new ArgumentNullException();
            }
            this.HiderImage = (Bitmap) hiderImage;
            this.EmbedImage = (Bitmap) embedImage;
            this.HeaderPixel = headerPixel;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Embeds an Image into another image
        ///     Precondition: none
        ///     Postcontition: The embedImage is embedded in the HiderImage
        /// </summary>
        /// <returns></returns>
        public Image Embed()
        {
            this.checkEncryption();
            return this.hideImage();
        }

        #endregion

        private void checkEncryption()
        {
            if (!this.HeaderPixel.IsEncrypted)
            {
                return;
            }
            var encrypter = new ImageEncryption(this.HiderImage, this.EmbedImage);
            this.EmbedImage = encrypter.EncryptedImage;
        }

        private Bitmap hideImage()
        {
            this.combinedImage = new Bitmap(this.HiderImage.Width, this.HiderImage.Height);

            for (var x = 0; x < this.HiderImage.Width; x++)
            {
                this.maskColumn(x);
            }
            this.combinedImage.SetPixel(0, 0, this.HeaderPixel.GetColor());
            return this.combinedImage;
        }

        private void maskColumn(int x)
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
            if (this.EmbedImage.Width > x && this.EmbedImage.Height > y)
            {
                var clrHidden = this.EmbedImage.GetPixel(x, y);
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