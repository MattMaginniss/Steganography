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
            this.ExtractedText = this.decodeMessageInImage((Bitmap) this.EncodedImage);
        }

        #endregion

        private string decodeMessageInImage(Bitmap bm)
        {
            // Initialize a random number generator.
            var rand = new Random(1234);

            // Create a new HashSet.
            var usedPositions = new HashSet<string>();

            // Make a byte array big enough to hold the message length.
            var len = 0;
            var bytes = BitConverter.GetBytes(len);

            // Decode the message length.
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = this.decodeByte(bm, rand, usedPositions);
            }
            len = BitConverter.ToInt32(bytes, 0);

            // Sanity check.
            if (len > 10000)
            {
                throw new InvalidDataException(
                    "Message length " + len +
                    " is too big to make sense. Invalid password.");
            }

            // Decode the message bytes.
            var chars = new char[len];
            for (var i = 0; i < chars.Length; i++)
            {
                chars[i] = (char) this.decodeByte(bm, rand, usedPositions);
            }
            return new string(chars);
        }

        // Decode a byte.
        private byte decodeByte(Bitmap bm, Random rand, HashSet<string> usedPositions)
        {
            byte value = 0;
            byte valueMask = 1;
            for (var i = 0; i < 8; i++)
            {
                // Find the position for the ith bit.
                int row, col, pix;
                this.pickPosition(bm, rand, usedPositions, out row, out col, out pix);

                // Get the color component value.
                byte colorValue = 0;
                switch (pix)
                {
                    case 0:
                        colorValue = bm.GetPixel(row, col).R;
                        break;
                    case 1:
                        colorValue = bm.GetPixel(row, col).G;
                        break;
                    case 2:
                        colorValue = bm.GetPixel(row, col).B;
                        break;
                }

                // Set the next bit if appropriate.
                if ((colorValue & 1) == 1)
                {
                    // Set the bit.
                    value = (byte) (value | valueMask);
                }

                // Move to the next bit.
                valueMask <<= 1;
            }

            return value;
        }

        private void pickPosition(Bitmap bm, Random rand,
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