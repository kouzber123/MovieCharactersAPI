using MovieCharactersAPI.Data.DTOs.MoviesDTOs;

namespace MovieCharactersAPI
{
  public interface IMovieRepository
  {
    Task<List<MovieDto>> GetAll();
    Task<MovieDto> GetById(int id);
    Task<MovieDto> Add(MovieDto MovieDto);
    Task<bool> Delete(int id);
    Task<bool> Update(int id, MovieDto MovieDto);
  }
}