using LalliLibrary.Models;
using LalliLibrary.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LalliLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILibraryRepository _repo;
        public BooksController(ILibraryRepository repo) => _repo = repo;

        /// GET /api/books
        [HttpGet]
        public async Task<IEnumerable<BookDto>> GetAll()
            => (await _repo.GetBooksAsync())
                .Select(b => new BookDto(
                    b.BookId,
                    b.Title,
                    b.AuthorId,
                    b.Author.Name
                ));

        /// GET /api/books/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> Get(int id)
        {
            var b = await _repo.GetBookAsync(id);
            return b is null
                ? NotFound()
                : Ok(new BookDto(b.BookId, b.Title, b.AuthorId, b.Author.Name));
        }

        /// POST /api/books
        [HttpPost]
        public async Task<ActionResult<BookDto>> Create(CreateBookDto dto)
        {
            var book = new Data.Entities.Book
            {
                Title = dto.Title,
                AuthorId = dto.AuthorId
            };
            var created = await _repo.CreateBookAsync(book);
            var result = new BookDto(
                created.BookId,
                created.Title,
                created.AuthorId,
                (await _repo.GetAuthorAsync(created.AuthorId))!.Name
            );
            return CreatedAtAction(nameof(Get), new { id = result.BookId }, result);
        }

        /// PUT /api/books/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateBookDto dto)
        {
            var existing = await _repo.GetBookAsync(id);
            if (existing is null) return NotFound();

            existing.Title = dto.Title;
            existing.AuthorId = dto.AuthorId;
            await _repo.UpdateBookAsync(existing);
            return NoContent();
        }

        /// DELETE /api/books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repo.GetBookAsync(id);
            if (existing is null) return NotFound();

            await _repo.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
