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

        /// <summary>
        /// Adds character 
        /// </summary>
        /// <param name="character"></param>
        /// <returns>Character</returns>
        public async Task<Character> AddCharacter(Character character)
        {
            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync();
            return character;
        }

        /// <summary>
        /// Deletes a character with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>no content</returns>
        /// <exception cref="CharacterNotFoundException"></exception>
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

        /// <summary>
        /// Get's all characters 
        /// </summary>
        /// <returns>character list</returns>
        public async Task<IEnumerable<Character>> GetAllCharacters()
        {
            return await _context.Characters.Include(x => x.Movies).ThenInclude(m => m.Franchise).ToListAsync();
        }

        /// <summary>
        /// Get character by it's given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>character</returns>
        /// <exception cref="CharacterNotFoundException"></exception>
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

        /// <summary>
        /// Updates character
        /// </summary>
        /// <param name="character"></param>
        /// <returns>character</returns>
        /// <exception cref="CharacterNotFoundException"></exception>
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
