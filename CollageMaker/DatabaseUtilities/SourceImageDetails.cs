using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CollageMaker.DatabaseUtilities
{
    [Index(nameof(FullFilePath))]
    public class SourceImageDetails
    {
        [Key]
        public Guid SourceImageId { get; set; }

        public Guid ImageHash { get; set; }
        public string FullFilePath { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public byte AverageA { get; set; }
        public byte AverageAR { get; set; }
        public byte AverageAG { get; set; }
        public byte AverageAB { get; set; }

        public byte StrideX { get; set; }

        public byte StrideY { get; set; }

        public SourceImageDetails()
        {
            SourceImageId = Guid.NewGuid();
        }
    }
}
