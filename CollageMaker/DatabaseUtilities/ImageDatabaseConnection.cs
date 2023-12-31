using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageMaker.DatabaseUtilities
{
    public class ImageDatabaseConnection
    {
        private Dictionary<string, SourceImageDetails> _SourceImageCache = null;

        private bool _UseCache = true;
        public ImageDatabaseConnection()
        {
            ImageDatabaseContext dbContent = new ImageDatabaseContext();
        }

        public async Task<List<SourceImageDetails>> GetAllSourceImagesInDatabase()
        {
            using(var dbContext = new ImageDatabaseContext())
            {
                var result = await dbContext.SourceImages.ToListAsync();
                return result;
            }
        }

        public async Task<SourceImageDetails?> GetSourceImageInfo(string sourceImagePath)
        {
            if(_SourceImageCache == null && _UseCache)
            {
                _SourceImageCache= new Dictionary<string, SourceImageDetails>();
                var allSourceImages = await GetAllSourceImagesInDatabase();
                foreach(var item in allSourceImages)
                {
                    _SourceImageCache.Add(item.FullFilePath, item);
                }

                if (_SourceImageCache.ContainsKey(sourceImagePath))
                {
                    return _SourceImageCache[sourceImagePath];
                }
                else
                {
                    // it wasn't loaded into the cache, so it doesn't exist
                    return null;
                }
            }

            using (var dbContext = new ImageDatabaseContext())
            {
                var result = await dbContext.SourceImages.FirstOrDefaultAsync(x => x.FullFilePath == sourceImagePath);   
                return result;
            }
        }

        public async Task<bool> AddOrUpdateSourceImageDetail(SourceImageDetails details)
        {
            try
            {
                if (_SourceImageCache == null && _UseCache)
                {
                    _SourceImageCache = new Dictionary<string, SourceImageDetails>();
                }
                if (_UseCache)
                {
                    if (_SourceImageCache.ContainsKey(details.FullFilePath))
                    {
                        _SourceImageCache[details.FullFilePath] = details;
                    }
                    else
                    {
                        // does not exist, add it
                        _SourceImageCache.Add(details.FullFilePath, details);
                    }
                }

                using (var dbContext = new ImageDatabaseContext())
                {
                    var existingItemInDatabase = await dbContext.SourceImages.FirstOrDefaultAsync(x => x.FullFilePath == details.FullFilePath);

                    if (existingItemInDatabase == null)
                    {
                        var addResult = await dbContext.SourceImages.AddAsync(details);
                        await dbContext.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        existingItemInDatabase.AverageAG = details.AverageAG;
                        existingItemInDatabase.AverageAR = details.AverageAR;
                        existingItemInDatabase.AverageAB = details.AverageAB;
                        existingItemInDatabase.AverageA = details.AverageA;
                        existingItemInDatabase.ImageHash = details.ImageHash;
                        existingItemInDatabase.FileExtension = details.FileExtension;
                        existingItemInDatabase.ImageWidth = details.ImageWidth;
                        existingItemInDatabase.ImageHeight = details.ImageHeight;
                        existingItemInDatabase.StrideX = details.StrideX;
                        existingItemInDatabase.StrideY = details.StrideY;
                        dbContext.Update(existingItemInDatabase);
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            

        }
    }
}
