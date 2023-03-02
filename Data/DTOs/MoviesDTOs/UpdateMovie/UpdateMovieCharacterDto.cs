using MovieCharactersAPI.Data.DTOs.MoviesDTOs.GetMovieDto;

namespace MovieCharactersAPI.Data.DTOs.MoviesDTOs.UpdateMovie
{
  public class UpdateMovieCharacters
  {
    public List <CharacterWithoutMoviesDTO> Characters { get; set; }

  }
}