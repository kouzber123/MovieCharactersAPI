using MovieCharactersApp.Data.DTOs.MoviesDTOs.GetMovieDto;

namespace MovieCharactersAPI.Data.DTOs.MoviesDTOs.UpdateMovie
{
  public class UpdateMovieCharactersDto
  {
    public List<CharacterWithoutMoviesDTO> Characters { get; set; }

  }
}