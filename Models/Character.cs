using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Character
    {
        public int Id { get; set; }

        [Required]
        public string Fullname { get; set; }

        [Column(TypeName = "nvarchar(40)")]
        public string? Alias { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? Gender { get; set; }

        public string? Picture { get; set; }

        public ICollection<Movie> Movies { get; set; }

    }
}
