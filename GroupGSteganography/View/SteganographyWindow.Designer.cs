using System.Drawing;

namespace GroupGSteganography.View
{
    partial class SteganographyWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SteganographyWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hiderImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToEncryptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encryptedImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decryptedImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largePictureBox = new System.Windows.Forms.PictureBox();
            this.bigPictureBoxContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.smallPictureBox = new System.Windows.Forms.PictureBox();
            this.smallPictureBoxContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox = new System.Windows.Forms.TextBox();
            this.textFieldContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loadToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encryptButton = new System.Windows.Forms.Button();
            this.decryptButton = new System.Windows.Forms.Button();
            this.imageRadioButton = new System.Windows.Forms.RadioButton();
            this.textRadioButton = new System.Windows.Forms.RadioButton();
            this.saveBigImageButton = new System.Windows.Forms.Button();
            this.saveDecryptedButton = new System.Windows.Forms.Button();
            this.loadBigImageButton = new System.Windows.Forms.Button();
            this.loadStuffToEncryptButton = new System.Windows.Forms.Button();
            this.qualityBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.helpLinkLabel = new System.Windows.Forms.LinkLabel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.largePictureBox)).BeginInit();
            this.bigPictureBoxContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smallPictureBox)).BeginInit();
            this.smallPictureBoxContextMenu.SuspendLayout();
            this.textFieldContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualityBar)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(868, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hiderImageToolStripMenuItem,
            this.imageToEncryptToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.openToolStripMenuItem.Text = "&Load...";
            // 
            // hiderImageToolStripMenuItem
            // 
            this.hiderImageToolStripMenuItem.Name = "hiderImageToolStripMenuItem";
            this.hiderImageToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.hiderImageToolStripMenuItem.Text = "Image to &Hide Message";
            this.hiderImageToolStripMenuItem.Click += new System.EventHandler(this.hiderImageToolStripMenuItem_Click);
            // 
            // imageToEncryptToolStripMenuItem
            // 
            this.imageToEncryptToolStripMenuItem.Name = "imageToEncryptToolStripMenuItem";
            this.imageToEncryptToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.imageToEncryptToolStripMenuItem.Text = "Image to &Encrypt";
            this.imageToEncryptToolStripMenuItem.Click += new System.EventHandler(this.imageToEncryptToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.encryptedImageToolStripMenuItem,
            this.decryptedImageToolStripMenuItem});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.saveToolStripMenuItem.Text = "&Save...";
            // 
            // encryptedImageToolStripMenuItem
            // 
            this.encryptedImageToolStripMenuItem.Name = "encryptedImageToolStripMenuItem";
            this.encryptedImageToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.encryptedImageToolStripMenuItem.Text = "&Encrypted Image";
            this.encryptedImageToolStripMenuItem.Click += new System.EventHandler(this.encryptedImageToolStripMenuItem_Click);
            // 
            // decryptedImageToolStripMenuItem
            // 
            this.decryptedImageToolStripMenuItem.Name = "decryptedImageToolStripMenuItem";
            this.decryptedImageToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.decryptedImageToolStripMenuItem.Text = "&Decrypted Image";
            this.decryptedImageToolStripMenuItem.Click += new System.EventHandler(this.decryptedImageToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.viewHelpToolStripMenuItem.Text = "&View Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            // 
            // largePictureBox
            // 
            this.largePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.largePictureBox.ContextMenuStrip = this.bigPictureBoxContextMenu;
            this.largePictureBox.Location = new System.Drawing.Point(12, 27);
            this.largePictureBox.Name = "largePictureBox";
            this.largePictureBox.Size = new System.Drawing.Size(350, 350);
            this.largePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.largePictureBox.TabIndex = 1;
            this.largePictureBox.TabStop = false;
            // 
            // bigPictureBoxContextMenu
            // 
            this.bigPictureBoxContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem1});
            this.bigPictureBoxContextMenu.Name = "bigPictureBoxContextMenu";
            this.bigPictureBoxContextMenu.Size = new System.Drawing.Size(101, 48);
            this.bigPictureBoxContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.bigPictureBoxContextMenu_Opening);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "&Load";
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem1.Text = "&Save";
            // 
            // smallPictureBox
            // 
            this.smallPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.smallPictureBox.ContextMenuStrip = this.smallPictureBoxContextMenu;
            this.smallPictureBox.Location = new System.Drawing.Point(613, 27);
            this.smallPictureBox.Name = "smallPictureBox";
            this.smallPictureBox.Size = new System.Drawing.Size(200, 200);
            this.smallPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.smallPictureBox.TabIndex = 2;
            this.smallPictureBox.TabStop = false;
            // 
            // smallPictureBoxContextMenu
            // 
            this.smallPictureBoxContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem1,
            this.saveToolStripMenuItem2});
            this.smallPictureBoxContextMenu.Name = "smallPictureBoxContextMenu";
            this.smallPictureBoxContextMenu.Size = new System.Drawing.Size(101, 48);
            // 
            // loadToolStripMenuItem1
            // 
            this.loadToolStripMenuItem1.Name = "loadToolStripMenuItem1";
            this.loadToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem1.Text = "Load";
            // 
            // saveToolStripMenuItem2
            // 
            this.saveToolStripMenuItem2.Name = "saveToolStripMenuItem2";
            this.saveToolStripMenuItem2.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem2.Text = "Save";
            // 
            // textBox
            // 
            this.textBox.ContextMenuStrip = this.textFieldContextMenuStrip;
            this.textBox.Enabled = false;
            this.textBox.Location = new System.Drawing.Point(613, 233);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(200, 144);
            this.textBox.TabIndex = 3;
            // 
            // textFieldContextMenuStrip
            // 
            this.textFieldContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem2,
            this.saveToolStripMenuItem3,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.textFieldContextMenuStrip.Name = "textFieldContextMenuStrip";
            this.textFieldContextMenuStrip.Size = new System.Drawing.Size(103, 92);
            // 
            // loadToolStripMenuItem2
            // 
            this.loadToolStripMenuItem2.Name = "loadToolStripMenuItem2";
            this.loadToolStripMenuItem2.Size = new System.Drawing.Size(102, 22);
            this.loadToolStripMenuItem2.Text = "Load";
            // 
            // saveToolStripMenuItem3
            // 
            this.saveToolStripMenuItem3.Name = "saveToolStripMenuItem3";
            this.saveToolStripMenuItem3.Size = new System.Drawing.Size(102, 22);
            this.saveToolStripMenuItem3.Text = "Save";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // encryptButton
            // 
            this.encryptButton.Location = new System.Drawing.Point(443, 117);
            this.encryptButton.Name = "encryptButton";
            this.encryptButton.Size = new System.Drawing.Size(75, 23);
            this.encryptButton.TabIndex = 1;
            this.encryptButton.Text = "Encrypt";
            this.encryptButton.UseVisualStyleBackColor = true;
            this.encryptButton.Click += new System.EventHandler(this.encryptButton_Click);
            this.encryptButton.Paint += new System.Windows.Forms.PaintEventHandler(this.encryptButton_Paint);
            // 
            // decryptButton
            // 
            this.decryptButton.Location = new System.Drawing.Point(443, 204);
            this.decryptButton.Name = "decryptButton";
            this.decryptButton.Size = new System.Drawing.Size(75, 23);
            this.decryptButton.TabIndex = 2;
            this.decryptButton.Text = "Decrypt";
            this.decryptButton.UseVisualStyleBackColor = true;
            this.decryptButton.Click += new System.EventHandler(this.decryptButton_Click);
            // 
            // imageRadioButton
            // 
            this.imageRadioButton.AutoSize = true;
            this.imageRadioButton.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.imageRadioButton.Checked = true;
            this.imageRadioButton.Location = new System.Drawing.Point(420, 281);
            this.imageRadioButton.Name = "imageRadioButton";
            this.imageRadioButton.Size = new System.Drawing.Size(98, 17);
            this.imageRadioButton.TabIndex = 3;
            this.imageRadioButton.TabStop = true;
            this.imageRadioButton.Text = "Image -> Image";
            this.imageRadioButton.UseVisualStyleBackColor = true;
            this.imageRadioButton.CheckedChanged += new System.EventHandler(this.imageRadioButton_CheckedChanged);
            // 
            // textRadioButton
            // 
            this.textRadioButton.AutoSize = true;
            this.textRadioButton.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.textRadioButton.Location = new System.Drawing.Point(428, 315);
            this.textRadioButton.Name = "textRadioButton";
            this.textRadioButton.Size = new System.Drawing.Size(90, 17);
            this.textRadioButton.TabIndex = 4;
            this.textRadioButton.Text = "Text -> Image";
            this.textRadioButton.UseVisualStyleBackColor = true;
            this.textRadioButton.CheckedChanged += new System.EventHandler(this.textRadioButton_CheckedChanged);
            // 
            // saveBigImageButton
            // 
            this.saveBigImageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveBigImageButton.Location = new System.Drawing.Point(12, 412);
            this.saveBigImageButton.Name = "saveBigImageButton";
            this.saveBigImageButton.Size = new System.Drawing.Size(350, 23);
            this.saveBigImageButton.TabIndex = 6;
            this.saveBigImageButton.Text = "Save Encrypted Image";
            this.saveBigImageButton.UseVisualStyleBackColor = true;
            this.saveBigImageButton.Click += new System.EventHandler(this.saveBigImageButton_Click);
            // 
            // saveDecryptedButton
            // 
            this.saveDecryptedButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveDecryptedButton.Location = new System.Drawing.Point(613, 412);
            this.saveDecryptedButton.Name = "saveDecryptedButton";
            this.saveDecryptedButton.Size = new System.Drawing.Size(200, 23);
            this.saveDecryptedButton.TabIndex = 8;
            this.saveDecryptedButton.Text = "Save Decrypted Image";
            this.saveDecryptedButton.UseVisualStyleBackColor = true;
            this.saveDecryptedButton.Click += new System.EventHandler(this.saveDecryptedButton_Click);
            // 
            // loadBigImageButton
            // 
            this.loadBigImageButton.Location = new System.Drawing.Point(12, 383);
            this.loadBigImageButton.Name = "loadBigImageButton";
            this.loadBigImageButton.Size = new System.Drawing.Size(350, 23);
            this.loadBigImageButton.TabIndex = 5;
            this.loadBigImageButton.Text = "Load Image";
            this.loadBigImageButton.UseVisualStyleBackColor = true;
            this.loadBigImageButton.Click += new System.EventHandler(this.loadBigImageButton_Click);
            // 
            // loadStuffToEncryptButton
            // 
            this.loadStuffToEncryptButton.Location = new System.Drawing.Point(613, 383);
            this.loadStuffToEncryptButton.Name = "loadStuffToEncryptButton";
            this.loadStuffToEncryptButton.Size = new System.Drawing.Size(200, 23);
            this.loadStuffToEncryptButton.TabIndex = 7;
            this.loadStuffToEncryptButton.Text = "Load Image to Encrypt";
            this.loadStuffToEncryptButton.UseVisualStyleBackColor = true;
            this.loadStuffToEncryptButton.Click += new System.EventHandler(this.loadStuffToEncryptButton_Click);
            // 
            // qualityBar
            // 
            this.qualityBar.Enabled = false;
            this.qualityBar.LargeChange = 1;
            this.qualityBar.Location = new System.Drawing.Point(420, 383);
            this.qualityBar.Maximum = 2;
            this.qualityBar.Name = "qualityBar";
            this.qualityBar.Size = new System.Drawing.Size(108, 45);
            this.qualityBar.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(425, 412);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Low";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(489, 412);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "High";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(425, 364);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Image Corruption";
            // 
            // helpLinkLabel
            // 
            this.helpLinkLabel.AutoSize = true;
            this.helpLinkLabel.Location = new System.Drawing.Point(509, 364);
            this.helpLinkLabel.Name = "helpLinkLabel";
            this.helpLinkLabel.Size = new System.Drawing.Size(19, 13);
            this.helpLinkLabel.TabIndex = 16;
            this.helpLinkLabel.TabStop = true;
            this.helpLinkLabel.Text = "(?)";
            this.helpLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // SteganographyWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 445);
            this.Controls.Add(this.helpLinkLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.qualityBar);
            this.Controls.Add(this.loadStuffToEncryptButton);
            this.Controls.Add(this.loadBigImageButton);
            this.Controls.Add(this.saveDecryptedButton);
            this.Controls.Add(this.saveBigImageButton);
            this.Controls.Add(this.textRadioButton);
            this.Controls.Add(this.imageRadioButton);
            this.Controls.Add(this.decryptButton);
            this.Controls.Add(this.encryptButton);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.smallPictureBox);
            this.Controls.Add(this.largePictureBox);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SteganographyWindow";
            this.Text = "Steganography by Nathan Beam and Matt Maginniss";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.largePictureBox)).EndInit();
            this.bigPictureBoxContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smallPictureBox)).EndInit();
            this.smallPictureBoxContextMenu.ResumeLayout(false);
            this.textFieldContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.qualityBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.PictureBox largePictureBox;
        private System.Windows.Forms.PictureBox smallPictureBox;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button encryptButton;
        private System.Windows.Forms.Button decryptButton;
        private System.Windows.Forms.RadioButton imageRadioButton;
        private System.Windows.Forms.RadioButton textRadioButton;
        private System.Windows.Forms.Button saveBigImageButton;
        private System.Windows.Forms.Button saveDecryptedButton;
        private System.Windows.Forms.Button loadBigImageButton;
        private System.Windows.Forms.Button loadStuffToEncryptButton;
        private System.Windows.Forms.ToolStripMenuItem hiderImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageToEncryptToolStripMenuItem;
        private System.Windows.Forms.TrackBar qualityBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel helpLinkLabel;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encryptedImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decryptedImageToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip bigPictureBoxContextMenu;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip smallPictureBoxContextMenu;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem2;
        private System.Windows.Forms.ContextMenuStrip textFieldContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
    }



}

