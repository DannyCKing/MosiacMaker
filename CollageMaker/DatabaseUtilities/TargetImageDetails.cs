using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageMaker.DatabaseUtilities
{
    public class TargetImageAreaDetails
    {
        [Key]
        public Guid TargetImageAreaId { get; set; }
        public TargetImage TargetImage { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public SourceImageDetails SourceImage { get; set; }
        public string ResizedImagePath { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public byte AverageA { get; set; }
        public byte AverageR { get; set; }
        public byte AverageG { get; set; }
        public byte AverageB { get; set; }
    }
}
