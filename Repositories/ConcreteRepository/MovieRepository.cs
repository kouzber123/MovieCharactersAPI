using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.UpdateMovie;
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

    // Updates a movie characters list.
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

    //update movie detail
    public async Task<IActionResult> UpdateMovieAsync(int id, UpdateMovieDto updateMovie)
    {
      var movie = await _dataContext.Movies.FindAsync(id);
      if (movie == null) return new NotFoundResult();

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


    // Asynchronously creates a new movie.
    public async Task<IActionResult> CreateMovieAsync(CreateMovieDto movieDto)
    {
      var existingCharacters = await _dataContext.Characters
      .Where(c => movieDto.Characters.Select(mc => mc.FullName).Contains(c.FullName))
      .ToListAsync();

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

