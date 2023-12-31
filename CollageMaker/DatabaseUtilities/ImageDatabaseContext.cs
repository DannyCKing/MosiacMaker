using CollageMaker.ImageUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CollageMaker.DatabaseUtilities
{
    public class ImageDatabaseContext : DbContext
    {
        private const string DATABASE_FILE_NAME = "ImageDatabase.db";

        private static string CONNECTION_STRING
        {
            get
            {
                string databaseFolder = Path.Combine(FileUtils.AppDataPath, "Databases");
                if(!Directory.Exists(databaseFolder))
                {
                    Directory.CreateDirectory(databaseFolder);
                }

                string databaseFilePath = Path.Combine(databaseFolder, DATABASE_FILE_NAME);

                return $"Data Source={databaseFilePath};";
            }
        }

        public DbSet<SourceImageDetails> SourceImages { get; set; }

        public DbSet<TargetImage> TargetImages { get; set; }

        public DbSet<TargetImage> TargetImageAreaDetails { get; set; }

        public ImageDatabaseContext()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(CONNECTION_STRING);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
