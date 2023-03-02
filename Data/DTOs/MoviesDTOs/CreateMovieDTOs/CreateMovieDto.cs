using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MovieCharactersAPI.Data.DTOs.FranchiseDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.GetMovieDto;

namespace MovieCharactersAPI.Data.DTOs.MoviesDTOs.CreateMovieDTOs
{
  public class CreateMovieDto
  {
    [JsonIgnore]
    public int Id { get; set; }
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