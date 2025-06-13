using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LalliLibrary.Data.Entities
{
    /// EF Core entity mapping to the Authors table
    [Table("Authors")]
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = null!;

        // Navigation: one author → many books
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
