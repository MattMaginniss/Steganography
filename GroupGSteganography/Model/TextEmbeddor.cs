using System;
using System.Collections.Generic;
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
        public string EmbeddedText { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextEmbeddor" /> class.
        /// </summary>
        /// <param name="sourceImage">The source image.</param>
        /// <param name="textToEmbed">The text to embed.</param>
        public TextEmbeddor(Image sourceImage, string textToEmbed)
        {
            this.SourceImage = sourceImage;
            this.EmbeddedText = textToEmbed;
        }

        #endregion

        #region Methods

        public Image Embed()
        {
            var encodedImage = (Bitmap) this.SourceImage.Clone();
            var markedImage = (Bitmap) this.SourceImage.Clone();

            this.encodeMessageInImage(encodedImage, markedImage, this.EmbeddedText);

            return markedImage;
        }

        #endregion

        private void encodeMessageInImage(Bitmap bm, Bitmap visible_bm, string message)
        {
            // Initialize a random number generator.
            var rand = new Random(1234);

            // Create a new HashSet.
            var usedPositions = new HashSet<string>();

            // Encode the message length.
            var bytes = BitConverter.GetBytes(message.Length);
            for (var i = 0; i < bytes.Length; i++)
            {
                this.encodeByte(bm, visible_bm, rand, bytes[i], usedPositions);
            }

            // Encode the message.
            var chars = message.ToCharArray();
            for (var i = 0; i < chars.Length; i++)
            {
                this.encodeByte(bm, visible_bm, rand, (byte) chars[i], usedPositions);
            }
        }

        private void encodeByte(Bitmap bm, Bitmap visibleBm, Random rand,
            byte value, HashSet<string> usedPositions)
        {
            for (var i = 0; i < 8; i++)
            {
                // Pick a position for the ith bit.
                int row, col, pix;
                this.PickPosition(bm, rand, usedPositions, out row, out col, out pix);

                // Get the color's pixel components.
                var clr = bm.GetPixel(row, col);
                var r = clr.R;
                var g = clr.G;
                var b = clr.B;

                // Get the next bit to store.
                var bit = 0;
                if ((value & 1) == 1)
                {
                    bit = 1;
                }

                // Update the color.
                switch (pix)
                {
                    case 0:
                        r = (byte) ((r & 0xFE) | bit);
                        break;
                    case 1:
                        g = (byte) ((g & 0xFE) | bit);
                        break;
                    case 2:
                        b = (byte) ((b & 0xFE) | bit);
                        break;
                }
                clr = Color.FromArgb(clr.A, r, g, b);
                bm.SetPixel(row, col, clr);

                // Display a red pixel.
                visibleBm.SetPixel(row, col, Color.Red);

                // Move to the next bit in the value.
                value >>= 1;
            }
        }

        private void PickPosition(Bitmap bm, Random rand,
            HashSet<string> usedPositions,
            out int row, out int col, out int pix)
        {
            for (;;)
            {
                // Pick random r, c, and pix.
                row = rand.Next(0, bm.Width);
                col = rand.Next(0, bm.Height);
                pix = rand.Next(0, 3);

                // See if this location is available.
                var key =
                    row + "/" +
                    col + "/" +
                    pix;
                if (!usedPositions.Contains(key))
                {
                    usedPositions.Add(key);
                    return;
                }
            }
        }
    }
}