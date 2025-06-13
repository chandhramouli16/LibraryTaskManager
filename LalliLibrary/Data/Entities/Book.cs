using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LalliLibrary.Data.Entities
{
    /// EF Core entity mapping to the Books table
    [Table("Books")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [Required, MaxLength(300)]
        public string Title { get; set; } = null!;

        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }

        // Navigation: each book → one author
        public Author Author { get; set; } = null!;
    }
}
