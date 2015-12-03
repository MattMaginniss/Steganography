using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using GroupGSteganography.Model;

namespace GroupGSteganography.View
{
    public partial class SteganographyWindow : Form
    {
        #region Constructors

        public SteganographyWindow()
        {
            this.InitializeComponent();
            this.drawLines();
        }

        #endregion

        private void drawLines()
        {

            var myPen = new Pen(System.Drawing.Color.Black);
                var graphics = this.CreateGraphics();
           graphics.DrawLine(myPen, 365, 128, 610, 128);
            graphics.DrawLine(myPen, 365, 128, 375, 118);
            graphics.DrawLine(myPen, 365, 128, 375, 138);

            graphics.DrawLine(myPen, 365, 215, 610, 215);
            graphics.DrawLine(myPen, 600, 225, 610, 215);
            graphics.DrawLine(myPen, 600, 205, 610, 215);
        }

        private void encryptButton_Paint(object sender, PaintEventArgs e)
        {
            this.drawLines();
        }

        private void saveBigImageButton_Click(object sender, System.EventArgs e)
        {
            this.saveImage(sender);
        }

        private void textRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            this.textBox.Enabled = this.textRadioButton.Checked;
            this.qualityBar.Enabled = this.textRadioButton.Checked;

            if (!this.textRadioButton.Checked)
            {
                return;
            }
            this.enableTextControls();

        }

        private void enableTextControls()
        {
            this.loadStuffToEncryptButton.Text = @"Load Text to Encrypt";
            this.saveDecryptedButton.Text = @"Save Decrypted Text";
            this.imageToEncryptToolStripMenuItem.Text = @"Text to Encrypt";
        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void loadBigImageButton_Click(object sender, System.EventArgs e)
        {
            this.loadImage(sender);
        }

        private void hiderImageToolStripMenuItem_Click(object sender, System.EventArgs e)
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
            return sender == this.loadStuffToEncryptButton || sender == this.imageToEncryptToolStripMenuItem || sender == this.textFieldLoadToolStripMenuItem;
        }

        private Bitmap loadImageDialog()
        {
            var ofd = new OpenFileDialog
            {
                Filter = @"PNG File (*.png)|*.png|Bitmap File (*.bmp)|*.bmp",
                InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Title = @"Please select an image file to encrypt."
            };
            //tertiary statement ONLY because resharper insisted
            return ofd.ShowDialog() == DialogResult.OK ? new Bitmap(ofd.FileName) : null;
        }

        private void imageRadioButton_CheckedChanged(object sender, EventArgs e)
        {

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
                @" However, the random alterations to the image will be more apparent", @"Image Corruption Help", MessageBoxButtons.OK, MessageBoxIcon.Question );
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
            else
            {
                return this.largePictureBox.Image == null;
            }

        }

        private void decryptedImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveImage(sender);
        }

        private void saveImage(object sender)
        {
            if (this.imageIsEmpty(sender))
            {
                MessageBox.Show(@"There is no image to save!", @"Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            return sender == this.saveDecryptedButton || sender == this.decryptedImageToolStripMenuItem || sender == this.smallPictureBoxSaveToolStripMenuItem;
        }

        private void saveDecryptedButton_Click(object sender, EventArgs e)
        {
            this.saveImage(sender);
        }

        private void smallPictureBox_Paint(object sender, PaintEventArgs e)
        {
            var canLoad = this.smallPictureBox.Image != null;
            this.saveDecryptedButton.Enabled = canLoad;
            this.encryptedImageToolStripMenuItem.Enabled = canLoad;
            this.smallPictureBoxSaveToolStripMenuItem.Enabled = canLoad;
            if (this.imageRadioButton.Checked)
            {
                this.checkBothImageBoxes();
            }
        }

        private void checkBothImageBoxes()
        {
            var bothImagesLoaded = (this.largePictureBox.Image != null && this.smallPictureBox.Image != null);

            this.encryptButton.Enabled = bothImagesLoaded;
        }


        private void largePictureBox_Paint(object sender, PaintEventArgs e)
        {
            var isLoaded = this.largePictureBox.Image != null;
            this.saveBigImageButton.Enabled = isLoaded;
            this.decryptedImageToolStripMenuItem.Enabled = isLoaded;
            this.bigPictureBoxSaveToolStripMenuItem.Enabled = isLoaded;
            this.decryptButton.Enabled = isLoaded;
            if (this.imageRadioButton.Checked)
            {
                this.checkBothImageBoxes();

            }
        }

        private void encryptButton_Click(object sender, EventArgs e)
        {
            IEmbeddor embeddor;
            if (this.textRadioButton.Checked)
            {
                embeddor = new TextEmbeddor(this.largePictureBox.Image, this.textBox.Text);
                this.largePictureBox.Image = embeddor.Embed();
            }
            else
            {
                embeddor = new ImageEmbeddor(this.largePictureBox.Image, this.smallPictureBox.Image);
                this.largePictureBox.Image = embeddor.Embed();
            }
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {

            if (this.textRadioButton.Checked)
            {
                TextExtractor extractor = new TextExtractor(this.largePictureBox.Image);
                extractor.Extract();
                this.textBox.Text = extractor.ExtractedText;
            }
            else
            {
                ImageExtractor extractor = new ImageExtractor(this.largePictureBox.Image);
                extractor.Extract();
                this.largePictureBox.Image = extractor.ExtractedImage;
            }
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

        private void loadText()
        {
            var ofdText = new OpenFileDialog()
            {
                Filter = @"Text File (*.txt)|*.txt",
                InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                
            };
            if (ofdText.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var text = System.IO.File.ReadAllText(ofdText.FileName);
            if (text.Length > this.textBox.MaxLength)
            {
                text = text.Substring(0, this.textBox.MaxLength);
            }
            this.textBox.Text = text;

        }
    }
}