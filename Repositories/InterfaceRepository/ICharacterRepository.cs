using MovieCharactersApp.Data.DTOs.CharacterDTOs;
using WebApplication1.Models;

namespace MovieCharactersApp.Repositories.InterfaceRepository
{
    public interface ICharacterRepository
    {
        Task<IEnumerable<Character>> GetAllCharacters();
        Task<Character> GetCharacterById(int id);

        Task <Character> AddCharacter(Character character);

        Task DeleteCharacter(int id);

        Task<Character> UpdateCharacter(Character character); 
    }
}
