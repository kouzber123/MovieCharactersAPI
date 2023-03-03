using MovieCharactersApp;
using Swashbuckle.AspNetCore.Filters;
using MovieCharactersApp.Data.DTOs.FranchiseDTOs;


namespace MovieCharactersAPI.SwaggerExamples.Requests
{
    public class CreateFranchiseRequest: IExamplesProvider<CreateFranchiseDto>
    {
        public CreateFranchiseDto GetExamples()
        {
            return new CreateFranchiseDto
            {
                  Name = "Horror"
            };
        }
    }
}