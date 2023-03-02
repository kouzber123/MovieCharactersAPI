using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs;
using MovieCharactersApp.Data.DTOs.MoviesDTOs.CreateMovieDTOs;

namespace MovieCharactersApp
{
  public interface IMovieRepository
  {
    Task<List<GetMovieDto>> GetMoviesAsync();
    Task<GetMovieDto> GetMovieAsync(int id);
    Task<GetMovieDto> CreateMovieAsync(CreateMovieDto MovieDto);
    Task DeleteMovieAsync(int id);
    Task<IActionResult> UpdateMovieAsync(int id, UpdateMovieDto movie);
  }
}