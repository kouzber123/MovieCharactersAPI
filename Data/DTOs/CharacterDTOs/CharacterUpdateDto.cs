using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCharactersApp.Data.DTOs.CharacterDTOs
{
    public class CharacterUpdateDto
    {
        [Required]
        public string FullName { get; set; }

        [Column(TypeName = "nvarchar(40)")]
        public string Alias { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Gender { get; set; }

        public string Picture { get; set; }

    }
}
