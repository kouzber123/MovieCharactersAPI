using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Swashbuckle.AspNetCore.Filters;

namespace MovieCharactersAPI.SwaggerExamples.Requests.MovieRequests
{
  /// <summary>
  /// EXAMPLE TEXT FOR SWAGGER
  /// </summary>
  /// <returns></returns>
  public class DeleteMovieRequest : IExamplesProvider<NoContentResult>
  {
    public NoContentResult GetExamples()
    {
      return new NoContentResult();
    }
  }
}