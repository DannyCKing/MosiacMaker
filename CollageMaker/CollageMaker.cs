using CollageMaker.DatabaseUtilities;
using CollageMaker.ImageUtilities;
using CollageMaker.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms.VisualStyles;

namespace CollageMaker
{
    public partial class CollageMaker : Form
    {
        public const int STRIDE = 20;

        private ImageDatabaseConnection DatabaseConnection;

        private ImageSettings Settings;

        Random random = new Random();

        BackgroundWorker imageProcessWorker = new BackgroundWorker();
        public CollageMaker()
        {
            DatabaseConnection = new ImageDatabaseConnection();
            Settings = new ImageSettings();
            Settings.LoadFromFile();
            InitializeComponent();
        }

        private void ChooseTargetImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog chooseTargetFileDialog = new OpenFileDialog();
            chooseTargetFileDialog.Multiselect = false;

            var result = chooseTargetFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string selectedPath = chooseTargetFileDialog.FileName;
                TargetImageFileTextBox.Text = selectedPath;
            }
        }

        private void ChooseSourceFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog chooseSourceDirectoryDialog = new FolderBrowserDialog();

            var result = chooseSourceDirectoryDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string selected = chooseSourceDirectoryDialog.SelectedPath;
                SourceFolderTextBox.Text = selected;
            }
        }

        private void CollageMaker_Load(object sender, EventArgs e)
        {
            TargetImageFileTextBox.Text = Settings.GetStringSetting(ImageSettings.TARGET_IMAGE_SETTING);
            SourceFolderTextBox.Text = Settings.GetStringSetting(ImageSettings.SOURCE_FOLDER_SETTING);
            ImagesToUseInXAxisTextBox.Text = Settings.GetStringSetting(ImageSettings.X_COUNT_SETTING);
            ImagesToUseInYAxisTextBox.Text = Settings.GetStringSetting(ImageSettings.Y_COUNT_SETTING);
        }

        private void StartProcessingButton_Click(object sender, EventArgs e)
        {
            StartProcessingButton.Enabled = false;
            imageProcessWorker = new BackgroundWorker();
            imageProcessWorker.WorkerReportsProgress = true;
            imageProcessWorker.WorkerSupportsCancellation = true;
            imageProcessWorker.DoWork += BackgroundWorker_DoWork;
            imageProcessWorker.RunWorkerCompleted += ImageProcessWorker_RunWorkerCompleted;
            imageProcessWorker.ProgressChanged += ImageProcessWorker_ProgressChanged;
            imageProcessWorker.RunWorkerAsync();
        }

        private void ImageProcessWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            try
            {
                ImageProgressBar.Value = e.ProgressPercentage;
                ImageProcessProgress updateModel = (ImageProcessProgress)e.UserState;
                ProcessLabel.Text = updateModel.Status;
            }
            catch (Exception)
            {

            }
        }

        private void ImageProcessWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            StartProcessingButton.Enabled = true;
        }

        private void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            var areInputsValid = ValidateInputs();

            if (!areInputsValid)
            {
                return;
            }

            var targetImagePath = TargetImageFileTextBox.Text;

            OriginalImagePictureBox.ImageLocation = targetImagePath;

            SaveCurrentUISettingsToFile();

            int xImageCount = int.Parse(ImagesToUseInXAxisTextBox.Text);
            int yImageCount = int.Parse(ImagesToUseInYAxisTextBox.Text);

            SourceFolderProfiles sourceProfiles = new SourceFolderProfiles(SourceFolderTextBox.Text);
            Bitmap b = new Bitmap(targetImagePath);

            int xSamples = b.Width / STRIDE;
            int ySamples = b.Height / STRIDE;

            ImageProcessProgress progressTracker = new ImageProcessProgress(xSamples * ySamples, sourceProfiles.GetFileCount(), xImageCount * yImageCount);

            progressTracker.UpdateStatus(ProgressType.TargetImageInProgress, 0);
            imageProcessWorker.ReportProgress(0, progressTracker);

            ImageUtils imageUtils = new ImageUtils(imageProcessWorker, progressTracker);

            // calculate actions to do for progress bar
            var profileToMatch = imageUtils.GetImageProfileOfTargetImage(targetImagePath, xImageCount, yImageCount, STRIDE, STRIDE);

            progressTracker.UpdateStatus(ProgressType.TargetImageComplete, 0);
            imageProcessWorker.ReportProgress(progressTracker.Progress, progressTracker);

            sourceProfiles.LoadFromFiles(STRIDE, STRIDE, imageUtils, progressTracker, imageProcessWorker).GetAwaiter().GetResult();

            if (sourceProfiles.AvailableProfiles.Count == 0)
            {
                MessageBox.Show("No source files found", "No files found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            progressTracker.UpdateStatus(ProgressType.SourceImagesCompleted, 0);
            imageProcessWorker.ReportProgress(progressTracker.Progress, progressTracker);

            imageUtils.SetProfilesToMatch(profileToMatch, sourceProfiles);

            progressTracker.UpdateStatus(ProgressType.FindingReplacementImagesComplete, 0);
            imageProcessWorker.ReportProgress(progressTracker.Progress, progressTracker);

            var bitmap = new Bitmap(profileToMatch.OriginalTargetImage.Width, profileToMatch.OriginalTargetImage.Height);
            Graphics g = Graphics.FromImage(bitmap);

            // add original image in the background
            //g.DrawImage(profileToMatch.OriginalTargetImage, new Point(0, 0));

            var totalImagesReplaced = 0;

            int currentProgress = progressTracker.Progress;

            for (int x = 0; x < profileToMatch.X_Count; x++)
            {
                for (int y = 0; y < profileToMatch.Y_Count; y++)
                {
                    // resize image
                    var area = profileToMatch.GetArea(x, y);
                    var newImageSizeX = profileToMatch.OriginalTargetImage.Width / xImageCount;
                    var newImageSizeY = profileToMatch.OriginalTargetImage.Height / yImageCount;

                    var imagesAvailableTouse = Math.Min(20, area.MatchingImagesInOrder.Count);
                    var indexToUse = random.Next(imagesAvailableTouse);
                    var imageToUse = area.MatchingImagesInOrder[indexToUse];

                    var smallImage = imageUtils.ResizeImage(imageToUse, new Size(newImageSizeX, newImageSizeY));
                    var xStart = area.X;
                    var yStart = area.Y;
                    g.DrawImage(smallImage, new Point(xStart, yStart));

                    // add cheating color
                    Bitmap Bmp = new Bitmap(smallImage.Width, smallImage.Height);
                    using (Graphics gfx = Graphics.FromImage(Bmp))
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(128, area.AreaColor.R, area.AreaColor.G, area.AreaColor.B)))
                    {
                        gfx.FillRectangle(brush, 0, 0, smallImage.Width, smallImage.Height);
                    }
                    g.DrawImage(Bmp, new Point(xStart, yStart));


                    totalImagesReplaced++;

                    progressTracker.UpdateStatus(ProgressType.ResizeImagesInProgress, totalImagesReplaced);

                    if(currentProgress != progressTracker.Progress)
                    {
                        imageProcessWorker.ReportProgress(progressTracker.Progress, progressTracker);
                        currentProgress = progressTracker.Progress;
                    }
                }
            }

            progressTracker.UpdateStatus(ProgressType.ResizeImagesComplete, 0);
            imageProcessWorker.ReportProgress(progressTracker.Progress, progressTracker);

            var newDirectory = Path.GetDirectoryName(targetImagePath);
            var newfileName = Path.GetFileNameWithoutExtension(targetImagePath) + "_new" + Path.GetExtension(targetImagePath);
            var newFullFilePath = Path.Combine(newDirectory, newfileName);
            bitmap.Save(newFullFilePath);

            progressTracker.UpdateStatus(ProgressType.SavingNewFile, 0);
            imageProcessWorker.ReportProgress(progressTracker.Progress, progressTracker);

            NewImagePictureBox.ImageLocation = newFullFilePath;

            progressTracker.UpdateStatus(ProgressType.Complete, 0);
            imageProcessWorker.ReportProgress(progressTracker.Progress, progressTracker);
        }

        private void SaveCurrentUISettingsToFile()
        {
            // save setings to file
            Settings.Set(ImageSettings.TARGET_IMAGE_SETTING, TargetImageFileTextBox.Text);
            Settings.Set(ImageSettings.SOURCE_FOLDER_SETTING, SourceFolderTextBox.Text);
            Settings.Set(ImageSettings.X_COUNT_SETTING, ImagesToUseInXAxisTextBox.Text);
            Settings.Set(ImageSettings.Y_COUNT_SETTING, ImagesToUseInYAxisTextBox.Text);
        }

        private bool ValidateInputs()
        {
            if (!File.Exists(TargetImageFileTextBox.Text))
            {
                MessageBox.Show("Enter a valid target image");
                return false;
            }

            if (!Directory.Exists(SourceFolderTextBox.Text))
            {
                MessageBox.Show("Enter a valid source folder");
                return false;

            }

            if (string.IsNullOrEmpty(ImagesToUseInXAxisTextBox.Text) || !int.TryParse(ImagesToUseInXAxisTextBox.Text, out int x))
            {
                MessageBox.Show("Enter a how many images you would like the new image to contain in the X-direction");
                return false;
            }

            if (string.IsNullOrEmpty(ImagesToUseInYAxisTextBox.Text) || !int.TryParse(ImagesToUseInYAxisTextBox.Text, out int y))
            {
                MessageBox.Show("Enter a how many images you would like the new image to contain in the Y-direction");
                return false;
            }

            return true;
        }

        private void SourceFolderTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
