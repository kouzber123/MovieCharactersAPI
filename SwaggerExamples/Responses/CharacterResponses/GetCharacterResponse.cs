using MovieCharactersApp.Data.DTOs.CharacterDTOs;
using MovieCharactersApp.Data.DTOs.MoviesDTOs.GetMovieDto;
using MovieCharactersApp.Models;
using Swashbuckle.AspNetCore.Filters;

namespace MovieCharactersApp.SwaggerExamples.Responses.CharacterResponses
{
    public class GetCharacterResponse : IExamplesProvider<CharacterReadDto>
    {
        public CharacterReadDto GetExamples()
        {
            return new CharacterReadDto
            { 

                FullName = "James Bond",
                Alias = "007",
                Gender = "Male",
                Picture = "https://images.immediate.co.uk/production/volatile/sites/3/2021/09/daniel-craig-007.jpg-303a730.png",
                Movies = new List<Movie> {
                new Movie
                {
                    Id = 23,
                    Title = "James bond",
                    Genre = "Action",
                    ReleaseYear = 2023,
                    Director = "James moan",
                    PictureUrl = "https://images.immediate.co.uk/production/volatile/sites/3/2021/09/daniel-craig-007.jpg-303a730.png",
                    TrailerUrl = "https://www.youtube.com/watch?v=BIhNsAtPbPI",
                    Characters = new List<Character>
                    {
                        new Character
                        {
                            FullName = "James Bond",
                            Gender = "Male",
                            Picture = "https://images.immediate.co.uk/production/volatile/sites/3/2021/09/daniel-craig-007.jpg-303a730.png"
                        }
                    },
                    FranchiseId = 15,
                    Franchise = new Franchise
                    {
                        Name = "James bond",
                        Description = "Universe where james bond is based"
                    }
                }
                }
            };
        }
    }
}
