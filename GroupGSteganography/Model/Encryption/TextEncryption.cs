namespace GroupGSteganography.Model.Encryption
{
    internal class TextEncryption
    {
        #region Properties

        private string UnencryptedText { get; set; }
        private int RotationValue { get; set; }
        public string EncryptedText { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextEncryption" /> class.
        /// </summary>
        /// <param name="unencryptedText">The unencrypted text.</param>
        /// <param name="rotValue">The rot value.</param>
        public TextEncryption(string unencryptedText, int rotValue)
        {
            this.UnencryptedText = unencryptedText;
            this.RotationValue = rotValue;
        }

        #endregion
    }
}