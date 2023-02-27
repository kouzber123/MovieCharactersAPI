using MovieCharactersAPI.Data.DTOs.MoviesDTOs;

namespace MovieCharactersAPI
{
  public interface IMovieRepository
  {
    Task<List<MovieListDto>> GetAll();
    Task<MovieDto> GetById(int id);
    Task<CreateMovieDto> Add(CreateMovieDto MovieDto);
    void Delete(int id);
    Task<bool> Update(int id, UpdateMovieDto MovieDto);
  }
}