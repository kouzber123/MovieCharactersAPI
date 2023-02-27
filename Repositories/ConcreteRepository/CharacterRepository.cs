using Microsoft.EntityFrameworkCore;
using MovieCharactersApp.Data.DataContext;
using MovieCharactersApp.Data.DTOs.CharacterDTOs;
using MovieCharactersApp.Repositories.InterfaceRepository;
using WebApplication1.Models;

namespace MovieCharactersApp.Repositories.ConcreteRepository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly DataContext _context;

        public CharacterRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Character> AddCharacter(Character character)
        {
            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync();
            return character;
        }

        public async Task DeleteCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Character>> GetAllCharacters()
        {
            return await _context.Characters.Include(x => x.Movies).ToListAsync();
        }

        public async Task<Character> GetCharacterById(int id)
        {
            var character = await _context.Characters.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);

            if(character == null)
            {
                return null;
            }
            else
            {
                return character;
            }
        }

        public Task<Character> UpdateCharacter(Character character)
        {
            throw new NotImplementedException();
        }
    }
}
