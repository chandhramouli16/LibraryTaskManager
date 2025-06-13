using LalliLibrary.Models;
using LalliLibrary.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LalliLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly ILibraryRepository _repo;
        public AuthorsController(ILibraryRepository repo) => _repo = repo;

        /// GET /api/authors
        [HttpGet]
        public async Task<IEnumerable<AuthorDto>> GetAll()
            => (await _repo.GetAuthorsAsync())
                .Select(a => new AuthorDto(a.AuthorId, a.Name));

        /// GET /api/authors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> Get(int id)
        {
            var a = await _repo.GetAuthorAsync(id);
            return a is null
                ? NotFound()
                : Ok(new AuthorDto(a.AuthorId, a.Name));
        }

        /// POST /api/authors
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> Create(CreateAuthorDto dto)
        {
            var author = new Data.Entities.Author { Name = dto.Name };
            var created = await _repo.CreateAuthorAsync(author);
            var result = new AuthorDto(created.AuthorId, created.Name);
            return CreatedAtAction(nameof(Get), new { id = result.AuthorId }, result);
        }

        /// PUT /api/authors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateAuthorDto dto)
        {
            var existing = await _repo.GetAuthorAsync(id);
            if (existing is null) return NotFound();

            existing.Name = dto.Name;
            await _repo.UpdateAuthorAsync(existing);
            return NoContent();
        }

        /// DELETE /api/authors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repo.GetAuthorAsync(id);
            if (existing is null) return NotFound();

            await _repo.DeleteAuthorAsync(id);
            return NoContent();
        }
    }
}
