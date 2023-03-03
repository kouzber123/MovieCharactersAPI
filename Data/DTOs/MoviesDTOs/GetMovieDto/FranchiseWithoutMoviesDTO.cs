

using System.ComponentModel.DataAnnotations.Schema;
namespace MovieCharactersApp.Data.DTOs.MoviesDTOs.GetMovieDto
{
  public class FranchiseWithoutMoviesDTO
  {
    [Column(TypeName = "nvarchar(40)")]
    public string Name { get; set; }

    public string Description { get; set; }
  }
}