using System;
using System.Drawing;
using System.Text;
using GroupGSteganography.Model.Encryption;

namespace GroupGSteganography.Model
{
    internal class TextExtractor : IExtractor
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the encoded image.
        /// </summary>
        /// <value>
        ///     The encoded image.
        /// </value>
        public Bitmap EncodedImage { get; private set; }

        /// <summary>
        ///     Gets or sets the extracted text.
        /// </summary>
        /// <value>
        ///     The extracted text.
        /// </value>
        public string ExtractedText { get; private set; }

        /// <summary>
        ///     Gets or sets the extracted text.
        /// </summary>
        /// <value>
        ///     The extracted text.
        /// </value>
        public string EncryptedText { get; private set; }

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
        ///     Initializes a new instance of the <see cref="TextExtractor" /> class.
        /// </summary>
        /// <param name="encodedImage">The encoded image.</param>
        public TextExtractor(Image encodedImage)
        {
            if (encodedImage == null)
            {
                throw new ArgumentNullException();
            }
            this.EncodedImage = (Bitmap) encodedImage;
            this.HeaderPixel = HeaderPixel.From(this.EncodedImage.GetPixel(0, 0));
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Extracts the text from the image.
        ///     Precondition: none
        ///     Postcontition: New text is extracted from the image
        /// </summary>
        public void Extract()
        {
            this.ExtractedText = extractText(this.EncodedImage);
            if (this.HeaderPixel.IsEncrypted)
            {
                this.EncryptedText = this.ExtractedText;
                var decrypter = new TextDecryption(this.EncryptedText, this.HeaderPixel.RotShift);
                this.ExtractedText = decrypter.DecryptText();
            }
        }

        #endregion

        private static string extractText(Bitmap bmp)
        {
            if (bmp == null)
            {
                throw new ArgumentNullException();
            }
            var img = bmp;
            var message = "";

            for (var i = 0; i < img.Width; i++)
            {
                for (var j = 0; j < img.Height; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        var pixel = img.GetPixel(i, j);

                        int value = pixel.B;
                        var c = Convert.ToChar(value);
                        var letter = Encoding.ASCII.GetString(new[] {Convert.ToByte(c)});
                        if (letter.Equals("#"))
                        {
                            for (var x = 1; x < 3; x ++)
                            {
                                pixel = img.GetPixel(i, j + x);
                                value = pixel.B;
                                c = Convert.ToChar(value);
                                letter = Encoding.ASCII.GetString(new[] {Convert.ToByte(c)});
                                if (!letter.Equals("#"))
                                {
                                    message = message + "#";
                                }
                                else
                                {
                                    return message;
                                }
                            }
                        }
                        else
                        {
                            message = message + letter;
                        }
                    }
                }
            }

            return message;
        }
    }
}