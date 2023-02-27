using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(40)")]
        public string? Genre { get; set; }

        [Column(TypeName = "nvarchar(4)")]
        public int? ReleaseYear { get; set; }
        [Column(TypeName = "nvarchar(40)")]
        public string? Director { get; set; }

        public string? Picture { get; set; }

        public string? Trailer { get; set; }

        public ICollection<Character> Characters { get; set; }

        public int? FranchiseId { get; set; }   
        public Franchise Franchise { get; set; }
    }
}
