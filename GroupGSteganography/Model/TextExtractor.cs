using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GroupGSteganography.Model.Encryption;

namespace GroupGSteganography.Model
{
    public class TextExtractor : IExtractor
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the encoded image.
        /// </summary>
        /// <value>
        ///     The encoded image.
        /// </value>
        public Image EncodedImage { get; }

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
        public HeaderPixel HeaderPixel { get; }

        #endregion

        #region Constructors

        public TextExtractor(Image encodedImage, HeaderPixel headerPixel)
        {
            this.EncodedImage = encodedImage;
            this.HeaderPixel = headerPixel;
        }

        #endregion

        #region Methods

        public void Extract()
        {
            this.ExtractedText = extractText((Bitmap) this.EncodedImage);
            MessageBox.Show(this.ExtractedText);
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
            var img = bmp;
            var message = "";

            var lastpixel = img.GetPixel(img.Width - 1, img.Height - 1);
            int msgLength = lastpixel.B;

            for (var i = 0; i < img.Width; i++)
            {
                for (var j = 0; j < img.Height; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        var pixel = img.GetPixel(i, j);

                        if (i*img.Width + j <= msgLength)
                        {
                            int value = pixel.B;
                            var c = Convert.ToChar(value);
                            var letter = Encoding.ASCII.GetString(new[] {Convert.ToByte(c)});

                            message = message + letter;
                        }
                    }
                }
            }

            return message;
        }
    }
}