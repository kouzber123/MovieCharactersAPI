using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetAll()
    {
      var movies = await _movieRepository.GetAll();

      return movies;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetbyId(int id)
    {
      var movies = await _movieRepository.GetById(id);

      return new OkObjectResult(movies);
    }

    [HttpPost("AddMovie")]

    public async Task<ActionResult<MovieDto>> AddMovie(MovieDto movieDto)
    {
      var movie = await _movieRepository.Add(movieDto);
      return new CreatedResult("AddMovie", movie);
    }

    [HttpDelete("DeleteMovie/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await _movieRepository.Delete(id);
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
        var res = await _movieRepository.Update(id, movieDto);
        return new OkObjectResult(res);
      }
      catch (ArgumentException ex)
      {

        return new NotFoundObjectResult(ex.Message);
      }


    }
  }
}