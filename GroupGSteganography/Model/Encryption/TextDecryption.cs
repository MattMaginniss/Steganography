namespace GroupGSteganography.Model.Encryption
{
    internal class TextDecryption
    {
        #region Properties

        private string EncryptedText { get; }
        private int RotationValue { get; }

        #endregion

        #region Constructors

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

        public string DecryptText()
        {
            return this.rotXEncryption();
        }

        private string rotXEncryption()
        {
            var array = this.EncryptedText.ToCharArray();

            for (var i = 0; i < array.Length; i++)
            {
                int number = array[i];

                if (number >= 'a' && number <= 'z')
                {
                    if (number - this.RotationValue > 'z')
                    {
                        number += 26;
                    }
                    else
                    {
                        number -= this.RotationValue;
                    }
                }
                else if (number >= 'A' && number <= 'Z')
                {
                    if (number - this.RotationValue > 'Z')
                    {
                        number += 26;
                    }
                    else
                    {
                        number -= this.RotationValue;
                    }
                }
                array[i] = (char) number;
            }
            return new string(array);
        }
    }
}