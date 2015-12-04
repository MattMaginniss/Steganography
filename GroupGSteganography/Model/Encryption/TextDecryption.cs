using System;

namespace GroupGSteganography.Model.Encryption
{
    internal class TextDecryption
    {
        #region Data members

        private readonly string encryptedText;
        private readonly int rotationValue;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextDecryption" /> class.
        /// </summary>
        /// <param name="encryptedText">The encrypted text.</param>
        public TextDecryption(string encryptedText)
        {
            if (encryptedText == null)
            {
                throw new ArgumentNullException();
            }
            this.encryptedText = encryptedText;
            this.rotationValue = 13;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextDecryption" /> class.
        /// </summary>
        /// <param name="encryptedText">The unencrypted text.</param>
        /// <param name="rotValue">The rot value.</param>
        public TextDecryption(string encryptedText, int rotValue)
        {
            if (encryptedText == null)
            {
                throw new ArgumentNullException();
            }
            this.encryptedText = encryptedText;
            this.rotationValue = rotValue;
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
            var array = this.encryptedText.ToCharArray();

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
            if (number - this.rotationValue < 'A')
            {
                number += 26;
                number -= this.rotationValue;
            }
            else
            {
                number -= this.rotationValue;
            }
            return number;
        }

        private int lowerCaseDecryption(int number)
        {
            if (number - this.rotationValue < 'a')
            {
                number += 26;
                number -= this.rotationValue;
            }
            else
            {
                number -= this.rotationValue;
            }
            return number;
        }
    }
}