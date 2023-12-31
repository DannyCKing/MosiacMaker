using CollageMaker.DatabaseUtilities;
using CollageMaker.ImageUtilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CollageMaker.Models
{
    class SourceFolderProfiles
    {
        private ImageDatabaseConnection ImageDB = new ImageDatabaseConnection();

        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };

        public Dictionary<string, ImageAreaProfile> AvailableProfiles { get; private set; }

        public string SourceFolder { get; private set; }

        public SourceFolderProfiles(string sourceFolder)
        {
            SourceFolder = sourceFolder;
            AvailableProfiles = new Dictionary<string, ImageAreaProfile>();
        }

        public int GetFileCount()
        {
            var sourceFiles = Directory.GetFiles(SourceFolder);
            return sourceFiles.Count(x => ImageExtensions.Contains(Path.GetExtension(x).ToUpperInvariant()));
        }

        public async Task<bool> LoadFromFiles(int xStride, int yStride, ImageUtils imageUtils, ImageProcessProgress progressTracker, System.ComponentModel.BackgroundWorker imageProcessWorker)
        {
            int sourceImagesComplete = 0;
            var sourceFiles = Directory.GetFiles(SourceFolder);

            int currentProgress = progressTracker.Progress;
            foreach(var file in sourceFiles)
            {
                if (ImageExtensions.Contains(Path.GetExtension(file).ToUpperInvariant()))
                {
                    ImageAreaProfile areaProfile = null;

                    //check database first
                    var databaseImageInfo = await ImageDB.GetSourceImageInfo(file);
                    if(databaseImageInfo != null)
                    {
                        areaProfile = new ImageAreaProfile(Color.FromArgb(databaseImageInfo.AverageA, databaseImageInfo.AverageAR, databaseImageInfo.AverageAG, databaseImageInfo.AverageAB), 0, 0);
                    }
                    else
                    {
                        areaProfile = imageUtils.GetImageProfileOfSourceImage(file, xStride, yStride);
                        //ImageDB.AddOrUpdateSourceImageDetail(new SourceImageDetails { })
                    }

                    if (areaProfile != null)
                    {
                        AvailableProfiles.Add(file, areaProfile);
                    }

                    sourceImagesComplete++;
                    progressTracker.UpdateStatus(ProgressType.SourceImagesInProgress, sourceImagesComplete);

                    if (currentProgress != progressTracker.Progress)
                    {
                        imageProcessWorker.ReportProgress(progressTracker.Progress, progressTracker);
                        currentProgress = progressTracker.Progress;
                    }
                }   
            }

            return true;
        }

        public List<string> FindOrderedListOfClosestImages(ImageAreaProfile input)
        {
            double currentDiff = double.MaxValue;
            string returnValue = null;

            List<ImageDiff> diffs = new List<ImageDiff>();
            foreach(var profile in AvailableProfiles)
            {
                var currentProfile = AvailableProfiles[profile.Key];

                var imageDiffVal = input - currentProfile;

                var imageDiff = new ImageDiff { ImageDiffDouble = imageDiffVal, ImageName = profile.Key };
                diffs.Add(imageDiff);
                if(imageDiffVal < currentDiff) 
                {
                    currentDiff = imageDiffVal;
                    returnValue = profile.Key;
                }
            }

            var sortedList = diffs.OrderBy(x => x.ImageDiffDouble);
            var sortedStrings = sortedList.Select(x => x.ImageName).ToList();
            return sortedStrings;
        }
    }

    public class ImageDiff
    {
        public string ImageName { get; set; }
        public double ImageDiffDouble { get; set; }
    }
}
