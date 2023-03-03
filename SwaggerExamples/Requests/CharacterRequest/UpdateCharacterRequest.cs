using MovieCharactersApp.Data.DTOs.CharacterDTOs;
using Swashbuckle.AspNetCore.Filters;

namespace MovieCharactersApp.SwaggerExamples.Requests.CharacterRequest
{
    public class UpdateCharacterRequest : IExamplesProvider<CharacterUpdateDto>
    {
        /// <summary>
        /// example text for swagger
        /// </summary>
        /// <returns></returns>
        public CharacterUpdateDto GetExamples()
        {
            return new CharacterUpdateDto
            {
                id = 0,
                FullName = "James Bond",
                Alias = "007",
                Gender = "Male",
                Picture = "https://images.immediate.co.uk/production/volatile/sites/3/2021/09/daniel-craig-007.jpg-303a730.png"
            };
        }
    }
}
