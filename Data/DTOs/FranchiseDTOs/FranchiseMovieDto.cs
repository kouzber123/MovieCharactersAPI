using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersApp.Data.DTOs.FranchiseDTOs
{


	public class FranchiseMovieDto
	{
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(40)")]
        public string Genre { get; set; }

        [Column(TypeName = "nvarchar(4)")]
        public int? ReleaseYear { get; set; }
        [Column(TypeName = "nvarchar(40)")]
        public string Director { get; set; }

        public string PictureUrl { get; set; }

        public string TrailerUrl { get; set; }

    }
}

