using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageMaker.Models
{
    public class TargetImageProfile
    {
        public Bitmap OriginalTargetImage { get; set; }
        public int X_Count { get; private set; }

        public int Y_Count { get; private set; }
        public Dictionary<Tuple<int,int>, ImageAreaProfile> Areas { get; private set; }

        public TargetImageProfile(int width, int height, Bitmap originalImage)
        {
            OriginalTargetImage = originalImage;
            X_Count = width;
            Y_Count = height;
            Areas = new Dictionary<Tuple<int, int>, ImageAreaProfile>();
        }

        public void SetAreaAverageColor(int x, int y, ImageAreaProfile color)
        {
            Areas[Tuple.Create(x, y)] = color;
        }

        internal ImageAreaProfile GetArea(int i, int j)
        {
            return Areas[Tuple.Create(i, j)];
        }
    }
}
