using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageMaker.Models
{
    internal class ImageProcessProgress
    {
        private const int SINGLE_LOCATION_SCANNING_COST = 1;

        private const int SOURCE_IMAGE_COST = 400;

        private const int FINDING_MATCH_COST = 5;

        private const int FINDING_REPLACE_IMAGE_PROCESSING_COST = 10;

        private const int RESIZE_IMAGE_PROCESSING_COST = 100;


        private const int SAVING_FILE_COST = 100;


        public int Progress
        {
            get
            {
                var progress = (1.0 * TotalActionsComplete) / TotalToComplete;
                progress = progress * 100;
                int result = (int)(Math.Floor(progress));
                return result;
            }

        }

        public String Status
        {
            get
            {
                if (CurrentStep == ProgressType.TargetImageInProgress)
                {
                    return "Scanning Target Image";
                }
                else if (CurrentStep == ProgressType.TargetImageComplete)
                {
                    return "Target Image Scanned";
                }
                else if (CurrentStep == ProgressType.SourceImagesInProgress)
                {
                    return $"Source folder being scanned. Item {CurrentStepItemCount} of {SourceImageCount}.";
                }
                else if (CurrentStep == ProgressType.SourceImagesCompleted)
                {
                    return $"Source folder scan complete";
                }
                else if (CurrentStep == ProgressType.FindingReplacementImagesInProgress)
                {
                    return $"Finding replacement images. Searching for replacement image {CurrentStepItemCount} of {ReplacementImageCount}.";
                }
                else if (CurrentStep == ProgressType.FindingReplacementImagesComplete)
                {
                    return $"Finding replacement images complete";
                }
                else if (CurrentStep == ProgressType.ResizeImagesInProgress)
                {
                    return $"Replacing images. Resizing image {CurrentStepItemCount} of {ReplacementImageCount}.";
                }
                else if (CurrentStep == ProgressType.ResizeImagesComplete)
                {
                    return $"Replacing images complete";
                }
                else if (CurrentStep == ProgressType.SavingNewFile)
                {
                    return $"Saving new file";
                }
                else if (CurrentStep == ProgressType.Complete)
                {
                    return $"File complete!";
                }
                else
                {
                    return "Unknown status";
                }
            }
        }

        ProgressType CurrentStep { get; set; }

        private long TotalActionsComplete { get; set; }

        private long TotalToComplete { get; set;}

        private long TargetImageSamples { get; set; }

        private int SourceImageCount;

        private int ReplacementImageCount;

        private int CurrentStepItemCount;

        public ImageProcessProgress(int targetImageSamples, int sourceImageCount, int replacementImageCount)
        {
            long total = 0;

            TargetImageSamples = targetImageSamples;
            // add cost to process target image
            total += targetImageSamples;

            // add cost to process all source images
            total += SOURCE_IMAGE_COST * sourceImageCount;

            // add cost to process all replacement images
            total += FINDING_REPLACE_IMAGE_PROCESSING_COST * replacementImageCount;

            // add cost to resize all images
            total += RESIZE_IMAGE_PROCESSING_COST * replacementImageCount;

            total += SAVING_FILE_COST;

            SourceImageCount = sourceImageCount;
            ReplacementImageCount = replacementImageCount;

            TotalToComplete = total;
        }

        public void UpdateStatus(ProgressType progressType, int currentItemProcessingCount = 0)
        {
            CurrentStep = progressType;
            CurrentStepItemCount = currentItemProcessingCount;
            long currentTotal = 0;

            if (progressType == ProgressType.TargetImageInProgress)
            {
                currentTotal += (long)(SINGLE_LOCATION_SCANNING_COST * currentItemProcessingCount);
            }
            else if (progressType == ProgressType.TargetImageComplete)
            {
                currentTotal += (long)(TargetImageSamples);
            }
            else if (progressType == ProgressType.SourceImagesInProgress)
            {
                currentTotal += (TargetImageSamples);
                currentTotal += (SOURCE_IMAGE_COST * currentItemProcessingCount);
            }
            else if(progressType == ProgressType.SourceImagesCompleted)
            {
                currentTotal += (TargetImageSamples);
                currentTotal += (SOURCE_IMAGE_COST * SourceImageCount);
            }
            else if (progressType == ProgressType.FindingReplacementImagesInProgress)
            {
                currentTotal += (TargetImageSamples);
                currentTotal += (SOURCE_IMAGE_COST * SourceImageCount);
                currentTotal += (FINDING_MATCH_COST * currentItemProcessingCount);
            }
            else if (progressType == ProgressType.FindingReplacementImagesComplete)
            {
                currentTotal += (TargetImageSamples);
                currentTotal += (SOURCE_IMAGE_COST * SourceImageCount);
                currentTotal += (FINDING_MATCH_COST * ReplacementImageCount);
            }
            else if (progressType == ProgressType.ResizeImagesInProgress)
            {
                currentTotal += (TargetImageSamples);
                currentTotal += (SOURCE_IMAGE_COST * SourceImageCount);
                currentTotal += (FINDING_MATCH_COST * ReplacementImageCount);
                currentTotal += (RESIZE_IMAGE_PROCESSING_COST * currentItemProcessingCount);

            }
            else if (progressType == ProgressType.ResizeImagesComplete)
            {
                currentTotal += (TargetImageSamples);
                currentTotal += (SOURCE_IMAGE_COST * SourceImageCount);
                currentTotal += (FINDING_MATCH_COST * ReplacementImageCount);
                currentTotal += (RESIZE_IMAGE_PROCESSING_COST * ReplacementImageCount);
            }
            else if (progressType == ProgressType.SavingNewFile)
            {
                currentTotal += (TargetImageSamples);
                currentTotal += (SOURCE_IMAGE_COST * SourceImageCount);
                currentTotal += (FINDING_MATCH_COST * ReplacementImageCount);
                currentTotal += (RESIZE_IMAGE_PROCESSING_COST * ReplacementImageCount);
            }
            else if (progressType == ProgressType.Complete)
            {
                currentTotal = TotalToComplete;
            }
            else
            {
                throw new InvalidOperationException("Invalid progress state");
            }

            TotalActionsComplete = currentTotal;
        }
    }

    public enum ProgressType
    {
        TargetImageInProgress,
        TargetImageComplete,
        SourceImagesInProgress,
        SourceImagesCompleted,
        FindingReplacementImagesInProgress,
        FindingReplacementImagesComplete,
        ResizeImagesInProgress,
        ResizeImagesComplete,
        SavingNewFile,
        Complete,
    }
}
