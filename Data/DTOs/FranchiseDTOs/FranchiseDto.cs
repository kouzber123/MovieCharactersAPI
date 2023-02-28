using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersApp.Data.DTOs.FranchiseDTOs
{
  public class FranchiseDto
  {
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(40)")]
    public string Name { get; set; }

    public string Description { get; set; }

    public List<string> Movies { get; set; }
  }
}