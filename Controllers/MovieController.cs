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
    public async Task<ActionResult<IEnumerable<MovieListDto>>> GetAll()
    {
      var movies = await _movieRepository.GetAll();

      return Ok(movies);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetbyId(int id)
    {
      var movies = await _movieRepository.GetById(id);

      return Ok(movies);
    }

    [HttpPost("AddMovie")]

    public async Task<ActionResult<CreateMovieDto>> AddMovie(CreateMovieDto movieDto)
    {
      var movie = await _movieRepository.Add(movieDto);
      return CreatedAtAction(nameof(GetbyId), new { Id = movie.Id }, movie);
    }

    [HttpDelete("DeleteMovie/{id}")]

    public IActionResult Delete(int id)
    {
      _movieRepository.Delete(id);

      return NoContent();
    }

    [HttpPatch("UpdateMovie/{id}")]

    public async Task<ActionResult> Update(int id, UpdateMovieDto movieDto)
    {

      try
      {
        await _movieRepository.Update(id, movieDto);
        return NoContent();
      }
      catch (ArgumentException ex)
      {

        return NotFound(ex.Message);
      }


    }
  }
}