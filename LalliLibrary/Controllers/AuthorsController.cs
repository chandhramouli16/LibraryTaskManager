using LalliLibrary.Data;
using LalliLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly LibraryContext _ctx;
    public AuthorsController(LibraryContext ctx) => _ctx = ctx;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _ctx.Authors.Include(a => a.Books).ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var author = await _ctx.Authors.Include(a => a.Books)
                                       .FirstOrDefaultAsync(a => a.Id == id);
        return author is null ? NotFound() : Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Author author)
    {
        _ctx.Authors.Add(author);
        await _ctx.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = author.Id }, author);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Author updated)
    {
        if (id != updated.Id) return BadRequest();
        _ctx.Entry(updated).State = EntityState.Modified;
        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var author = await _ctx.Authors.FindAsync(id);
        if (author is null) return NotFound();
        _ctx.Authors.Remove(author);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }
}
