using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.UpdateMovie;
using MovieCharactersAPI.Exceptions;
using MovieCharactersApp.Data.DataContext;
using MovieCharactersApp.Data.DTOs.MoviesDTOs.CreateMovieDTOs;

namespace MovieCharactersApp.Controllers
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


    /// <summary>
    /// Returns list of movies with characters and fransise
    /// return bad request if fail
    /// </summary>
    /// <response code="200">Query was successful</response>
    /// <response code="400">Bad request something went wrong</response>
    [HttpGet("List")]
    [ProducesResponseType(typeof(List<GetMovieDto>), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 400)]
    public async Task<ActionResult<List<GetMovieDto>>> GetAll()
    {
      try
      {
        var movies = await _movieRepository.GetMoviesAsync();
        return new OkObjectResult(movies);
      }
      catch (MovieNotFoundException m)
      {
        return new BadRequestObjectResult(new ProblemDetails
        {
          Detail = m.Message
        });
      }
    }

    /// <summary>
    /// Find movie result from given id
    /// throw not found exception
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200">Query was successful</response>
    /// <response code="404">Incorrect Id</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetMovieDto), 200)]
    [ProducesResponseType(typeof(NotFoundResult), 404)]
    public async Task<ActionResult<GetMovieDto>> GetbyId(int id)
    {
      try
      {
        var movies = await _movieRepository.GetMovieAsync(id);
        return new OkObjectResult(movies);
      }
      catch (System.Exception m)
      {
        return new NotFoundObjectResult(m.Message);
      }
    }

    /// <summary>
    /// Create new movie, 
    /// use existing character  and francsise if named
    /// else create new character and fransise to addtion to movie
    /// return bad request if fail 
    /// </summary>
    /// <param name="movieDto"></param>
    /// <response code="201">Creates movie in the database</response>
    /// <response code="400">Bad request when creating movie</response>
    [HttpPost(template: "Create")]
    [ProducesResponseType(typeof(GetMovieDto), 201)]
    [ProducesResponseType(typeof(NotFoundResult), 404)]
    public async Task<ActionResult<GetMovieDto>> AddMovie([FromBody] CreateMovieDto movieDto)
    {
      try
      {
        var movie = await _movieRepository.CreateMovieAsync(movieDto);
        return new CreatedResult("AddMovie", movie);
      }
      catch (MovieNotFoundException m)
      {
        return new BadRequestObjectResult(new ProblemDetails
        {
          Detail = m.Message
        });
      }

    }

    /// <summary>
    /// Takes ID of movie and deletes it,
    ///  throw not found exception
    /// </summary>
    /// <param name="id"></param>
    /// <response code="204">No content result, object deleted succesfully</response>
    /// <response code="404">Not found, id not correct</response>

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(NoContentResult), 204)]
    [ProducesResponseType(typeof(NotFoundResult), statusCode: 404)]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await _movieRepository.DeleteMovieAsync(id);
        return new NoContentResult();
      }
      catch (MovieNotFoundException m)
      {
        return new NotFoundObjectResult(m.Message);
      }

    }

    /// <summary>
    /// Update given movie id content
    /// throw not found exception
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateMovieDto"></param>
    /// <response code="200">Request was succesful</response>
    /// <response code="404">Not found, id not correct</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UpdateMovieDto), 200)]
    [ProducesResponseType(typeof(NotFoundResult), 404)]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateMovieDto updateMovieDto)
    {
      try
      {
        var result = await _movieRepository.UpdateMovieAsync(id, updateMovieDto);

        return new OkObjectResult(result);

      }
      catch (MovieNotFoundException m)
      {

        return new NotFoundObjectResult(new ProblemDetails
        {
          Detail = m.Message
        });
      }
    }

    /// <summary>
    /// Update characters in a movie
    /// if character does not exist create new character and add it
    /// throw not found exception
    /// or add existing character
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateMovieCharacters"></param>
    /// <response code="200">Request was succesful</response>
    /// <response code="404">Not found, id not correct</response>
    [HttpPut("Character/{Id}")]
    [ProducesResponseType(typeof(UpdateMovieCharactersDto), 200)]
    [ProducesResponseType(typeof(NotFoundResult), 404)]
    public async Task<ActionResult> UpdateMovieCharacter(int id, [FromBody] UpdateMovieCharactersDto updateMovieCharacters)
    {
      try
      {
        var res = await _movieRepository.UpdateMovieCharacterAsync(id, updateMovieCharacters);

        return new OkObjectResult(res);
      }
      catch (MovieNotFoundException m)
      {

        return new NotFoundObjectResult(new ProblemDetails
        {
          Detail = m.Message
        });
      }
    }
  }
}