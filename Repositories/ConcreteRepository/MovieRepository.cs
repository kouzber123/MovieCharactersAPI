using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.UpdateMovie;
using MovieCharactersAPI.Exceptions;
using MovieCharactersApp.Data.DataContext;
using MovieCharactersApp.Data.DTOs.CharacterDTOs;
using MovieCharactersApp.Data.DTOs.MoviesDTOs.CreateMovieDTOs;
using MovieCharactersApp.Models;

namespace MovieCharactersApp.Repositories.ConcreteRepository
{
  public class MovieRepository : IMovieRepository
  {
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;
    public MovieRepository(DataContext dataContext, IMapper mapper)
    {
      _mapper = mapper;
      _dataContext = dataContext;
    }

    /// <summary>
    /// Update movie characters, all else excluded
    /// custom throw not found exception 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateMovieCharacters"></param>
    /// <returns></returns>
    public async Task<IActionResult> UpdateMovieCharacterAsync(int id, UpdateMovieCharactersDto updateMovieCharacters)
    {
      var movie = await _dataContext.Movies.Include(c => c.Characters).FirstOrDefaultAsync(m => m.Id == id);

      if (movie == null) return new NotFoundResult();

      foreach (var character in updateMovieCharacters.Characters)
      {
        var existingCharacter = await _dataContext.Characters.FindAsync(character.Id);

        if (existingCharacter != null)
        {
          movie.Characters.Add(existingCharacter);
          var updateDetails = new CharacterDto
          {
            Id = existingCharacter.Id,
            FullName = character.FullName != null ? character.FullName : existingCharacter.FullName,
            Alias = character.Alias != null ? character.Alias : existingCharacter.Alias,
            Gender = character.Gender != null ? character.Gender : existingCharacter.Gender,
            PictureUrl = character.PictureUrl != null ? character.PictureUrl : existingCharacter.Picture
          };
          _mapper.Map(updateDetails, existingCharacter);
        }
        else
        {
          movie.Characters.Add(_mapper.Map<Character>(character));
        }
      }
      _dataContext.Entry(movie).State = EntityState.Modified;
      await _dataContext.SaveChangesAsync();
      return new OkObjectResult(_mapper.Map<GetMovieDto>(movie));
    }


    /// <summary>
    /// Update movie and its content, characters and fransise excluded
    ///  custom throw not found exception 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateMovie"></param>
    /// <returns></returns>
    public async Task<IActionResult> UpdateMovieAsync(int id, UpdateMovieDto updateMovie)
    {
      var movie = await _dataContext.Movies.FindAsync(id);
      if (movie == null) throw new MovieNotFoundException(id);

      var upateDetails = new UpdateMovieDto
      {
        Title = updateMovie.Title != null ? updateMovie.Title : movie.Title,
        Genre = updateMovie.Genre != null ? updateMovie.Genre : movie.Genre,
        ReleaseYear = updateMovie.ReleaseYear != null ? updateMovie.ReleaseYear : movie.ReleaseYear,
        Director = updateMovie.Director != null ? updateMovie.Director : movie.Director,
        PictureUrl = updateMovie.PictureUrl != null ? updateMovie.PictureUrl : movie.PictureUrl,
        TrailerUrl = updateMovie.TrailerUrl != null ? updateMovie.TrailerUrl : movie.TrailerUrl,
      };
      _mapper.Map(upateDetails, movie);
      _dataContext.Entry(movie).State = EntityState.Modified;
      await _dataContext.SaveChangesAsync();
      return new OkObjectResult(_mapper.Map<UpdateMovieDto>(movie));
    }



    /// <summary>
    /// Create a movie, add existing characters and fransise if any
    /// else create them
    /// custom throw not found exception 
    /// </summary>
    /// <param name="movieDto"></param>
    /// <returns></returns>
    public async Task<IActionResult> CreateMovieAsync(CreateMovieDto movieDto)
    {
      var existingCharacters = await _dataContext.Characters
      .Where(c => movieDto.Characters.Select(mc => mc.FullName).Contains(c.FullName))
      .ToListAsync();

      if (existingCharacters == null) throw new MovieNotFoundException("Create");

      var newMovie = _mapper.Map<Movie>(movieDto);
      newMovie.Characters = new List<Character>(); // create empty list so we dont dublicate characters

      var existingFranchise = await _dataContext.Franchises.FirstOrDefaultAsync(f => f.Name == movieDto.Franchise.Name);
      newMovie.Franchise = existingFranchise != null ? existingFranchise : _mapper.Map<Franchise>(movieDto.Franchise);

      foreach (var character in movieDto.Characters)
      {
        var existingCharacter = existingCharacters.FirstOrDefault(c => c.FullName == character.FullName);
        newMovie.Characters.Add(existingCharacter != null ? existingCharacter : _mapper.Map<Character>(character));
      }
      _dataContext.Movies.Add(newMovie);
      await _dataContext.SaveChangesAsync();

      var newMovieDto = _mapper.Map<GetMovieDto>(newMovie);
      return new CreatedAtRouteResult(nameof(CreateMovieAsync), newMovieDto);
    }

    /// <summary>
    /// Delete movie by id
    /// custom throw not found exception 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>no content</returns>
    public async Task DeleteMovieAsync(int id)
    {
      var movie = await _dataContext.Movies.FirstOrDefaultAsync(x => x.Id == id);

      if (movie != null)
      {
        _dataContext.Movies.Remove(movie);
        await _dataContext.SaveChangesAsync();
      }
      else
      {
        throw new MovieNotFoundException(id);
      }
    }
    /// <summary>
    /// Get movies and its characters
    /// custom throw not found exception 
    /// </summary>
    /// <returns></returns>
    public async Task<List<GetMovieDto>> GetMoviesAsync()
    {
      var movies = await _dataContext.Movies
      .Include(c => c.Characters)
      .Include(f => f.Franchise)
      .ToListAsync();

      if (movies == null) throw new MovieNotFoundException("GET");

      return _mapper.Map<List<GetMovieDto>>(movies);
    }

    /// <summary>
    /// Get a movie by id and return its details
    /// custom throw not found exception 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<GetMovieDto> GetMovieAsync(int id)
    {

      var movie = await _dataContext.Movies
      .Include(c => c.Characters)
      .Include(f => f.Franchise)
      .FirstOrDefaultAsync(m => m.Id == id);

      if (movie == null) throw new MovieNotFoundException(id);

      var movieMap = _mapper.Map<GetMovieDto>(movie);
      return movieMap;
    }


  }
}

