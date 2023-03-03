using Swashbuckle.AspNetCore.Filters;
using MovieCharactersApp.Data.DTOs.FranchiseDTOs;

namespace MovieCharactersAPI.SwaggerExamples.Requests
{
    public class GetAllFranchisesRequest: IExamplesProvider<FranchiseDto>
    {
        public FranchiseDto GetExamples()
        {
            return new FranchiseDto
            {
                Id = 1,
                Name = "Marvels",
                Description= "Legendary francise makes very woke movies box office is more over 1 dollar, and its multi dollar company"
                
             };
  
        }
    }
}