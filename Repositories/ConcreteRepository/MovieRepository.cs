using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs;
using MovieCharactersApp.Data.DataContext;

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


    public async Task<bool> Update(int id, UpdateMovieDto movieDto)
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
    public async Task<CreateMovieDto> Add(CreateMovieDto MovieDto)
    {
      //include 1, 2 check if exists then create instance then add
      // var movie = await _dataContext.Movies.Include(m => m.Characters).Include(f => f.Franchise).ToListAsync();
      var existingCharacters = await _dataContext.Characters
      .Where(c => MovieDto.Characters.Select(mc => mc.FullName).Contains(c.FullName))
      .ToListAsync();

      var existingFranchise = await _dataContext.Franchises.FirstOrDefaultAsync(f => f.Name == MovieDto.Franchise.Name);

      var newMovie = new Movie
      {
        Title = MovieDto.Title,
        Genre = MovieDto.Genre,
        ReleaseYear = MovieDto.ReleaseYear,
        Director = MovieDto.Director,
        TrailerUrl = MovieDto.TrailerUrl,
        PictureUrl = MovieDto.PictureUrl,
        Characters = new List<Character>(),
        Franchise = new Franchise()
      };
      //check if character from movie dto exists already by full name
      foreach (var character in MovieDto.Characters)
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
            PictureUrl = character.PictureUrl,
          });
          newMovie.Franchise = new Franchise
          {
            Id = 0,
            Name = MovieDto.Franchise.Name,
            Description = MovieDto.Franchise.Description
          };
        };
      }
      _dataContext.Movies.Add(newMovie);
      await _dataContext.SaveChangesAsync();

      //return movie
      var movieDto = _mapper.Map<CreateMovieDto>(newMovie);
      return movieDto;
    }

    public void Delete(int id)
    {
      var movie = _dataContext.Movies.FirstOrDefault(x => x.Id == id);

      if (movie != null)
      {
        _dataContext.Movies.Remove(movie);
        _dataContext.SaveChanges();
      }
    }
    public async Task<List<MovieListDto>> GetAll()
    {
      var movies = await _dataContext.Movies
      .Include(c => c.Characters)
      .Include(f => f.Franchise)
      .ToListAsync();

      return movies.Select(m => new MovieListDto
      {
        Id = m.Id,
        Title = m.Title,
        Genre = m.Genre,
        ReleaseYear = m.ReleaseYear,
        Director = m.Director,
        Characters = m.Characters.Select(c => new CharacterNameDto
        {
          Id = c.Id,
          FullName = c.FullName
        }).ToList(),
        Franchise = new FranchiseDto
        {
          Id = m.Franchise.Id,
          Name = m.Franchise.Name
        }

      }).ToList();
    }

    public async Task<MovieDto> GetById(int id)
    {
      var movie = await _dataContext.Movies
      .Include(c => c.Characters)
      .Include(f => f.Franchise)
      .FirstOrDefaultAsync(x => x.Id == id);

      var movieDto = new MovieDto
      {
        Id = movie.Id,
        Title = movie.Title,
        Genre = movie.Genre,
        TrailerUrl = movie.TrailerUrl,
        PictureUrl = movie.PictureUrl,
        Director = movie.Director,
        ReleaseYear = movie.ReleaseYear,
        Characters = movie.Characters.Select(c => new CharacterDto
        {
          FullName = c.FullName
        }).ToArray(),
        Franchise = new FranchiseDto
        {
          Id = movie.Franchise.Id,
          Name = movie.Franchise.Name,
          Description = movie.Franchise.Description
        }
      };

      return movieDto;

    }

    public Task<bool> Update(int id, MovieDto MovieDto)
    {
      throw new NotImplementedException();
    }
  }
}