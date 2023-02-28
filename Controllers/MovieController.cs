using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.CreateMovieDTOs;

namespace MovieCharactersAPI.Controllers
{
  public class MovieController : BaseApiController
  {
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;
    public MovieController(IMovieRepository movieRepository, IMapper mapper)
    {
      _mapper = mapper;
      _movieRepository = movieRepository;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<List<GetMovieDto>>> GetAll()
    {
      var movies = await _movieRepository.GetMoviesAsync();

      return new OkObjectResult(movies);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<GetMovieDto>> GetbyId(int id)
    {
      var movies = await _movieRepository.GetMovieAsync(id);
      // var serialMovie = JsonSerializer.Serialize<MovieDto>(movies);
      return new OkObjectResult(movies);
    }

    [HttpPost("AddMovie")]

    public async Task<ActionResult<CreateMovieDto>> AddMovie(CreateMovieDto movieDto)
    {
      var movie = await _movieRepository.CreateMovieAsync(movieDto);
      return new CreatedResult("AddMovie", movie);
    }

    [HttpDelete("DeleteMovie/{id}")]
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

    [HttpPatch("UpdateMovie/{id}")]

    public async Task<ActionResult> Update(int id, MovieDto movieDto)
    {

      try
      {
        var res = await _movieRepository.UpdateMovieAsync(id, movieDto);
        return new OkObjectResult(res);
      }
      catch (ArgumentException ex)
      {

        return new NotFoundObjectResult(ex.Message);
      }


    }
  }
}