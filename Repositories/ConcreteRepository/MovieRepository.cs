using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharactersApp.Data.DTOs.MoviesDTOs.CreateMovieDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs;
using MovieCharactersApp.Data.DataContext;
using WebApplication1.Models;

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

    // Updates a movie.
    public async Task<IActionResult> UpdateMovieAsync(int id, UpdateMovieDto updateMovie)
    {
      var movie = await _dataContext.Movies.FindAsync(id);

      if (movie == null)
      {
        return new NotFoundResult();
      }
      _mapper.Map(updateMovie, movie);
      var findId = updateMovie.characterWithoutMoviesDTO.Select(i => i.Id).ToList();
      // var characters = await _dataContext.Characters.Where(c => c.Id == findId)

      foreach (var character in updateMovie.characterWithoutMoviesDTO)
      {
        var foundChar = await _dataContext.Characters.FindAsync(character.Id);

        if (foundChar != null)
        {
          // _mapper.Map(character, foundChar);er;
          _mapper.Map(character, foundChar);
          movie.Characters.Add(foundChar);
        }
        else
        {
          var newCharacter = _mapper.Map(character, foundChar);
          newCharacter.Id = 0;
          _dataContext.Characters.Add(newCharacter);
          movie.Characters.Add(newCharacter);
        }
      }

      _dataContext.Entry(movie).State = EntityState.Modified;

      // _dataContext.Movies.Update(movie);
      await _dataContext.SaveChangesAsync();

      return new OkObjectResult(_mapper.Map<GetMovieDto>(updateMovie));

    }

    public async Task<GetMovieDto> CreateMovieAsync(CreateMovieDto movieDto)
    {
      //exisitng c
      var existingCharacters = await _dataContext.Characters
      .Where(c => movieDto.Characters.Select(mc => mc.FullName).Contains(c.FullName))
      .ToListAsync();

      //search existing f
      var existingFranchise = await _dataContext.Franchises.FirstOrDefaultAsync(f => f.Name == movieDto.Franchise.Name);

      var newMovie = _mapper.Map<Movie>(movieDto);

      //check if character from movie dto exists already by full name
      foreach (var character in movieDto.Characters)
      {
        //now check if singular character exists in db
        var existingCharacter = existingCharacters.FirstOrDefault(c => c.FullName == character.FullName);
        if (existingCharacter != null)
        {
          //existing true
          newMovie.Characters.Add(existingCharacter);
          // newMovie.Characters.Add(existingCharacter);
        }
        if (existingFranchise != null)
        {
          newMovie.Franchise = existingFranchise;
        }
        //existing false
        else
        {
          var characters = _mapper.Map<Character>(movieDto.Characters);

          newMovie.Characters.Add(characters);

          newMovie.Franchise = _mapper.Map<Franchise>(movieDto.Franchise);
        };
      }
      //do the changes here
      _dataContext.Movies.Add(newMovie);
      await _dataContext.SaveChangesAsync();

      //return created movie for user
      var newMovieDto = _mapper.Map<GetMovieDto>(newMovie);
      return newMovieDto;
    }


    // Deletes a single movie.
    public async Task<bool> DeleteMovieAsync(int id)
    {
      var movie = await _dataContext.Movies.FirstOrDefaultAsync(x => x.Id == id);

      if (movie != null)
      {
        _dataContext.Movies.Remove(movie);
        await _dataContext.SaveChangesAsync();
        return true;
      }
      return false;
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