using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageMaker.ImageUtilities
{
    public class FileUtils
    {
        public static string AppDataPath
        {
            get
            {
                var appData =  Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var daniel_king = "daniel_king";
                var collage_maker = "collage_maker";

                var fullPath = Path.Combine(appData, daniel_king, collage_maker);

                return fullPath;
            }
        }
    }
}
