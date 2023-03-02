using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.CreateMovieDTOs;

namespace MovieCharactersAPI
{
  public interface IMovieRepository
  {
    Task<List<GetMovieDto>> GetMoviesAsync();
    Task<GetMovieDto> GetMovieAsync(int id);
    Task<IActionResult> CreateMovieAsync(CreateMovieDto MovieDto);
    Task DeleteMovieAsync(int id);
    Task<IActionResult> UpdateMovieAsync(int id, UpdateMovieDto movie);
    // Task<IActionResult> UpdateMovieCharacterAsync(int id, UpdateMovieCharacters movieCharacters);
  }
}