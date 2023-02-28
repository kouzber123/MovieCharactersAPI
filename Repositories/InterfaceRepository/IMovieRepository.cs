using MovieCharactersApp.Data.DTOs.MoviesDTOs;
using MovieCharactersApp.Data.DTOs.MoviesDTOs.CreateMovieDTOs;

namespace MovieCharactersApp
{
  public interface IMovieRepository
  {
    Task<List<GetMovieDto>> GetMoviesAsync();
    Task<GetMovieDto> GetMovieAsync(int id);
    Task<CreateMovieDto> CreateMovieAsync(CreateMovieDto MovieDto);
    Task<bool> DeleteMovieAsync(int id);
    Task<bool> UpdateMovieAsync(int id, MovieDto MovieDto);
  }
}