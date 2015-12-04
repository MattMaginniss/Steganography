using System;
using System.Drawing;
using GroupGSteganography.Model.Encryption;

namespace GroupGSteganography.Model
{
    /// <summary>
    ///     Handles Embedding of text into image
    /// </summary>
    public class TextEmbeddor : IEmbeddor
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the source image.
        /// </summary>
        /// <value>
        ///     The source image.
        /// </value>
        public Image SourceImage { get; private set; }

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
        public HeaderPixel HeaderPixel { get; private set; }

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
        ///     Precondition: None
        ///     Postcontition: Text is embedded in an image
        /// </summary>
        /// <returns>the image with embedded text.</returns>
        public Image Embed()
        {
            this.checkForEncryption();

            var img = new Bitmap(this.SourceImage);

            for (var i = 0; i < img.Width; i++)
            {
                for (var j = 0; j < img.Height; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        var pixel = img.GetPixel(i, j);

                        this.embedHiddenMessageCharacter(i, img, j, pixel);
                        this.setEndOfMessageCharacters(i, img, j, pixel);
                    }
                    else
                    {
                        this.embedHeaderPixel(img);
                    }
                }
            }

            return img;
        }

        #endregion

        private void checkForEncryption()
        {
            if (this.HeaderPixel.IsEncrypted)
            {
                this.encryptMessage();
            }
        }

        private void encryptMessage()
        {
            var encrypter = new TextEncryption(this.MessageText, this.HeaderPixel.RotShift);
            this.MessageText = encrypter.EncryptText();
        }

        private void embedHeaderPixel(Bitmap img)
        {
            img.SetPixel(0, 0, this.HeaderPixel.GetColor());
        }

        private void embedHiddenMessageCharacter(int i, Bitmap img, int j, Color pixel)
        {
            if (i*img.Width + j <= this.MessageText.Length)
            {
                var letter = Convert.ToChar(this.MessageText.Substring(i*img.Width + j - 1, 1));
                var value = Convert.ToInt16(letter);

                img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, value));
            }
        }

        private void setEndOfMessageCharacters(int i, Bitmap img, int j, Color pixel)
        {
            if (this.MessageText.Length < i*img.Width + j &&
                i*img.Width + j <= this.MessageText.Length + 3)
            {
                var value = Convert.ToInt16('#');
                img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, value));
            }
        }
    }
}