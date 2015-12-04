using System.Drawing;

namespace GroupGSteganography.Model.Encryption
{
    internal class ImageEncryption
    {
        #region Properties

        public Image MessageImage { get; }

        #endregion

        #region Constructors

        public ImageEncryption(Image messageImage)
        {
            this.MessageImage = messageImage;
        }

        #endregion

        public Image EncryptMessageImage()
        {
            var encryptedImage = this.MessageImage;

            return encryptedImage;
        }
    }
}