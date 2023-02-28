using MovieCharactersAPI.Data.DTOs.MoviesDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.CreateMovieDTOs;

namespace MovieCharactersAPI
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