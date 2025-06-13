using System.ComponentModel.DataAnnotations;

namespace LalliLibrary.Models
{
    /// Data accepted by POST /api/books
    public class CreateBookDto
    {
        [Required, MaxLength(300)]
        public string Title { get; set; } = null!;

        [Required]
        public int AuthorId { get; set; }
    }
}
