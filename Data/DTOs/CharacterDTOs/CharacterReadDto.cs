using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieCharactersApp.Models;

namespace MovieCharactersApp.Data.DTOs.CharacterDTOs
{
  public class CharacterReadDto
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Column(TypeName = "nvarchar(40)")]
        public string Alias { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Gender { get; set; }

        public string Picture { get; set; }
        public List<GetMovieDto> Movies { get; set; }
    }
}
