namespace CollageMaker
{
    partial class CollageMaker
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TargetImageFileTextBox = new TextBox();
            TargetImageLabel = new Label();
            SourceFolderLabel = new Label();
            SourceFolderTextBox = new TextBox();
            ChooseTargetImageButton = new Button();
            ChooseSourceFolderButton = new Button();
            ImagesToUseInXAxisTextBox = new TextBox();
            ImageAxisForXLabel = new Label();
            ImageAxisForYLabel = new Label();
            ImagesToUseInYAxisTextBox = new TextBox();
            StartProcessingButton = new Button();
            ImageProgressBar = new ProgressBar();
            ProcessLabel = new Label();
            label1 = new Label();
            OriginalImagePictureBox = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            NewImagePictureBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)OriginalImagePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NewImagePictureBox).BeginInit();
            SuspendLayout();
            // 
            // TargetImageFileTextBox
            // 
            TargetImageFileTextBox.Location = new Point(168, 14);
            TargetImageFileTextBox.Margin = new Padding(4, 5, 4, 5);
            TargetImageFileTextBox.Name = "TargetImageFileTextBox";
            TargetImageFileTextBox.Size = new Size(707, 31);
            TargetImageFileTextBox.TabIndex = 0;
            // 
            // TargetImageLabel
            // 
            TargetImageLabel.AutoSize = true;
            TargetImageLabel.Location = new Point(45, 17);
            TargetImageLabel.Margin = new Padding(4, 0, 4, 0);
            TargetImageLabel.Name = "TargetImageLabel";
            TargetImageLabel.Size = new Size(115, 25);
            TargetImageLabel.TabIndex = 1;
            TargetImageLabel.Text = "Target Image";
            // 
            // SourceFolderLabel
            // 
            SourceFolderLabel.AutoSize = true;
            SourceFolderLabel.Location = new Point(39, 81);
            SourceFolderLabel.Margin = new Padding(4, 0, 4, 0);
            SourceFolderLabel.Name = "SourceFolderLabel";
            SourceFolderLabel.Size = new Size(121, 25);
            SourceFolderLabel.TabIndex = 2;
            SourceFolderLabel.Text = "Source Folder";
            // 
            // SourceFolderTextBox
            // 
            SourceFolderTextBox.Location = new Point(168, 78);
            SourceFolderTextBox.Margin = new Padding(4, 5, 4, 5);
            SourceFolderTextBox.Name = "SourceFolderTextBox";
            SourceFolderTextBox.Size = new Size(707, 31);
            SourceFolderTextBox.TabIndex = 3;
            SourceFolderTextBox.TextChanged += SourceFolderTextBox_TextChanged;
            // 
            // ChooseTargetImageButton
            // 
            ChooseTargetImageButton.Location = new Point(904, 14);
            ChooseTargetImageButton.Margin = new Padding(4, 5, 4, 5);
            ChooseTargetImageButton.Name = "ChooseTargetImageButton";
            ChooseTargetImageButton.Size = new Size(261, 38);
            ChooseTargetImageButton.TabIndex = 4;
            ChooseTargetImageButton.Text = "Choose Target Image";
            ChooseTargetImageButton.TextImageRelation = TextImageRelation.TextAboveImage;
            ChooseTargetImageButton.UseVisualStyleBackColor = true;
            ChooseTargetImageButton.Click += ChooseTargetImageButton_Click;
            // 
            // ChooseSourceFolderButton
            // 
            ChooseSourceFolderButton.Location = new Point(904, 79);
            ChooseSourceFolderButton.Margin = new Padding(4, 5, 4, 5);
            ChooseSourceFolderButton.Name = "ChooseSourceFolderButton";
            ChooseSourceFolderButton.Size = new Size(261, 38);
            ChooseSourceFolderButton.TabIndex = 5;
            ChooseSourceFolderButton.Text = "Choose Source Folder";
            ChooseSourceFolderButton.TextImageRelation = TextImageRelation.TextAboveImage;
            ChooseSourceFolderButton.UseVisualStyleBackColor = true;
            ChooseSourceFolderButton.Click += ChooseSourceFolderButton_Click;
            // 
            // ImagesToUseInXAxisTextBox
            // 
            ImagesToUseInXAxisTextBox.Location = new Point(318, 172);
            ImagesToUseInXAxisTextBox.Margin = new Padding(4, 5, 4, 5);
            ImagesToUseInXAxisTextBox.Name = "ImagesToUseInXAxisTextBox";
            ImagesToUseInXAxisTextBox.Size = new Size(98, 31);
            ImagesToUseInXAxisTextBox.TabIndex = 6;
            ImagesToUseInXAxisTextBox.Text = "20";
            // 
            // ImageAxisForXLabel
            // 
            ImageAxisForXLabel.AutoSize = true;
            ImageAxisForXLabel.Location = new Point(290, 142);
            ImageAxisForXLabel.Margin = new Padding(4, 0, 4, 0);
            ImageAxisForXLabel.Name = "ImageAxisForXLabel";
            ImageAxisForXLabel.Size = new Size(173, 25);
            ImageAxisForXLabel.TabIndex = 7;
            ImageAxisForXLabel.Text = "Image Layout X Axis";
            // 
            // ImageAxisForYLabel
            // 
            ImageAxisForYLabel.AutoSize = true;
            ImageAxisForYLabel.Location = new Point(509, 142);
            ImageAxisForYLabel.Margin = new Padding(4, 0, 4, 0);
            ImageAxisForYLabel.Name = "ImageAxisForYLabel";
            ImageAxisForYLabel.Size = new Size(172, 25);
            ImageAxisForYLabel.TabIndex = 9;
            ImageAxisForYLabel.Text = "Image Layout Y Axis";
            // 
            // ImagesToUseInYAxisTextBox
            // 
            ImagesToUseInYAxisTextBox.Location = new Point(533, 172);
            ImagesToUseInYAxisTextBox.Margin = new Padding(4, 5, 4, 5);
            ImagesToUseInYAxisTextBox.Name = "ImagesToUseInYAxisTextBox";
            ImagesToUseInYAxisTextBox.Size = new Size(107, 31);
            ImagesToUseInYAxisTextBox.TabIndex = 8;
            ImagesToUseInYAxisTextBox.Text = "20";
            // 
            // StartProcessingButton
            // 
            StartProcessingButton.Location = new Point(348, 233);
            StartProcessingButton.Margin = new Padding(4, 5, 4, 5);
            StartProcessingButton.Name = "StartProcessingButton";
            StartProcessingButton.Size = new Size(261, 38);
            StartProcessingButton.TabIndex = 10;
            StartProcessingButton.Text = "Start Processing";
            StartProcessingButton.TextImageRelation = TextImageRelation.TextAboveImage;
            StartProcessingButton.UseVisualStyleBackColor = true;
            StartProcessingButton.Click += StartProcessingButton_Click;
            // 
            // ImageProgressBar
            // 
            ImageProgressBar.Location = new Point(127, 294);
            ImageProgressBar.Name = "ImageProgressBar";
            ImageProgressBar.Size = new Size(852, 34);
            ImageProgressBar.TabIndex = 11;
            // 
            // ProcessLabel
            // 
            ProcessLabel.AutoSize = true;
            ProcessLabel.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ProcessLabel.Location = new Point(136, 335);
            ProcessLabel.Name = "ProcessLabel";
            ProcessLabel.Size = new Size(0, 29);
            ProcessLabel.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(39, 303);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(81, 25);
            label1.TabIndex = 13;
            label1.Text = "Progress";
            // 
            // OriginalImagePictureBox
            // 
            OriginalImagePictureBox.BackgroundImageLayout = ImageLayout.Center;
            OriginalImagePictureBox.Location = new Point(39, 411);
            OriginalImagePictureBox.Name = "OriginalImagePictureBox";
            OriginalImagePictureBox.Size = new Size(342, 271);
            OriginalImagePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            OriginalImagePictureBox.TabIndex = 14;
            OriginalImagePictureBox.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(154, 371);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(129, 25);
            label2.TabIndex = 15;
            label2.Text = "Original Image";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(757, 371);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(102, 25);
            label3.TabIndex = 16;
            label3.Text = "New Image";
            // 
            // NewImagePictureBox
            // 
            NewImagePictureBox.BackgroundImageLayout = ImageLayout.Center;
            NewImagePictureBox.Location = new Point(637, 411);
            NewImagePictureBox.Name = "NewImagePictureBox";
            NewImagePictureBox.Size = new Size(342, 271);
            NewImagePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            NewImagePictureBox.TabIndex = 17;
            NewImagePictureBox.TabStop = false;
            // 
            // CollageMaker
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1178, 744);
            Controls.Add(NewImagePictureBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(OriginalImagePictureBox);
            Controls.Add(label1);
            Controls.Add(ProcessLabel);
            Controls.Add(ImageProgressBar);
            Controls.Add(StartProcessingButton);
            Controls.Add(ImageAxisForYLabel);
            Controls.Add(ImagesToUseInYAxisTextBox);
            Controls.Add(ImageAxisForXLabel);
            Controls.Add(ImagesToUseInXAxisTextBox);
            Controls.Add(ChooseSourceFolderButton);
            Controls.Add(ChooseTargetImageButton);
            Controls.Add(SourceFolderTextBox);
            Controls.Add(SourceFolderLabel);
            Controls.Add(TargetImageLabel);
            Controls.Add(TargetImageFileTextBox);
            Margin = new Padding(4, 5, 4, 5);
            Name = "CollageMaker";
            Text = "Collage Maker";
            Load += CollageMaker_Load;
            ((System.ComponentModel.ISupportInitialize)OriginalImagePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)NewImagePictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox SourceFolderTextBox;
        private Label SourceFolderLabel;
        private Label TargetImageLabel;
        private TextBox TargetImageFileTextBox;
        private Button ChooseSourceFolderButton;
        private Button ChooseTargetImageButton;
        private TextBox ImagesToUseInXAxisTextBox;
        private Label ImageAxisForXLabel;
        private Label ImageAxisForYLabel;
        private TextBox ImagesToUseInYAxisTextBox;
        private Button StartProcessingButton;
        private ProgressBar ImageProgressBar;
        private Label ProcessLabel;
        private Label label1;
        private PictureBox OriginalImagePictureBox;
        private Label label2;
        private Label label3;
        private PictureBox NewImagePictureBox;
    }
}
