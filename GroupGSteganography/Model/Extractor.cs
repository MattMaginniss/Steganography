﻿using System.Drawing;

namespace GroupGSteganography.Model
{
    public class Extractor
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the extracted image.
        /// </summary>
        /// <value>
        ///     The extracted image.
        /// </value>
        public Image ExtractedImage { get; set; }

        #endregion

        /// <summary>
        ///     Extracts the text.
        /// </summary>
        /// <param name="encodedImage">The encoded image.</param>
        /// <returns>The extracted text</returns>
        public string ExtractText(Image encodedImage)
        {
            var bmp = new Bitmap(encodedImage);

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
                            charValue = this.reverseBits(charValue);

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

        private int reverseBits(int n)
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