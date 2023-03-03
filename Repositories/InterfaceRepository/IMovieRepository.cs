using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.UpdateMovie;
using MovieCharactersApp.Data.DTOs.MoviesDTOs.CreateMovieDTOs;

namespace MovieCharactersApp
{
  public interface IMovieRepository
  {
    Task<List<GetMovieDto>> GetMoviesAsync();
    Task<GetMovieDto> GetMovieAsync(int id);
    Task<IActionResult> CreateMovieAsync(CreateMovieDto MovieDto);
    Task DeleteMovieAsync(int id);
    Task<IActionResult> UpdateMovieAsync(int id, UpdateMovieDto movie);
    Task<IActionResult> UpdateMovieCharacterAsync(int id, UpdateMovieCharactersDto movieCharacters);
  }
}