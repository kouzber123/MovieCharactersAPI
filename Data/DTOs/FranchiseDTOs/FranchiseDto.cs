using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs;

namespace MovieCharactersAPI.Data.DTOs.FranchiseDTOs
{
  public class FranchiseDto
  {
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(40)")]
    public string Name { get; set; }

    public string Description { get; set; }

     public MovieDto[] Movies { get; set; }
  }
}