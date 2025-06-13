using System.ComponentModel.DataAnnotations;

namespace LalliLibrary.Models
{
    /// Data accepted by POST /api/authors
    public class CreateAuthorDto
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = null!;
    }
}
