

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MovieCharactersApp.Data.DTOs.MoviesDTOs.GetMovieDto
{
  public class FranchiseWithoutMoviesDTO
  {
    [JsonIgnore]
    public int Id { get; set; }
    [Column(TypeName = "nvarchar(40)")]
    public string Name { get; set; }

    public string Description { get; set; }
  }
}