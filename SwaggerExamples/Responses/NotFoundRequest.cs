using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace MovieCharactersAPI.SwaggerExamples.Responses
{
  public class NotFoundRequest : IExamplesProvider<NotFoundResult>
  {  /// <summary>
     /// EXAMPLE TEXT FOR SWAGGER
     /// </summary>
     /// <returns></returns>
    public NotFoundResult GetExamples()
    {
      return new NotFoundResult();
    }
  }
}