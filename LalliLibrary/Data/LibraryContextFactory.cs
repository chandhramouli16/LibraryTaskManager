using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LalliLibrary.Data
{
    public class LibraryContextFactory
        : IDesignTimeDbContextFactory<LibraryContext>
    {
        public LibraryContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            var cnn = config.GetConnectionString("LibraryConnectionString");
            optionsBuilder.UseSqlServer(cnn);

            return new LibraryContext(optionsBuilder.Options);
        }
    }
}
