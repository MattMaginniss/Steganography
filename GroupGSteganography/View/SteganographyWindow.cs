﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using GroupGSteganography.Model;
using GroupGSteganography.Model.Embedding;
using GroupGSteganography.Model.Extraction;

namespace GroupGSteganography.View
{
    /// <summary>
    ///     GUI code behind. Handles control events
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class SteganographyWindow : Form
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SteganographyWindow" /> class.
        /// </summary>
        public SteganographyWindow()
        {
            this.InitializeComponent();
            this.drawLines();
        }

        #endregion

        #region ActionHandlers

        private void encryptButton_Click(object sender, EventArgs e)
        {
            var headerPixel = this.createHeaderPixel();
            if (this.textRadioButton.Checked)
            {
                this.embedText(headerPixel);
            }
            else if (this.checkImageSizes())
            {
                this.embedImage(headerPixel);
            }
            else
            {
                MessageBox.Show(@"Encrypted image is too large for hider image", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            var image = (Bitmap) this.largePictureBox.Image;
            var headerPixel = HeaderPixel.From(image.GetPixel(0, 0));

            if (headerPixel.IsImage)
            {
                this.extractImage();
            }
            else
            {
                this.extractText();
            }
        }

        private void textRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox.Enabled = this.textRadioButton.Checked;
            this.qualityBar.Enabled = this.textRadioButton.Checked;

            if (!this.textRadioButton.Checked)
            {
                return;
            }
            this.updateControls();
            this.enableTextControls();
        }

        private void encryptButton_Paint(object sender, PaintEventArgs e)
        {
            this.drawLines();
        }

        private void saveBigImageButton_Click(object sender, EventArgs e)
        {
            this.saveImage(sender);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loadBigImageButton_Click(object sender, EventArgs e)
        {
            this.loadImage(sender);
        }

        private void hiderImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.loadImage(sender);
        }

        private void imageToEncryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.textRadioButton.Checked)
            {
                this.loadText();
            }
            else
            {
                this.loadImage(sender);
            }
        }

        private void loadStuffToEncryptButton_Click(object sender, EventArgs e)
        {
            if (this.textRadioButton.Checked)
            {
                this.loadText();
            }
            else
            {
                this.loadImage(sender);
            }
        }

        private void imageRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.updateControls();
            if (this.imageRadioButton.Checked)
            {
                this.loadStuffToEncryptButton.Text = @"Load Image to Encrypt";
                this.saveDecryptedButton.Text = @"Save Decrypted Image";
            }
            else
            {
                this.smallPictureBox.Image = null;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(
                @"Use this slider to adjust the level of corruption the image will experience." +
                @"The greater the corruption, the longer your hidden message can be." +
                @" However, the random alterations to the image will be more apparent", @"Image Corruption Help",
                MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void encryptedImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveImage(sender);
        }

        private bool imageIsEmpty(object sender)
        {
            if (sender == this.saveDecryptedButton || sender == this.decryptedImageToolStripMenuItem)
            {
                return this.smallPictureBox.Image == null;
            }
            return this.largePictureBox.Image == null;
        }

        private void decryptedImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveImage(sender);
        }

        private void saveDecryptedButton_Click(object sender, EventArgs e)
        {
            if (this.imageRadioButton.Checked)
            {
                this.saveImage(sender);
            }
            else
            {
                this.saveText();
            }
        }

        private void smallPictureBox_Paint(object sender, PaintEventArgs e)
        {
            this.updateControls();
        }

        private void largePictureBox_Paint(object sender, PaintEventArgs e)
        {
            this.updateControls();
        }

        private void smallPictureBoxLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.loadImage(sender);
        }

        private void smallPictureBoxSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveImage(sender);
        }

        private void bigPictureBoxLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.loadImage(sender);
        }

        private void bigPictureBoxSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveImage(sender);
        }

        private void textFieldLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.loadText();
        }

        private void qualityBar_Scroll(object sender, EventArgs e)
        {
            this.setMaxChars();
            this.truncateTextBox();
        }

        private void encryptionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.rotationUpDown.Enabled = this.encryptionCheckBox.Checked && this.textRadioButton.Checked;
        }

        private void rotHelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(
                @"The encryption used in this program is a ceaser cipher, which shifts the letters down a given number of 'slots.'" +
                @"For example, with a given shift of 1, A will become B, B will become C, and so on.",
                @"Encryption Help", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox.Paste();
        }

        private void textFieldSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveText();
        }

        #endregion

        private void drawLines()
        {
            var myPen = new Pen(Color.Black);
            var graphics = CreateGraphics();
            graphics.DrawLine(myPen, 365, 128, 610, 128);
            graphics.DrawLine(myPen, 365, 128, 375, 118);
            graphics.DrawLine(myPen, 365, 128, 375, 138);

            graphics.DrawLine(myPen, 365, 215, 610, 215);
            graphics.DrawLine(myPen, 600, 225, 610, 215);
            graphics.DrawLine(myPen, 600, 205, 610, 215);
        }

        private void enableTextControls()
        {
            this.loadStuffToEncryptButton.Text = @"Load Text File to Encrypt";
            this.saveDecryptedButton.Text = @"Save Decrypted Text";
            this.imageToEncryptToolStripMenuItem.Text = @"Text to Encrypt";
        }

        private void loadImage(object sender)
        {
            var image = this.loadImageDialog();
            if (image == null)
            {
                return;
            }
            if (this.isSmallImageLoad(sender))
            {
                this.smallPictureBox.Image = image;
            }
            else
            {
                this.largePictureBox.Image = image;
            }
        }

        private bool isSmallImageLoad(object sender)
        {
            return sender == this.loadStuffToEncryptButton || sender == this.imageToEncryptToolStripMenuItem ||
                   sender == this.smallPictureBoxLoadToolStripMenuItem;
        }

        private Bitmap loadImageDialog()
        {
            var ofd = new OpenFileDialog
            {
                Filter = @"PNG File (*.png)|*.png|Bitmap File (*.bmp)|*.bmp",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Title = @"Please select an image file to encrypt."
            };
            //tertiary statement ONLY because resharper insisted
            return ofd.ShowDialog() == DialogResult.OK ? new Bitmap(ofd.FileName) : null;
        }

        private void saveImage(object sender)
        {
            if (this.imageIsEmpty(sender))
            {
                MessageBox.Show(@"There is no image to save!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var saveDialog = new SaveFileDialog {Filter = @"PNG File (*.png)|*.png|Bitmap File (*.bmp)|*.bmp"};
            var imageFormat = ImageFormat.Png;

            if (saveDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var extension = Path.GetExtension(saveDialog.FileName);
            Debug.Assert(extension != null, "extension != null");
            if (extension.Equals(".bmp"))
            {
                imageFormat = ImageFormat.Bmp;
            }

            if (this.isSmallImageSave(sender))
            {
                this.smallPictureBox.Image.Save(saveDialog.FileName, imageFormat);
            }
            else
            {
                this.largePictureBox.Image.Save(saveDialog.FileName, imageFormat);
            }
        }

        private bool isSmallImageSave(object sender)
        {
            return sender == this.saveDecryptedButton || sender == this.decryptedImageToolStripMenuItem ||
                   sender == this.smallPictureBoxSaveToolStripMenuItem;
        }

        private void saveText()
        {
            var saveDialog = new SaveFileDialog {Filter = @"TXT File (*.txt)|*.txt"};
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveDialog.FileName, this.textBox.Text);
            }
        }

        private void updateControls()
        {
            this.updateSmallControls();

            this.updateLargeControls();
        }

        private void updateSmallControls()
        {
            var isLoaded = this.smallPictureBox.Image != null;
            this.saveDecryptedButton.Enabled = isLoaded;
            this.decryptedImageToolStripMenuItem.Enabled = isLoaded;
            this.smallPictureBoxSaveToolStripMenuItem.Enabled = isLoaded;
            if (this.imageRadioButton.Checked)
            {
                this.checkBothImageBoxes();
            }
        }

        private void updateLargeControls()
        {
            var isLoaded = this.largePictureBox.Image != null;
            this.saveBigImageButton.Enabled = isLoaded;
            this.encryptedImageToolStripMenuItem.Enabled = isLoaded;
            this.bigPictureBoxSaveToolStripMenuItem.Enabled = isLoaded;
            this.decryptButton.Enabled = isLoaded;
            if (this.imageRadioButton.Checked)
            {
                this.checkBothImageBoxes();
            }
            else
            {
                this.encryptButton.Enabled = isLoaded;
            }
            this.setMaxChars();
        }

        private void checkBothImageBoxes()
        {
            var bothImagesLoaded = this.largePictureBox.Image != null && this.smallPictureBox.Image != null;

            this.encryptButton.Enabled = bothImagesLoaded;
            this.updateImageTooLargeWarning();
        }

        private void updateImageTooLargeWarning()
        {
            var isInvalid = !this.checkImageSizes();
            this.warningLabel1.Visible = isInvalid;
            this.warningLabel2.Visible = isInvalid;
            this.warningPictureBox.Visible = isInvalid;
        }

        private void setMaxChars()
        {
            if (this.largePictureBox.Image == null)
            {
                return;
            }
            var image = this.largePictureBox.Image;
            var maxChars = image.Width*image.Height/16.0;
            var maxCharsInt = (int) Math.Truncate(maxChars)*3;
            maxCharsInt *= this.qualityBar.Value + 1;
            this.textBox.MaxLength = maxCharsInt;
            this.truncateTextBox();
        }

        private void truncateTextBox()
        {
            var text = this.textBox.Text;
            if (text.Length > this.textBox.MaxLength)
            {
                text = text.Substring(0, this.textBox.MaxLength);
            }
            this.textBox.Text = text;
        }

        private HeaderPixel createHeaderPixel()
        {
            var isImage = this.imageRadioButton.Checked;
            var isEncrypted = this.encryptionCheckBox.Checked;
            var rotShift = (int) this.rotationUpDown.Value;
            var bitsPerColorChannel = this.qualityBar.Value + 1;

            return new HeaderPixel(isImage, isEncrypted, rotShift, bitsPerColorChannel);
        }

        private void embedImage(HeaderPixel headerPixel)
        {
            if (headerPixel == null)
            {
                throw new ArgumentNullException();
            }
            IEmbeddor embeddor = new ImageEmbeddor(this.largePictureBox.Image, this.smallPictureBox.Image, headerPixel);
            this.largePictureBox.Image = embeddor.Embed();
            MessageBox.Show(@"Successfully embedded!", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void embedText(HeaderPixel headerPixel)
        {
            if (headerPixel == null)
            {
                throw new ArgumentNullException();
            }
            IEmbeddor embeddor = new TextEmbeddor(this.largePictureBox.Image, this.textBox.Text, headerPixel);
            this.largePictureBox.Image = embeddor.Embed();
            this.textBox.Text = "";
            MessageBox.Show(@"Successfully embedded!", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private bool checkImageSizes()
        {
            if (this.largePictureBox.Image == null || this.smallPictureBox.Image == null)
            {
                return true;
            }

            var hiderImage = this.largePictureBox.Image;
            var hiddenImage = this.smallPictureBox.Image;

            var isValid = hiderImage.Width >= hiddenImage.Width && hiderImage.Height >= hiddenImage.Height;

            return isValid;
        }

        private void extractText()
        {
            this.textRadioButton.Checked = true;
            this.textBox.MaxLength = int.MaxValue;
            var extractor = new TextExtractor(this.largePictureBox.Image);
            extractor.Extract();
            this.buildExtractedText(extractor);
        }

        private void buildExtractedText(TextExtractor extractor)
        {
            if (extractor == null)
            {
                throw new ArgumentNullException();
            }
            var text = extractor.ExtractedText;
            if (extractor.EncryptedText != null)
            {
                text = "Encrypted: " + extractor.EncryptedText + Environment.NewLine + Environment.NewLine +
                       "Decrypted: " + text + Environment.NewLine + Environment.NewLine + "Shift: " +
                       extractor.HeaderPixel.RotShift;
            }
            this.textBox.Text = text;
        }

        private void extractImage()
        {
            this.imageRadioButton.Checked = true;
            var extractor = new ImageExtractor(this.largePictureBox.Image);
            extractor.Extract();
            this.smallPictureBox.Image = extractor.ExtractedImage;
        }

        private void loadText()
        {
            var ofdText = new OpenFileDialog
            {
                Filter = @"Text File (*.txt)|*.txt",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (ofdText.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var text = File.ReadAllText(ofdText.FileName);
            if (text.Length > this.textBox.MaxLength)
            {
                text = text.Substring(0, this.textBox.MaxLength);
            }
            this.textBox.Text = text;
        }
    }
}