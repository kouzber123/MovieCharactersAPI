using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieCharactersApp.Data.DataContext;
using MovieCharactersApp.Exceptions;
using MovieCharactersApp.Repositories.InterfaceRepository;
using MovieCharactersApp.Models;

namespace MovieCharactersApp.Repositories.ConcreteRepository
{
  public class CharacterRepository : ICharacterRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CharacterRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Adds a character with the given character object
        public async Task<Character> AddCharacter(Character character)
        {
            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync();
            return character;
        }

        //Deletes a character with id given.
        public async Task DeleteCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                throw new CharacterNotFoundException(id);
            }
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
        }

        //Gets all characters.
        public async Task<IEnumerable<Character>> GetAllCharacters()
        {
            return await _context.Characters.Include(x => x.Movies).ThenInclude(m => m.Franchise).ToListAsync();
        }

        //Get a character by its id.
        public async Task<Character> GetCharacterById(int id)
        {
            var character = await _context.Characters.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);

            if(character == null)
            {
                throw new CharacterNotFoundException(id);
            }
            else
            {
                return character;
            }
        }

        //Updates a character with a given character object.
        public async Task<Character> UpdateCharacter(Character character)
        {
            var oldCharacter = await _context.Characters.AnyAsync(x => x.Id == character.Id);

            if (!oldCharacter)
            {
                throw new CharacterNotFoundException(character.Id);
            }

            _context.Entry(character).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return character;
        }
    }
}
