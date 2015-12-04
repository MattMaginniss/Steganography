using System;
using System.Drawing;
using GroupGSteganography.Model.Encryption;

namespace GroupGSteganography.Model
{
    public class TextEmbeddor : IEmbeddor
    {
        #region Types and Delegates

        public enum State
        {
            Hiding,
            FillingWithZeros
        }

        #endregion

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
                    if (i == 0 && j == 0)
                    {
                        img.SetPixel(0, 0, this.HeaderPixel.GetColor());
                    }
                    else
                    {
                        var pixel = img.GetPixel(i, j);

                        if (i < 1 && j < this.MessageText.Length)
                        {
                            var letter = Convert.ToChar(this.MessageText.Substring(j, 1));
                            var value = Convert.ToInt32(letter);

                            img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, value));
                        }

                        if (i == img.Width - 1 && j == img.Height - 1)
                        {
                            img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, this.MessageText.Length));
                        }
                    }
                }
            }

            return img;
        }

        #endregion
    }
}