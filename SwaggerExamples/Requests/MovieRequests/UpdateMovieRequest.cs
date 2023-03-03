using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.UpdateMovie;
using Swashbuckle.AspNetCore.Filters;

namespace MovieCharactersAPI.SwaggerExamples.Requests.MovieRequests
{
  public class UpdateMovieRequest : IExamplesProvider<UpdateMovieDto>
  {
    public UpdateMovieDto GetExamples()
    {
      return new UpdateMovieDto
      {
        Title = "James bond Not bond",
        Genre = "Action comedy",
        ReleaseYear = 2023,
        Director = "James moan",
        PictureUrl = "https://images.immediate.co.uk/production/volatile/sites/3/2021/09/daniel-craig-007.jpg-303a730.png",
        TrailerUrl = "https://www.youtube.com/watch?v=BIhNsAtPbPI",
    
      };
    }
  }
}