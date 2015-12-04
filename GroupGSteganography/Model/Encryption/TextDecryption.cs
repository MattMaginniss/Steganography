namespace GroupGSteganography.Model.Encryption
{
    internal class TextDecryption
    {
        #region Properties

        private string EncryptedText { get; }
        private int RotationValue { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextDecryption" /> class.
        /// </summary>
        /// <param name="encryptedText">The encrypted text.</param>
        public TextDecryption(string encryptedText)
        {
            this.EncryptedText = encryptedText;
            this.RotationValue = 13;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextDecryption" /> class.
        /// </summary>
        /// <param name="encryptedText">The unencrypted text.</param>
        /// <param name="rotValue">The rot value.</param>
        public TextDecryption(string encryptedText, int rotValue)
        {
            this.EncryptedText = encryptedText;
            this.RotationValue = rotValue;
        }

        #endregion

        /// <summary>
        ///     Decrypts the text.
        /// </summary>
        /// <returns>returns the decrypted text.</returns>
        public string DecryptText()
        {
            return this.rotXDecryption();
        }

        private string rotXDecryption()
        {
            var array = this.EncryptedText.ToCharArray();

            for (var i = 0; i < array.Length; i++)
            {
                int number = array[i];

                if (number >= 'a' && number <= 'z')
                {
                    number = this.lowerCaseDecryption(number);
                }
                else if (number >= 'A' && number <= 'Z')
                {
                    number = this.upperCaseDecryption(number);
                }
                array[i] = (char) number;
            }
            return new string(array);
        }

        private int upperCaseDecryption(int number)
        {
            if (number - this.RotationValue < 'A')
            {
                number += 26;
                number -= this.RotationValue;
            }
            else
            {
                number -= this.RotationValue;
            }
            return number;
        }

        private int lowerCaseDecryption(int number)
        {
            if (number - this.RotationValue < 'a')
            {
                number += 26;
                number -= this.RotationValue;
            }
            else
            {
                number -= this.RotationValue;
            }
            return number;
        }
    }
}