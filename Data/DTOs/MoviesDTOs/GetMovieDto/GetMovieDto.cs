using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieCharactersAPI.Data.DTOs.CharacterDTOs;
using MovieCharactersAPI.Data.DTOs.FranchiseDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.GetMovieDto;

namespace MovieCharactersAPI
{
  public class GetMovieDto
  {

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

    public List<CharacterWithoutMoviesDTO> Characters { get; set; }

    public int? FranchiseId { get; set; }
    public FranchiseWithoutMoviesDTO Franchise { get; set; }
  }
}