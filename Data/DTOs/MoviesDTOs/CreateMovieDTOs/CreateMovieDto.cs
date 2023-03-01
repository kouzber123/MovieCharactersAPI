using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MovieCharactersApp.Data.DTOs.FranchiseDTOs;
using MovieCharactersApp.Data.DTOs.MoviesDTOs.GetMovieDto;

namespace MovieCharactersApp.Data.DTOs.MoviesDTOs.CreateMovieDTOs
{
  public class CreateMovieDto
  {

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Title { get; set; }

    [Column(TypeName = "nvarchar(40)")]
    public string Genre { get; set; }

    [Column(TypeName = "nvarchar(4)")]
    public int? ReleaseYear { get; set; }
    [Column(TypeName = "nvarchar(40)")]
    public string Director { get; set; }
    public string PictureUrl { get; set; }
    public string TrailerUrl { get; set; }


    public List<CreateMovieCharacterDto> Characters { get; set; } = new();
    public int FranchiseId { get; set; }
    public FranchiseWithoutMoviesDTO Franchise { get; set; } = new();
  }
}