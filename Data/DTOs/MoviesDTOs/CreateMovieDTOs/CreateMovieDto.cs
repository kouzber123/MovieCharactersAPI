using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MovieCharactersApp.Data.DTOs.FranchiseDTOs;
using MovieCharactersApp.Data.DTOs.MoviesDTOs.GetMovieDto;

namespace MovieCharactersApp.Data.DTOs.MoviesDTOs.CreateMovieDTOs
{
  public class CreateMovieDto
  {
    [JsonIgnore]
    public int Id { get; set; }

    public string Title { get; set; }
    public string Genre { get; set; }
    public int ReleaseYear { get; set; }
    public string Director { get; set; }
    public string PictureUrl { get; set; }
    public string TrailerUrl { get; set; }
    public ICollection<CreateMovieCharacterDto> Characters { get; set; }

    public FranchiseWithoutMoviesDTO Franchise { get; set; }
  }
}