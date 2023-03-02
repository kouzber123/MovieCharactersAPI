using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.CreateMovieDTOs;
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

    // Updates a movie.
    // public async Task<IActionResult> UpdateMovieCharacterAsync(int id, UpdateMovieCharacters updateMovieCharacters)
    // {
    //   var movie = await _dataContext.Movies.FindAsync(id);

    //   if (movie == null)
    //   {
    //     return new NotFoundResult();
    //   }
    //   _mapper.Map(updateMovieCharacters, movie);

    //   foreach (var character in updateMovieCharacters.Characters)
    //   {
    //     var existingCharacter = await _dataContext.Characters.FindAsync(character.Id);

    //     if (existingCharacter != null)
    //     {
    //       _mapper.Map(character, existingCharacter);
    //       movie.Characters.Add(existingCharacter);
    //     }
    //     else
    //     {
    //       var newCharacter = _mapper.Map(character, existingCharacter);
    //       newCharacter.Id = 0;
    //       _dataContext.Characters.Add(newCharacter);
    //       movie.Characters.Add(newCharacter);
    //     }
    //   }//update
    //   _dataContext.Entry(movie).State = EntityState.Modified;
    //   await _dataContext.SaveChangesAsync();
    //   return new OkObjectResult(_mapper.Map<Movie>(movie));
    // }

    // public async Task<IActionResult> UpdateMovieAsync(int id, UpdateMovieDto updateMovie)
    // {
    //   var movie = await _dataContext.Movies.FindAsync(id);

    //   if (movie == null)
    //   {
    //     return new NotFoundResult();
    //   }
    //   _mapper.Map(updateMovie, movie);
    //   var findId = updateMovie.characters.Select(i => i.Id).ToList();
    //   // _mapper.Map(updateMovie, movie);
    //   // var findId = updateMovie.characterWithoutMoviesDTO.Select(i => i.Id).ToList();
    //   // var characters = await _dataContext.Characters.Where(c => c.Id == findId)

    //   foreach (var character in updateMovie.characters)
    //   {
    //     var foundChar = await _dataContext.Characters.FindAsync(character.Id);

    //     if (foundChar != null)
    //     {
    //       // _mapper.Map(character, foundChar);er;
    //       _mapper.Map(character, foundChar);
    //       movie.Characters.Add(foundChar);
    //     }
    //     else
    //     {
    //       var newCharacter = _mapper.Map<Character>(character);
    //       newCharacter.FullName = character.FullName;
    //       newCharacter.Id = 0;

    //       _dataContext.Characters.Add(newCharacter);
    //       movie.Characters.Add(newCharacter);
    //     }
    //   }

    //   _dataContext.Entry(movie).State = EntityState.Modified;

    //   // _dataContext.Movies.Update(movie);
    //   await _dataContext.SaveChangesAsync();

    //   return new OkObjectResult(_mapper.Map<GetMovieDto>(updateMovie));

    // }

    public async Task<IActionResult> UpdateMovieAsync(int id, UpdateMovieDto updateMovie)
    {

      var movie = await _dataContext.Movies.FindAsync(id);

      if (movie == null)
      {
        return new NotFoundResult();
      }
      _mapper.Map(updateMovie, movie);

      var updatedMovie = _mapper.Map<Movie>(updateMovie); //reset list
      updatedMovie.Characters = new List<Character>();

      foreach (var character in updateMovie.characters)
      {
        var existingCharacter = await _dataContext.Characters.FindAsync(character.Id);

        if (existingCharacter != null)
        {
          _mapper.Map(character, existingCharacter); //update exisitng character
          updatedMovie.Characters.Add(existingCharacter);
        }
        else
        {
          var newCharacter = _mapper.Map<Character>(character);
          newCharacter.Id = 0;
          _dataContext.Characters.Add(newCharacter);
          updatedMovie.Characters.Add(newCharacter);
        }
      }
      // _mapper.Map(updatedMovie, movie);
      movie.Characters = updatedMovie.Characters;
      //update
      // _dataContext.Update(movie);
      _dataContext.Entry(movie).State = EntityState.Modified;
      await _dataContext.SaveChangesAsync();

      return new OkObjectResult(_mapper.Map<GetMovieDto>(movie));
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
    public async Task  DeleteMovieAsync(int id)
    {
      var movie = await _dataContext.Movies.FirstOrDefaultAsync(x => x.Id == id);

      if (movie != null)
      {
        _dataContext.Movies.Remove(movie);
        await _dataContext.SaveChangesAsync();
        return new NotFoundResult();
      }
      return new NoContentResult();
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