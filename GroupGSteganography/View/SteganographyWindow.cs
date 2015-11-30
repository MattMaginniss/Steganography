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
        }


        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void loadBigImageButton_Click(object sender, System.EventArgs e)
        {
            this.loadHidingImage();
        }

        private void hiderImageToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.loadHidingImage();
        }

        private void loadHidingImage()
        {
            var ofd = new OpenFileDialog
            {
                Filter = @"PNG File (*.png)|*.png|Bitmap File (*.bmp)|*.bmp",
                InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Title = @"Please select an image file to encrypt."
        };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var image = new Bitmap(ofd.FileName);
                this.largePictureBox.Image = image;
            }
        }
    }
}