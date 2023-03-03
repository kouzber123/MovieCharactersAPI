using MovieCharactersApp.Data.DTOs.CharacterDTOs;
using MovieCharactersApp.Data.DTOs.MoviesDTOs.CreateMovieDTOs;
using Swashbuckle.AspNetCore.Filters;

namespace MovieCharactersApp.SwaggerExamples.Requests.CharacterRequest
{
    public class CreateCharacterRequest : IExamplesProvider<CharacterCreateDto>
    {
        /// <summary>
        /// EXAMPLE TEXT FOR SWAGGER
        /// </summary>
        /// <returns></returns>
        public CharacterCreateDto GetExamples()
        {
            return new CharacterCreateDto
            {

                FullName = "James Bond",
                Alias= "007",
                Gender = "Male",
                Picture = "https://images.immediate.co.uk/production/volatile/sites/3/2021/09/daniel-craig-007.jpg-303a730.png"
            };
        }

    }
}
