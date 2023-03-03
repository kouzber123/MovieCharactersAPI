using MovieCharactersApp;
using MovieCharactersApp.Data.DTOs.MoviesDTOs.CreateMovieDTOs;
using MovieCharactersApp.Data.DTOs.MoviesDTOs.GetMovieDto;
using Swashbuckle.AspNetCore.Filters;

namespace MovieCharactersAPI.SwaggerExamples.Requests
{
  public class CreateMovieRequest : IExamplesProvider<CreateMovieDto>
  {
    public CreateMovieDto GetExamples()
    {
      return new CreateMovieDto
      {
        Title = "James bond",
        Genre = "Action",
        ReleaseYear = 2023,
        Director = "James moan",
        PictureUrl = "https://images.immediate.co.uk/production/volatile/sites/3/2021/09/daniel-craig-007.jpg-303a730.png",
        TrailerUrl = "https://www.youtube.com/watch?v=BIhNsAtPbPI",
        Characters = new List<CreateMovieCharacterDto> {
            new CreateMovieCharacterDto{
            FullName = "James Bond",
            Gender = "Male",
            PictureUrl = "https://images.immediate.co.uk/production/volatile/sites/3/2021/09/daniel-craig-007.jpg-303a730.png"
                }
        },
        Franchise = new FranchiseWithoutMoviesDTO
        {
          Name = "James bond",
          Description = "Universe where james bond is based"
        }
      };

    }

  }
}