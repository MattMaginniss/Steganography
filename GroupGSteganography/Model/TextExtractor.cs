using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

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
        public string ExtractedText { get; set; }

        #endregion

        #region Constructors

        public TextExtractor(Image encodedImage)
        {
            this.EncodedImage = encodedImage;
        }

        #endregion

        #region Methods

        public void Extract()
        {
            this.ExtractedText = extractText((Bitmap)this.EncodedImage);
        }

        #endregion

        public static string extractText(Bitmap bmp)
        {
            int colorUnitIndex = 0;
            int charValue = 0;

            // holds the text that will be extracted from the image
            string extractedText = String.Empty;

            // pass through the rows
            for (int i = 0; i < bmp.Height; i++)
            {
                // pass through each row
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);

                    // for each pixel, pass through its elements (RGB)
                    for (int n = 0; n < 3; n++)
                    {
                        switch (colorUnitIndex % 3)
                        {
                            case 0:
                                {
                                    // get the LSB from the pixel element (will be pixel.R % 2)
                                    // then add one bit to the right of the current character
                                    // this can be done by (charValue = charValue * 2)
                                    // replace the added bit (which value is by default 0) with
                                    // the LSB of the pixel element, simply by addition
                                    charValue = charValue * 2 + pixel.R % 2;
                                }
                                break;
                            case 1:
                                {
                                    charValue = charValue * 2 + pixel.G % 2;
                                }
                                break;
                            case 2:
                                {
                                    charValue = charValue * 2 + pixel.B % 2;
                                }
                                break;
                        }

                        colorUnitIndex++;

                        // if 8 bits has been added,
                        // then add the current character to the result text
                        if (colorUnitIndex % 8 == 0)
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
                            char c = (char)charValue;

                            // add the current character to the result text
                            extractedText += c.ToString();
                        }
                    }
                }
            }

            return extractedText;
        }

        public static int reverseBits(int n)
        {
            int result = 0;

            for (int i = 0; i < 8; i++)
            {
                result = result * 2 + n % 2;

                n /= 2;
            }

            return result;
        }
    }
}