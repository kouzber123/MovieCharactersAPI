

using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCharactersApp.Data.DTOs.MoviesDTOs.GetMovieDto
{
  public class CharacterWithoutMoviesDTO
  {
    public int Id { get; set; }

    public string FullName { get; set; }

    [Column(TypeName = "nvarchar(40)")]
    public string Alias { get; set; }

    [Column(TypeName = "nvarchar(20)")]
    public string Gender { get; set; }

    public string PictureUrl { get; set; }
  }
}