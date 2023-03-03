using MovieCharactersApp;
using MovieCharactersApp.Data.DTOs.MoviesDTOs.GetMovieDto;
using Swashbuckle.AspNetCore.Filters;

namespace MovieCharactersAPI.SwaggerExamples.Responses
{
  public class GetMoviesRequest : IExamplesProvider<List<GetMovieDto>>
  {
    public List<GetMovieDto> GetExamples()
    {
      return new List<GetMovieDto>
      {
         new GetMovieDto {

         Id = 23,
        Title = "James bond",
        Genre = "Action",
        ReleaseYear = 2023,
        Director = "James moan",
        PictureUrl = "https://images.immediate.co.uk/production/volatile/sites/3/2021/09/daniel-craig-007.jpg-303a730.png",
        TrailerUrl = "https://www.youtube.com/watch?v=BIhNsAtPbPI",
        Characters = new List<CharacterWithoutMoviesDTO> {
            new CharacterWithoutMoviesDTO{
            FullName = "James Bond",
            Gender = "Male",
            PictureUrl = "https://images.immediate.co.uk/production/volatile/sites/3/2021/09/daniel-craig-007.jpg-303a730.png"
                }
        }.ToList(),
        FranchiseId = 15,
        Franchise = new FranchiseWithoutMoviesDTO
        {
          Name = "James bond",
          Description = "Universe where james bond is based"
        }
        },
        new GetMovieDto {

         Id = 24,
        Title = "James bond 2",
        Genre = "Action",
        ReleaseYear = 2023,
        Director = "James moan",
        PictureUrl = "https://images.immediate.co.uk/production/volatile/sites/3/2021/09/daniel-craig-007.jpg-303a730.png",
        TrailerUrl = "https://www.youtube.com/watch?v=BIhNsAtPbPI",
        Characters = new List<CharacterWithoutMoviesDTO> {
            new CharacterWithoutMoviesDTO{
            FullName = "James Bond",
            Gender = "Male",
            PictureUrl = "https://images.immediate.co.uk/production/volatile/sites/3/2021/09/daniel-craig-007.jpg-303a730.png"
                }
        }.ToList(),
        FranchiseId = 15,
        Franchise = new FranchiseWithoutMoviesDTO
        {
          Name = "James bond",
          Description = "Universe where james bond is based"
        }
        }
      };
    }
  }
}