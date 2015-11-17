using System.Drawing;

namespace GroupGSteganography.Model
{
    public class Extractor
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the extracted image.
        /// </summary>
        /// <value>
        ///     The extracted image.
        /// </value>
        public Image ExtractedImage { get; set; }

        /// <summary>
        ///     Gets or sets the extracted text.
        /// </summary>
        /// <value>
        ///     The extracted text.
        /// </value>
        public string ExtractedText { get; set; }

        #endregion
    }
}