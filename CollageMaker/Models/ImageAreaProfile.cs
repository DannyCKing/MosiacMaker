using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CollageMaker.Models
{
    public class ImageAreaProfile
    {
        public Color AreaColor { get; private set; }

        public List<string> MatchingImagesInOrder { get; private set; }

        public int X { get; private set; }

        public int Y { get; private set; }

        public ImageAreaProfile(Color color, int x, int y)
        {
            AreaColor = color;
            X = x;
            Y = y;
        }

        public void SetMatchingImage(List<string> matchingImages)
        {
            MatchingImagesInOrder = matchingImages;
        }

        public static double operator -(ImageAreaProfile mine, ImageAreaProfile other)
        {
            var mineValue = (0.11 * mine.AreaColor.B) + (0.59 * mine.AreaColor.G) + (0.30 * mine.AreaColor.R);
            var otherValue = (0.11 * other.AreaColor.B) + (0.59 * other.AreaColor.G) + (0.30 * other.AreaColor.R);
            var totalDiff = (Math.Abs(mineValue - otherValue)) * 100.0 / 255.0;
            return totalDiff;
        }
    }
}
