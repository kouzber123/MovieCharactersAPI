using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Data.DTOs.CharacterDTOs;
using MovieCharactersAPI.Data.DTOs.FranchiseDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.GetMovieDto;
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
    public async Task<bool> UpdateMovieAsync(int id, MovieDto movieDto)
    {
      var movie = await _dataContext.Movies.Include(c => c.Characters).Include(f => f.Franchise).FirstOrDefaultAsync(m => m.Id == id);

      var character = await _dataContext.Characters.FirstOrDefaultAsync(x => x.Id == id);

      if (character == null)
      {
        throw new ArgumentNullException("ID was not found");
      }

      _mapper.Map(movieDto, movie);
      _dataContext.Entry(character).State = EntityState.Modified;
      await _dataContext.SaveChangesAsync();

      return true;

    }
    public async Task<MovieDto> CreateMovieAsync(MovieDto movieDto)
    {
      //include 1, 2 check if exists then create instance then add
      // var movie = await _dataContext.Movies.Include(m => m.Characters).Include(f => f.Franchise).ToListAsync();
      var existingCharacters = await _dataContext.Characters
      .Where(c => movieDto.Characters.Select(mc => mc.FullName).Contains(c.FullName))
      .ToListAsync();

      var existingFranchise = await _dataContext.Franchises.FirstOrDefaultAsync(f => f.Name == movieDto.Franchise.Name);

      // _mapper.Map<MovieDto>(movieDto);
      var newMovie = new Movie
      {
        Title = movieDto.Title,
        Genre = movieDto.Genre,
        ReleaseYear = movieDto.ReleaseYear,
        Director = movieDto.Director,
        TrailerUrl = movieDto.TrailerUrl,
        PictureUrl = movieDto.PictureUrl,
        Characters = new List<Character>(),
        Franchise = new Franchise()
      };
      //check if character from movie dto exists already by full name
      foreach (var character in movieDto.Characters)
      {
        //now check if singular character exists in db
        var existingCharacter = existingCharacters.FirstOrDefault(c => c.FullName == character.FullName);
        if (existingCharacter != null)
        {
          newMovie.Characters.Add(existingCharacter);
        }
        if (existingFranchise != null)
        {
          newMovie.Franchise = existingFranchise;
        }
        //else we create a new characters and fransisez
        else
        {
          newMovie.Characters.Add(new Character
          {
            Id = 0,
            FullName = character.FullName,
            Alias = character.Alias,
            Gender = character.Gender,
            Picture = character.PictureUrl,
          });
          newMovie.Franchise = new Franchise
          {
            Id = 0,
            Name = movieDto.Franchise.Name,
            Description = movieDto.Franchise.Description
          };
        };
      }
      _dataContext.Movies.Add(newMovie);
      await _dataContext.SaveChangesAsync();

      //return movie
      var newMovieDto = _mapper.Map<MovieDto>(newMovie);
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
    public async Task<List<MovieDto>> GetMoviesAsync()
    {
      var movies = await _dataContext.Movies
      .Include(c => c.Characters)
      .Include(f => f.Franchise)
      .ToListAsync();

      //auto mapper way
      var movieMap = _mapper.Map<List<MovieDto>>(movies);
      return movieMap;

  //-----------manual way
      // return movies.Select(m => new MovieDto
      // {
      //   Id = m.Id,
      //   Title = m.Title,
      //   Genre = m.Genre,
      //   ReleaseYear = m.ReleaseYear,
      //   Director = m.Director,
      //   Characters = m.Characters.Select(c => new CharacterWithoutMoviesDTO
      //   {
      //     Id = c.Id,
      //     FullName = c.FullName
      //   }).ToArray(),
      //   Franchise = new FranchiseWithoutMoviesDTO
      //   {
      //     Id = m.Franchise.Id,
      //     Name = m.Franchise.Name
      //   }

      // }).ToList();
    }

    // Get a movie.
    public async Task<MovieDto> GetMovieAsync(int id)
    {

      var movie = await _dataContext.Movies
      .Include(c => c.Characters)
      .Include(f => f.Franchise)
      .FirstOrDefaultAsync(m => m.Id == id);

      //auto mapper way
      var movieMap = _mapper.Map<MovieDto>(movie);
      return movieMap;

      //..........manual way
      // var movieDto = new MovieDto
      // {
      //   Id = movie.Id,
      //   Title = movie.Title,
      //   Genre = movie.Genre,
      //   TrailerUrl = movie.TrailerUrl,
      //   PictureUrl = movie.PictureUrl,
      //   Director = movie.Director,
      //   ReleaseYear = movie.ReleaseYear,
      //   Characters = movie.Characters.Select(c => new CharacterDto
      //   {
      //     FullName = c.FullName
      //   }).ToList(),
      //   Franchise = new FranchiseDto
      //   {
      //     Id = movie.Franchise.Id,
      //     Name = movie.Franchise.Name,
      //     Description = movie.Franchise.Description
      //   }
      // };


      // return movieDto;
    }

  }
}