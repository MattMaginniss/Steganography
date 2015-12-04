using System;

namespace GroupGSteganography.Model.Encryption
{
    internal class TextEncryption
    {
        #region Data members

        private readonly string unencryptedText;
        private readonly int rotationValue;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextEncryption" /> class.
        /// </summary>
        /// <param name="unencryptedText">The unencrypted text.</param>
        public TextEncryption(string unencryptedText)
        {
            if (unencryptedText == null)
            {
                throw new ArgumentNullException();
            }
            this.unencryptedText = unencryptedText;
            this.rotationValue = 13;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextEncryption" /> class.
        /// </summary>
        /// <param name="unencryptedText">The unencrypted text.</param>
        /// <param name="rotValue">The rot value.</param>
        public TextEncryption(string unencryptedText, int rotValue)
        {
            if (unencryptedText == null)
            {
                throw new ArgumentNullException();
            }
            this.unencryptedText = unencryptedText;
            this.rotationValue = rotValue;
        }

        #endregion

        /// <summary>
        ///     Encrypts the text.
        /// </summary>
        /// <returns>Encrypted text using the ROTx method. x standing for the rotValue.</returns>
        public string EncryptText()
        {
            return this.rotXEncryption();
        }

        private string rotXEncryption()
        {
            var array = this.unencryptedText.ToCharArray();

            for (var i = 0; i < array.Length; i++)
            {
                int number = array[i];

                if (number >= 'a' && number <= 'z')
                {
                    number = this.lowerCaseEncryption(number);
                }
                else if (number >= 'A' && number <= 'Z')
                {
                    number = this.upperCaseEncryption(number);
                }
                array[i] = (char) number;
            }
            return new string(array);
        }

        private int upperCaseEncryption(int number)
        {
            if (number + this.rotationValue > 'Z')
            {
                number -= 26;
                number += this.rotationValue;
            }
            else
            {
                number += this.rotationValue;
            }
            return number;
        }

        private int lowerCaseEncryption(int number)
        {
            if (number + this.rotationValue > 'z')
            {
                number -= 26;
                number += this.rotationValue;
            }
            else
            {
                number += this.rotationValue;
            }
            return number;
        }
    }
}