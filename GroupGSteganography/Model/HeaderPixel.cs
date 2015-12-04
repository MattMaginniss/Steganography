using System.Drawing;

namespace GroupGSteganography.Model
{
    /// <summary>
    /// Class to generate a headerpixel to store info about an encrypted image
    /// </summary>
    public class HeaderPixel
    {
        #region Data members

        private int red;
        private int green;
        private int blue;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is an image.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is image; otherwise, <c>false</c> if this image is text.
        /// </value>
        public bool IsImage { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is encrypted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is encrypted; otherwise, <c>false</c>.
        /// </value>
        public bool IsEncrypted { get; }

        /// <summary>
        /// Gets the rotation shift of the Ceasar Cipher.
        /// </summary>
        /// <value>
        /// The rotation shift.
        /// </value>
        public int RotShift { get; }

        /// <summary>
        /// Gets the bits per color channel.
        /// </summary>
        /// <value>
        /// The bits per color channel.
        /// </value>
        public int BitsPerColorChannel { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderPixel"/> class.
        /// </summary>
        /// <param name="isImage">if set to <c>true</c> [is image].</param>
        /// <param name="isEncrypted">if set to <c>true</c> [is encrypted].</param>
        /// <param name="rotShift">The rot shift.</param>
        /// <param name="bitsPerColorChannel">The bits per color channel.</param>
        public HeaderPixel(bool isImage, bool isEncrypted, int rotShift, int bitsPerColorChannel)
        {
            this.IsImage = isImage;
            this.IsEncrypted = isEncrypted;
            this.RotShift = rotShift;
            this.BitsPerColorChannel = bitsPerColorChannel;
            this.buildPixel();
        }

        #endregion

        /// <summary>
        /// Gets the Color to embed as the first pixel, serving as the Header Pixel.
        /// </summary>
        /// <returns>New Color with appropriate values embedded</returns>
        public Color GetColor()
        {
            return Color.FromArgb(this.red, this.green, this.blue);
        }

        /// <summary>
        /// Static method to build a new HeaderPixel from a given Color (pixel)
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>New HeaderPixel with values extracted from the Color</returns>
        public static HeaderPixel From(Color color)
        {
            var isImage = false;
            var isEncrypted = false;
            var rotShift = 0;
            var bitsPerColorChannel = color.B;

            if (color.R == 0)
            {
                isImage = true;
            }
            if (color.G >= 100)
            {
                isEncrypted = true;
                rotShift = color.G - 100;
            }
            return new HeaderPixel(isImage, isEncrypted, rotShift, bitsPerColorChannel);
        }

        private void buildPixel()
        {
            this.buildRed();
            this.buildGreen();
            this.buildBlue();
        }

        private void buildRed()
        {
            if (this.IsImage)
            {
                this.red = 0;
            }
            else
            {
                this.red = 1;
            }
        }

        private void buildGreen()
        {
            if (!this.IsEncrypted)
            {
                this.green = 0;
            }
            else
            {
                this.green = 100;
                this.green += this.RotShift;
            }
        }

        private void buildBlue()
        {
            this.blue = this.BitsPerColorChannel;
        }
    }
}