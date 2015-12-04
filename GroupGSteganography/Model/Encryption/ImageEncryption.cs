using System.Drawing;

namespace GroupGSteganography.Model.Encryption
{
    internal class ImageEncryption
    {
        public Bitmap EncryptedImage { get; private set; }
        private readonly Bitmap hiderImage;
        private readonly Bitmap embeddedImage;

        public ImageEncryption(Bitmap hiderImage, Bitmap embeddedImage)
        {
            this.hiderImage = hiderImage;
            this.embeddedImage = embeddedImage;
            this.encryptImage();
        }

        private void encryptImage()
        {
            this.fitEmbeddedImage();
            this.scrambleImage();
        }

        private void fitEmbeddedImage()
        {
            var resizedCanvas = new Bitmap(this.hiderImage.Width, this.hiderImage.Height);
            var gfx = Graphics.FromImage(resizedCanvas);

            gfx.DrawImageUnscaled(this.embeddedImage, 0, 0);
            this.EncryptedImage = resizedCanvas;
        }

        private void scrambleImage()
        {
            var scrambledImage = new Bitmap(this.EncryptedImage.Width, this.EncryptedImage.Height);
            var graphics = Graphics.FromImage(scrambledImage);

            var width = this.EncryptedImage.Width;
            var height = this.EncryptedImage.Height;

            var quad1 = new Rectangle(width/2, 0, width/2, height/2);
            var quad2 = new Rectangle(0, 0, width/2, height/2);
            var quad3 = new Rectangle(0, height / 2, width/2, height/2);
            var quad4 = new Rectangle(width/2, height/2, width/2, height/2);

            graphics.DrawImage(this.EncryptedImage, quad1, quad3, GraphicsUnit.Pixel);
            graphics.DrawImage(this.EncryptedImage, quad3, quad1, GraphicsUnit.Pixel);
            graphics.DrawImage(this.EncryptedImage, quad2, quad4, GraphicsUnit.Pixel);
            graphics.DrawImage(this.EncryptedImage, quad4, quad2, GraphicsUnit.Pixel);

            graphics.Dispose();
            this.EncryptedImage = scrambledImage;
        }
    }
}