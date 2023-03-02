using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.CreateMovieDTOs;
using MovieCharactersApp.Data.DataContext;

namespace MovieCharactersAPI.Controllers
{
  public class MovieController : BaseApiController
  {
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;
    public MovieController(IMovieRepository movieRepository, IMapper mapper, DataContext dataContext)
    {
      _dataContext = dataContext;
      _mapper = mapper;
      _movieRepository = movieRepository;
    }

    [HttpGet("Movies")]
    public async Task<ActionResult<List<GetMovieDto>>> GetAll()
    {
      var movies = await _movieRepository.GetMoviesAsync();

      return new OkObjectResult(movies);
    }
    [HttpGet("Movie/{id}")]
    public async Task<ActionResult<GetMovieDto>> GetbyId(int id)
    {
      var movies = await _movieRepository.GetMovieAsync(id);
      // var serialMovie = JsonSerializer.Serialize<MovieDto>(movies);
      return new OkObjectResult(movies);
    }
    [HttpPost("Create")]
    public async Task<ActionResult<GetMovieDto>> AddMovie(CreateMovieDto movieDto)
    {
      var movie = await _movieRepository.CreateMovieAsync(movieDto);
      return new CreatedResult("AddMovie", movie);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await _movieRepository.DeleteMovieAsync(id);
        return new NoContentResult();
      }
      catch (ArgumentException ex)
      {

        return new NotFoundObjectResult(ex.Message);
      }

    }

    [HttpPut("{id}")]

    public async Task<ActionResult> Update(int id, UpdateMovieDto updateMovieDto)
    {
      try
      {
        var res = await _movieRepository.UpdateMovieAsync(id, updateMovieDto);

        return new OkObjectResult(res);
      }
      catch (System.Exception ex)
      {

        return new NotFoundObjectResult(ex.Message);
      }


    }
    // [HttpPatch("{id}")]

    // public async Task<ActionResult> UpdateMovieCharacter(int id, UpdateMovieCharacters updateMovieCharacters)
    // {
    //   try
    //   {
    //     var res = await _movieRepository.UpdateMovieCharacterAsync(id, updateMovieCharacters);

    //     return new OkObjectResult(res);
    //   }
    //   catch (System.Exception ex)
    //   {

    //     return new NotFoundObjectResult(ex.Message);
    //   }
    // }
  }
}