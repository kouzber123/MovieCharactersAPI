using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.CreateMovieDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.UpdateMovie;
using MovieCharactersApp.Data.DataContext;
using WebApplication1.Models;

namespace MovieCharactersAPI.Repositories.ConcreteRepository
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

    // Updates a movie characters list.
    public async Task<IActionResult> UpdateMovieCharacterAsync(int id, UpdateMovieCharacters updateMovieCharacters)
    {
      var movie = await _dataContext.Movies.Include(c => c.Characters).FirstOrDefaultAsync(m => m.Id == id);
      if (movie == null)
      {
        return new NotFoundResult();
      }
      foreach (var character in updateMovieCharacters.Characters)
      {
        var existingCharacter = await _dataContext.Characters.FindAsync(character.Id);

        if (existingCharacter != null)
        {
          movie.Characters.Add(existingCharacter);

          if (character.FullName == null)
          {
            _mapper.Map(existingCharacter, existingCharacter);
          }
          else
          {
            _mapper.Map(character, existingCharacter);
          }
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

  //update movie detail
    public async Task<IActionResult> UpdateMovieAsync(int id, UpdateMovieDto updateMovie)
    {
      var movie = await _dataContext.Movies.Include(c => c.Characters).FirstOrDefaultAsync(m => m.Id == id);
      if (movie == null)
      {
        return new NotFoundResult();
      }
      _mapper.Map(updateMovie, movie);
      _dataContext.Entry(movie).State = EntityState.Modified;
      await _dataContext.SaveChangesAsync();
      return new OkObjectResult(_mapper.Map<UpdateMovieDto>(movie));
    }

    // Asynchronously creates a new movie.
    public async Task<IActionResult> CreateMovieAsync(CreateMovieDto movieDto)
    {

      var existingCharacters = await _dataContext.Characters
      .Where(c => movieDto.Characters.Select(mc => mc.FullName).Contains(c.FullName))
      .ToListAsync();

      var newMovie = _mapper.Map<Movie>(movieDto);
      newMovie.Characters = new List<Character>(); // create empty list so we dont dublicate characters

      var existingFranchise = await _dataContext.Franchises.FirstOrDefaultAsync(f => f.Name == movieDto.Franchise.Name);
      //check francshise
      if (existingFranchise != null)
      {
        newMovie.Franchise = existingFranchise;
      }
      else
      {
        newMovie.Franchise = _mapper.Map<Franchise>(movieDto.Franchise);
      }
      //check characters
      foreach (var character in movieDto.Characters)
      {
        var existingCharacter = existingCharacters.FirstOrDefault(c => c.FullName == character.FullName);
        if (existingCharacter != null)
        {
          newMovie.Characters.Add(existingCharacter);
        }
        else
        {
          var newCharacter = _mapper.Map<Character>(character);
          newCharacter.Id = 0;
          _dataContext.Characters.Add(newCharacter);
          newMovie.Characters.Add(newCharacter);
        }

      }
      _dataContext.Movies.Add(newMovie);
      await _dataContext.SaveChangesAsync();

      var newMovieDto = _mapper.Map<GetMovieDto>(newMovie);
      return new CreatedAtRouteResult(nameof(CreateMovieAsync), newMovieDto);
    }

    // Deletes a single movie.
    public async Task DeleteMovieAsync(int id)
    {
      var movie = await _dataContext.Movies.FirstOrDefaultAsync(x => x.Id == id);

      if (movie != null)
      {
        _dataContext.Movies.Remove(movie);
        await _dataContext.SaveChangesAsync();

      }

    }

    // Asynchronously returns a list of movies.
    public async Task<List<GetMovieDto>> GetMoviesAsync()
    {
      var movies = await _dataContext.Movies
      .Include(c => c.Characters)
      .Include(f => f.Franchise)
      .ToListAsync();

      //auto mapper way
      return _mapper.Map<List<GetMovieDto>>(movies);
    }

    // Get a movie.
    public async Task<GetMovieDto> GetMovieAsync(int id)
    {

      var movie = await _dataContext.Movies
      .Include(c => c.Characters)
      .Include(f => f.Franchise)
      .FirstOrDefaultAsync(m => m.Id == id);

      //auto mapper way
      var movieMap = _mapper.Map<GetMovieDto>(movie);
      return movieMap;
    }


  }
}