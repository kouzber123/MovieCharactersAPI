using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCharactersAPI.Data.DTOs.MoviesDTOs.GetMovieDto
{
  public class FranchiseWithoutMoviesDTO
  {
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(40)")]
    public string Name { get; set; }

    public string Description { get; set; }
  }
}