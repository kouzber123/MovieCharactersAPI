using WebApplication1.Models;

namespace MovieCharactersApp.Data.DTOs.CharacterDTOs
{
    public class CharacterReadDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Alias { get; set; }

        public string Gender { get; set; }

        public string PictureUrl { get; set; }

        public List<int> Movies { get; set; }
    }
}
