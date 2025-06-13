using LalliLibrary.Data;
using LalliLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LalliLibrary.Repositories
{
    /// EF Core–backed implementation of ILibraryRepository
    public class LibraryRepository : ILibraryRepository
    {
        private readonly LibraryContext _ctx;
        public LibraryRepository(LibraryContext ctx) => _ctx = ctx;

        // ---------- Authors ----------
        public async Task<IEnumerable<Author>> GetAuthorsAsync()
            => await _ctx.Authors.AsNoTracking().ToListAsync();

        public async Task<Author?> GetAuthorAsync(int id)
            => await _ctx.Authors
                         .Include(a => a.Books)
                         .AsNoTracking()
                         .FirstOrDefaultAsync(a => a.AuthorId == id);

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            _ctx.Authors.Add(author);
            await _ctx.SaveChangesAsync();
            return author;
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            _ctx.Authors.Update(author);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var a = await _ctx.Authors.FindAsync(id);
            if (a != null)
            {
                _ctx.Authors.Remove(a);
                await _ctx.SaveChangesAsync();
            }
        }

        // ---------- Books ----------
        public async Task<IEnumerable<Book>> GetBooksAsync()
            => await _ctx.Books
                         .Include(b => b.Author)
                         .AsNoTracking()
                         .ToListAsync();

        public async Task<Book?> GetBookAsync(int id)
            => await _ctx.Books
                         .Include(b => b.Author)
                         .AsNoTracking()
                         .FirstOrDefaultAsync(b => b.BookId == id);

        public async Task<Book> CreateBookAsync(Book book)
        {
            _ctx.Books.Add(book);
            await _ctx.SaveChangesAsync();
            return book;
        }

        public async Task UpdateBookAsync(Book book)
        {
            _ctx.Books.Update(book);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var b = await _ctx.Books.FindAsync(id);
            if (b != null)
            {
                _ctx.Books.Remove(b);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}
