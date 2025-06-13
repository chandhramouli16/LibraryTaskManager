namespace LalliLibrary.Models
{
    /// Data returned by GET /api/books
    public record BookDto(int BookId, string Title, int AuthorId, string AuthorName);
}
