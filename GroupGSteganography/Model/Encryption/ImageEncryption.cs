using System.Drawing;

namespace GroupGSteganography.Model.Encryption
{
    internal class ImageEncryption
    {
        public Image MessageImage { get; private set; }
        
        
        public ImageEncryption(Image messageImage)
        {
            this.MessageImage = messageImage;
        }

        public Image EncryptMessageImage()
        {
            var encryptedImage = this.MessageImage;
            

            return encryptedImage;
        }
    }
}