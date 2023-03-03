using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MovieCharactersApp
{
  public class CreateMovieCharacterDto
  {
    public string FullName { get; set; }

    [Column(TypeName = "nvarchar(40)")]
    public string Alias { get; set; }

    [Column(TypeName = "nvarchar(20)")]
    public string Gender { get; set; }

    public string PictureUrl { get; set; }
  }
}