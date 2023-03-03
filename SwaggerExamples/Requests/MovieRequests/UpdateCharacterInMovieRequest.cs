using MovieCharactersAPI.Data.DTOs.MoviesDTOs.UpdateMovie;
using MovieCharactersApp.Data.DTOs.CharacterDTOs;
using MovieCharactersApp.Data.DTOs.MoviesDTOs.GetMovieDto;
using Swashbuckle.AspNetCore.Filters;

namespace MovieCharactersAPI.SwaggerExamples.Requests.MovieRequests
{
  public class UpdateCharacterInMovieRequest : IExamplesProvider<UpdateMovieCharactersDto>
  {
    public UpdateMovieCharactersDto GetExamples()
    {
      return new UpdateMovieCharactersDto
      {

        Characters = new List<CharacterWithoutMoviesDTO> {
            new CharacterWithoutMoviesDTO {
                Id = 23,
                FullName = "James with big bonus bonus",
                Gender = "NON BINARY SHE/HER",
                PictureUrl = "Did.COM",
                Alias = "UFO"
            }
         }

      };
    }
  }
}
