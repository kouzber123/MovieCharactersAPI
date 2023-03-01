using MovieCharactersApp.Data.DTOs.MoviesDTOs.CreateMovieDTOs;
using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs;

namespace MovieCharactersApp
{
  public interface IMovieRepository
  {
    Task<List<GetMovieDto>> GetMoviesAsync();
    Task<GetMovieDto> GetMovieAsync(int id);
    Task<GetMovieDto> CreateMovieAsync(CreateMovieDto MovieDto);
    Task<bool> DeleteMovieAsync(int id);
    Task<IActionResult> UpdateMovieAsync(int id, UpdateMovieDto movie);
  }
}