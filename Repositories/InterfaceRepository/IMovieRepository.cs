using MovieCharactersAPI.Data.DTOs.MoviesDTOs;

namespace MovieCharactersAPI
{
  public interface IMovieRepository
  {
    Task<List<MovieDto>> GetMoviesAsync();
    Task<MovieDto> GetMovieAsync(int id);
    Task<MovieDto> CreateMovieAsync(MovieDto MovieDto);
    Task<bool> DeleteMovieAsync(int id);
    Task<bool> UpdateMovieAsync(int id, MovieDto MovieDto);
  }
}