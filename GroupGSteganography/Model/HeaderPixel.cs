using System.Drawing;

namespace GroupGSteganography.Model
{
    public class HeaderPixel
    {
        #region Data members

        private int red;
        private int green;
        private int blue;

        #endregion

        #region Properties

        public bool IsImage { get; }
        public bool IsEncrypted { get; }
        public int RotShift { get; }
        public int BitsPerColorChannel { get; }

        #endregion

        #region Constructors

        public HeaderPixel(bool isImage, bool isEncrypted, int rotShift, int bitsPerColorChannel)
        {
            this.IsImage = isImage;
            this.IsEncrypted = isEncrypted;
            this.RotShift = rotShift;
            this.BitsPerColorChannel = bitsPerColorChannel;
            this.buildPixel();
        }

        #endregion

        public Color GetColor()
        {
            return Color.FromArgb(this.red, this.green, this.blue);
        }

        public static HeaderPixel From(Color color)
        {
            var isImage = false;
            var isEncrypted = false;
            var rotShift = 0;
            var bitsPerColorChannel = 0;

            if (color.R == 0)
            {
                isImage = true;
            }
            if (color.G >= 100)
            {
                isEncrypted = true;
                rotShift = color.G - 100;
            }
            bitsPerColorChannel = color.B;
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