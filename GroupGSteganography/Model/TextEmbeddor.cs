using System.Drawing;

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
        public string MessageText { get; }

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
            var bmp = (Bitmap) this.SourceImage;
            var text = this.MessageText;

            // initially, we'll be hiding characters in the image
            var state = State.Hiding;

            // holds the index of the character that is being hidden
            var charIndex = 0;

            // holds the value of the character converted to integer
            var charValue = 0;

            // holds the index of the color element (R or G or B) that is currently being processed
            long pixelElementIndex = 0;

            // holds the number of trailing zeros that have been added when finishing the process
            var zeros = 0;

            // hold pixel elements

            // pass through the rows
            for (var i = 0; i < bmp.Height; i++)
            {
                // pass through each row
                for (var j = 0; j < bmp.Width; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        bmp.SetPixel(0, 0, this.HeaderPixel.GetColor());
                        break;
                    }
                    // holds the pixel that is currently being processed
                    var pixel = bmp.GetPixel(j, i);

                    // now, clear the least significant bit (LSB) from each pixel element
                    var red = pixel.R - pixel.R%2;
                    var green = pixel.G - pixel.G%2;
                    var blue = pixel.B - pixel.B%2;

                    // for each pixel, pass through its elements (RGB)
                    for (var n = 0; n < 3; n++)
                    {
                        // check if new 8 bits has been processed
                        if (pixelElementIndex%8 == 0)
                        {
                            // check if the whole process has finished
                            // we can say that it's finished when 8 zeros are added
                            if (state == State.FillingWithZeros && zeros == 8)
                            {
                                // apply the last pixel on the image
                                // even if only a part of its elements have been affected
                                if ((pixelElementIndex - 1)%3 < 2)
                                {
                                    bmp.SetPixel(j, i, Color.FromArgb(red, green, blue));
                                }

                                // return the bitmap with the text hidden in
                                return bmp;
                            }

                            // check if all characters has been hidden
                            if (charIndex >= text.Length)
                            {
                                // start adding zeros to mark the end of the text
                                state = State.FillingWithZeros;
                            }
                            else
                            {
                                // move to the next character and process again
                                charValue = text[charIndex++];
                            }
                        }

                        // check which pixel element has the turn to hide a bit in its LSB
                        switch (pixelElementIndex%3)
                        {
                            case 0:
                            {
                                if (state == State.Hiding)
                                {
                                    // the rightmost bit in the character will be (charValue % 2)
                                    // to put this value instead of the LSB of the pixel element
                                    // just add it to it
                                    // recall that the LSB of the pixel element had been cleared
                                    // before this operation
                                    red += charValue%2;

                                    // removes the added rightmost bit of the character
                                    // such that next time we can reach the next one
                                    charValue /= 2;
                                }
                            }
                                break;
                            case 1:
                            {
                                if (state == State.Hiding)
                                {
                                    green += charValue%2;

                                    charValue /= 2;
                                }
                            }
                                break;
                            case 2:
                            {
                                if (state == State.Hiding)
                                {
                                    blue += charValue%2;

                                    charValue /= 2;
                                }

                                bmp.SetPixel(j, i, Color.FromArgb(red, green, blue));
                            }
                                break;
                        }

                        pixelElementIndex++;

                        if (state == State.FillingWithZeros)
                        {
                            // increment the value of zeros until it is 8
                            zeros++;
                        }
                    }
                }
            }
            return bmp;
        }

        #endregion
    }
}