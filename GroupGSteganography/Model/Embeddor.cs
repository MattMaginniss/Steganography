using System.Drawing;

namespace GroupGSteganography.Model
{
    public class Embeddor
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the source image.
        /// </summary>
        /// <value>
        ///     The source image.
        /// </value>
        public Image SourceImage { get; set; }

        /// <summary>
        ///     Gets or sets the embeded image.
        /// </summary>
        /// <value>
        ///     The embeded image.
        /// </value>
        public Image EmbededImage { get; set; }

        /// <summary>
        ///     Gets or sets the embedded text.
        /// </summary>
        /// <value>
        ///     The embedded text.
        /// </value>
        public string EmbeddedText { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Embeddor" /> class.
        /// </summary>
        /// <param name="sourceImage">The source image.</param>
        public Embeddor(Image sourceImage)
        {
            this.SourceImage = sourceImage;
        }

        #endregion
    }
}