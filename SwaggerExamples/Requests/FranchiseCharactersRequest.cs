using Swashbuckle.AspNetCore.Filters;
using MovieCharactersApp.Data.DTOs.FranchiseDTOs;

namespace MovieCharactersAPI.SwaggerExamples.Requests
{
    public class FranchiseCharactersRequest: IExamplesProvider<FranchiseCharacterDto>
    {
        public FranchiseCharacterDto GetExamples()
        {
            return new FranchiseCharacterDto
            {
                Characters = new List<FranchiseCharacterSingleDto>{
                    new FranchiseCharacterSingleDto{
                        Franchise = "Marvel",
                        Fullname = new List<string>{
                            "names"
                        }
                    }
                }
            };
        }
    }
}