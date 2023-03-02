using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.GetMovieDto;

namespace MovieCharactersAPI.Data.DTOs.MoviesDTOs.UpdateMovie
{
  public class UpdateMovieDto
  {

    [JsonIgnore]
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

    // public List<CharacterWithoutMoviesDTO> Characters { get; set; }
  }
}