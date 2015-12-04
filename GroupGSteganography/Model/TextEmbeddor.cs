using System;
using System.Drawing;
using GroupGSteganography.Model.Encryption;

namespace GroupGSteganography.Model
{
    internal class TextEmbeddor : IEmbeddor
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the source image.
        /// </summary>
        /// <value>
        ///     The source image.
        /// </value>
        public Image SourceImage { get; }

        /// <summary>
        ///     Gets or sets the embedded text.
        /// </summary>
        /// <value>
        ///     The embedded text.
        /// </value>
        public string MessageText { get; private set; }

        /// <summary>
        ///     Gets or sets the HeaderPixel, which stores information about the image.
        /// </summary>
        /// <value>
        ///     The Header Pixel.
        /// </value>
        public HeaderPixel HeaderPixel { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextEmbeddor" /> class.
        /// </summary>
        /// <param name="sourceImage">The source image.</param>
        /// <param name="textToEmbed">The text to embed.</param>
        /// <param name="headerPixel">The header pixel</param>
        public TextEmbeddor(Image sourceImage, string textToEmbed, HeaderPixel headerPixel)
        {
            if (sourceImage == null)
            {
                throw new ArgumentNullException();
            }
            if (textToEmbed == null)
            {
                throw new ArgumentNullException();
            }
            if (headerPixel == null)
            {
                throw new ArgumentNullException();
            }
            this.SourceImage = sourceImage;
            this.MessageText = textToEmbed;
            this.HeaderPixel = headerPixel;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Embeds this text into the image.
        /// </summary>
        /// <returns>the image with embedded text.</returns>
        public Image Embed()
        {
            if (this.HeaderPixel.IsEncrypted)
            {
                var encrypter = new TextEncryption(this.MessageText, this.HeaderPixel.RotShift);
                this.MessageText = encrypter.EncryptText();
            }
            var img = new Bitmap(this.SourceImage);

            for (var i = 0; i < img.Width; i++)
            {
                for (var j = 0; j < img.Height; j++)
                {
                    this.setEmbeddedPixel(i, j, img);
                }
            }

            return img;
        }

        #endregion

        private void setEmbeddedPixel(int i, int j, Bitmap img)
        {
            if (img == null)
            {
                throw new ArgumentNullException();
            }
            if (i != 0 || j != 0)
            {
                var pixel = img.GetPixel(i, j);

                this.embedCharacter(i, j, img, pixel);
                this.setEndMessageCharacters(i, j, img, pixel);
            }
            else
            {
                this.embedHeaderPixel(img);
            }
        }

        private void embedHeaderPixel(Bitmap img)
        {
            if (img == null)
            {
                throw new ArgumentNullException();
            }
            img.SetPixel(0, 0, this.HeaderPixel.GetColor());
        }

        private void embedCharacter(int i, int j, Bitmap img, Color pixel)
        {
            if (img == null)
            {
                throw new ArgumentNullException();
            }
            if (pixel == null)
            {
                throw new ArgumentNullException();
            }
            if (i*img.Width + j <= this.MessageText.Length)
            {
                var letter = Convert.ToChar(this.MessageText.Substring(j - 1, 1));
                var value = Convert.ToInt16(letter);

                img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, value));
            }
        }

        private void setEndMessageCharacters(int i, int j, Bitmap img, Color pixel)
        {
            if (img == null)
            {
                throw new ArgumentNullException();
            }
            if (pixel == null)
            {
                throw new ArgumentNullException();
            }
            if (this.MessageText.Length < (i*img.Width) + j &&
                (i*img.Width) + j <= this.MessageText.Length + 3)
            {
                var value = Convert.ToInt16('#');
                img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, value));
            }
        }
    }
}