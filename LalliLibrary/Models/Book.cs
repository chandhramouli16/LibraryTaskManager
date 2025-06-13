using LalliLibrary.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;

    public int AuthorId { get; set; }

    [JsonIgnore]             // ← don’t expect this on input
    [NotMapped]              // ← EF will still map it because of the FK; this just stops accidental binding
    public Author? Author { get; set; }
}
