using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageMaker.DatabaseUtilities
{
    public class TargetImage
    {
        [Key]
        public Guid TargetImageId { get; set; }

        public string TargetImagePath { get; set; }

        public int XAxisImageCount { get; set; }

        public int YAxisImageCount { get; set; }

        public List<TargetImageAreaDetails> AreaDetails { get; set; }


    }
}
