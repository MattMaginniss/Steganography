using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

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

        }

        private void textRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            this.textBox.Enabled = this.textRadioButton.Checked;

            if (!this.textRadioButton.Checked)
            {
                return;
            }
            this.loadStuffToEncryptButton.Text = @"Load Text to Encrypt";
            this.saveDecryptedButton.Text = @"Save Decrypted Text";
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
            this.loadImage(sender);
        }

        private void loadStuffToEncryptButton_Click(object sender, EventArgs e)
        {
            this.loadImage(sender);
        }

        private void loadImage(object sender)
        {
            var image = this.loadImageDialog();
            if (image == null)
            {
                return;
            }
            if (sender == this.loadStuffToEncryptButton || sender == this.imageToEncryptToolStripMenuItem)
            {
                this.smallPictureBox.Image = image;
            }
            else
            {
                this.largePictureBox.Image = image;
            }
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
    }
}