using System.Text.Json.Serialization;

namespace MovieCharactersApp
{
  public class CreateMovieCharacterDto
  {
    public string FullName { get; set; }
    public string Alias { get; set; }
    public string Gender { get; set; }
    public string PictureUrl { get; set; }
  }
}