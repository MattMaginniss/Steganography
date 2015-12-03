namespace GroupGSteganography.Model.Encryption
{
    internal class TextEncryption
    {
        #region Properties

        private string UnencryptedText { get; }
        private int RotationValue { get; set; }

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

        public string EncryptText()
        {
            var array = this.UnencryptedText.ToCharArray();
            for (var i = 0; i < array.Length; i++)
            {
                int number = array[i];

                if (number >= 'a' && number <= 'z')
                {
                    if (number > 'm')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                else if (number >= 'A' && number <= 'Z')
                {
                    if (number > 'M')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                array[i] = (char) number;
            }
            return new string(array);
        }
    }
}