using CollageMaker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageMaker.ImageUtilities
{
    internal class ImageUtils
    {
        private Dictionary<string, Image> _ImageCache = new Dictionary<string, Image>();
        private BackgroundWorker imageProcessWorker;
        private ImageProcessProgress progressTracker;

        public ImageUtils(BackgroundWorker imageProcessWorker, ImageProcessProgress progressTracker)
        {
            this.imageProcessWorker = imageProcessWorker;
            this.progressTracker = progressTracker;
        }

        public TargetImageProfile GetImageProfileOfTargetImage(string targetImagePath, int xCount, int yCount, int xStride, int yStride)
        {
            Bitmap b =  new Bitmap(targetImagePath);

            TargetImageProfile imageProfile = new TargetImageProfile(xCount, yCount, b);

            // start in the first square

            int squareWidth = b.Width / xCount;
            int squareHeight = b.Height / yCount;

            int samplesTaken = 0;
            int currentProgress = 0;

            for (int currentXPiece = 0; currentXPiece < xCount; currentXPiece++) 
            {
                for (int currentYPiece = 0; currentYPiece < yCount; currentYPiece++)
                {
                    int startX = currentXPiece * squareWidth;
                    int endX = startX + squareWidth;

                    int startY = squareHeight * currentYPiece;
                    int endY = startY + squareHeight;

                    Color colorForArea = GetColorForAreaInImage(b, startX, endX, startY, endY, xStride, yStride);
                    imageProfile.SetAreaAverageColor(currentXPiece, currentYPiece, new ImageAreaProfile(colorForArea, startX, startY));
                    samplesTaken++;

                    progressTracker.UpdateStatus(ProgressType.TargetImageInProgress, samplesTaken);
                    if(currentProgress != progressTracker.Progress)
                    {
                        currentProgress = progressTracker.Progress;
                        imageProcessWorker.ReportProgress(progressTracker.Progress, progressTracker);
                    }
                }
            }
            return imageProfile;
        }

        public ImageAreaProfile GetImageProfileOfSourceImage(string sourceImage, int xStride, int yStride)
        {
            try
            {
                using (Bitmap b = new Bitmap(sourceImage))
                {
                    Color colorForArea = GetColorForAreaInImage(b, 0, b.Width, 0, b.Height, xStride, yStride);
                    return new ImageAreaProfile(colorForArea, 0, 0);
                }
            }
            catch(Exception) 
            {
                // not an image
                return null;
            }
        }

        private Color GetColorForAreaInImage(Bitmap b, int startX, int endX, int startY, int endY, int xStride, int yStride)
        {
            int alphaTotal = 0;
            int redTotal = 0;
            int greenTotal = 0;
            int blueTotal = 0;
            int sampleCount = 0;
            for(int i = startX; i < endX; i+= xStride)
            {
                for(int j  = startY; j < endY; j+=yStride)
                {
                    Color pixelColors = b.GetPixel(i, j);
                    alphaTotal += pixelColors.A;
                    redTotal += pixelColors.R;
                    greenTotal += pixelColors.G;
                    blueTotal += pixelColors.B;
                    sampleCount++;
                }
            }

            int returnA = alphaTotal / sampleCount;
            int returnR = redTotal / sampleCount;
            int returnG = greenTotal / sampleCount;
            int returnB = blueTotal / sampleCount;
            return Color.FromArgb(returnA, returnR, returnG, returnB);
        }

        internal void SetProfilesToMatch(TargetImageProfile profileToMatch, SourceFolderProfiles sourceProfiles)
        {
            int replacementsFound = 0;
            int currentProgress = progressTracker.Progress;

            for (int i = 0; i < profileToMatch.X_Count; i++)
            {
                for(int j = 0; j < profileToMatch.Y_Count; j++)
                {
                    var currentArea = profileToMatch.GetArea(i, j);
                    currentArea.SetMatchingImage(sourceProfiles.FindOrderedListOfClosestImages(currentArea));

                    progressTracker.UpdateStatus(ProgressType.FindingReplacementImagesInProgress, replacementsFound);
                    if (currentProgress != progressTracker.Progress)
                    {
                        currentProgress = progressTracker.Progress;
                        imageProcessWorker.ReportProgress(progressTracker.Progress, progressTracker);
                    }
                }
            }
        }

        public Image ResizeImage(string imageFile, Size desiredSize)
        {
            bool useMiddle = true;

            // check cache first for image
            if(_ImageCache.ContainsKey(imageFile))
            {
                return _ImageCache[imageFile];
            }
            else
            {
                using (var imageToResize = Image.FromFile(imageFile))
                {
                    Size sizeToUse = CalculateSizeToUse(imageToResize.Size, desiredSize);
                    var resizedImage = new Bitmap(imageToResize, sizeToUse);
                    
                    // image will be resized to scale, crop off excess
                    int leftMargin = (resizedImage.Width - desiredSize.Width) / 2;
                    int topMargin = (resizedImage.Height - desiredSize.Height) / 2;

                    leftMargin = Math.Max(leftMargin, 0);
                    topMargin = Math.Max(topMargin, 0);

                    Rectangle r = new Rectangle(leftMargin, topMargin, desiredSize.Width, desiredSize.Height);
                    //resizedImage = CropRect(resizedImage, r);
                    Image result = resizedImage;
                    _ImageCache[imageFile] = result;
                    return result;
                }
            }
        }

        public static Bitmap CropRect(Bitmap b, Rectangle r)
        {
            return b.Clone(r, b.PixelFormat);
        }

        private Size CalculateSizeToUse(Size currentImageSize, Size desiredSize)
        {
            var currentRatio = (1.0 * currentImageSize.Width) / currentImageSize.Height;
            var desiredRatio = (1.0 * desiredSize.Width) / desiredSize.Height;

            double ratioToUse = 0;

            if(currentRatio > desiredRatio) 
            {
                // the desired image size is taller than the current image
                // we will resize the image so the height fits first.
                ratioToUse = desiredSize.Height / (currentImageSize.Height * 1.0);
            }
            else if(currentRatio < desiredRatio)
            {
                // the desired image size is wider than the current image
                ratioToUse = desiredSize.Width / (currentImageSize.Width * 1.0) ;
            }
            else
            {
                // ratio matches
                return desiredSize;
            }

            var heightToUse = (int)(currentImageSize.Height * ratioToUse);
            var widthToUse = (int)(currentImageSize.Width * ratioToUse);
            return new Size(widthToUse, heightToUse);
        }
    }
}
