using System.Data.Odbc;
using System.Drawing;
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
            this.ExtractedText = this.extractText((Bitmap) this.EncodedImage);
            if (this.HeaderPixel.IsEncrypted)
            {
                this.EncryptedText = this.ExtractedText;
                var decrypter = new TextDecryption(this.EncryptedText, this.HeaderPixel.RotShift);
                this.ExtractedText = decrypter.DecryptText();
            }
        }

        #endregion

        private string extractText(Bitmap bmp)
        {
            var colorUnitIndex = 0;
            var charValue = 0;

            // holds the text that will be extracted from the image
            var extractedText = string.Empty;

            // pass through the rows
            for (var i = 0; i < bmp.Height; i++)
            {
                // pass through each row
                for (var j = 0; j < bmp.Width; j++)
                {
                    var pixel = bmp.GetPixel(j, i);
                    if (j == 0 && i == 0)
                    {
                        break;
                    }
                    // for each pixel, pass through its elements (RGB)
                    for (var n = 0; n < 3; n++)
                    {
                        switch (colorUnitIndex%3)
                        {
                            case 0:
                            {
                                // get the LSB from the pixel element (will be pixel.R % 2)
                                // then add one bit to the right of the current character
                                // this can be done by (charValue = charValue * 2)
                                // replace the added bit (which value is by default 0) with
                                // the LSB of the pixel element, simply by addition
                                charValue = charValue*2 + pixel.R%2;
                            }
                                break;
                            case 1:
                            {
                                charValue = charValue*2 + pixel.G%2;
                            }
                                break;
                            case 2:
                            {
                                charValue = charValue*2 + pixel.B%2;
                            }
                                break;
                        }

                        colorUnitIndex++;

                        // if 8 bits has been added,
                        // then add the current character to the result text
                        if (colorUnitIndex%8 == 0)
                        {
                            // reverse? of course, since each time the process occurs
                            // on the right (for simplicity)
                            charValue = reverseBits(charValue);

                            // can only be 0 if it is the stop character (the 8 zeros)
                            if (charValue == 0)
                            {
                                return extractedText;
                            }

                            // convert the character value from int to char
                            var c = (char) charValue;

                            // add the current character to the result text
                            extractedText += c.ToString();
                        }
                    }
                }
            }

            return extractedText;
        }

        private static int reverseBits(int n)
        {
            var result = 0;

            for (var i = 0; i < 8; i++)
            {
                result = result*2 + n%2;

                n /= 2;
            }

            return result;
        }
    }
}