using LalliLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LalliLibrary.Data
{
    /// The EF Core DbContext for Lalli Library.
    /// Defines DbSets for each entity/table.
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> opts)
            : base(opts) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
