using Swashbuckle.AspNetCore.Filters;
using MovieCharactersApp.Data.DTOs.FranchiseDTOs;

namespace MovieCharactersAPI.SwaggerExamples.Requests
{
    public class EditFranchiseRequest: IExamplesProvider<EditFranchiseDto>
    {
        /// <summary>
        /// EXAMPLE TEXT FOR SWAGGER 
        /// </summary>
        /// <returns></returns>
        public EditFranchiseDto GetExamples()
        {
            return new EditFranchiseDto
            {
                Id = 1,
                Name = "Marvel",
                Description = "Legendary thing",
                Movies = new List<FranchiseMovieDto> {
                    new FranchiseMovieDto{
                        Id =1,
                        Title = "New Movie",
                        Genre = "Horror",
                        ReleaseYear = 2023,
                        Director = "MR Director",
                        PictureUrl = "",
                        TrailerUrl = ""
                    }

                }

            };
        }
    }
}