using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCharactersApp.Data.DataContext;
using MovieCharactersApp.Data.DTOs.CharacterDTOs;
using MovieCharactersApp.Repositories.InterfaceRepository;
using MovieCharactersApp.Repositories.ConcreteRepository;
using WebApplication1.Models;

namespace MovieCharactersApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;

        public CharactersController(DataContext context, ICharacterRepository characterRepository, IMapper mapper)
        {
            _context = context;
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

        // GET: api/Characters
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CharacterReadDto>>> GetAllCharacters()
        {
            return Ok(_mapper.Map<IEnumerable<CharacterReadDto>>(await _characterRepository.GetAllCharacters()));
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterReadDto>> GetCharacterById(int id)
        {
            return Ok(_mapper.Map<CharacterReadDto>(await _characterRepository.GetCharacterById(id)));
        }

        // POST: api/Characters
        [HttpPost("CreateCharacter")]
        public async Task<ActionResult<Character>> CreateCharacter(CharacterCreateDto characterCreateDto)
        {
            var character = _mapper.Map<Character>(characterCreateDto);
            await _characterRepository.AddCharacter(character);
            return CreatedAtAction(nameof(GetCharacterById), new { id = character.Id }, character);
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            await _characterRepository.DeleteCharacter(id);
            return NoContent();
        }

        [HttpPatch("UpdateCharacter/{id}")]
        public async Task<IActionResult> UpdateCharacter(int id, CharacterUpdateDto characterUpdateDto)
        {
            var character = _mapper.Map<Character>(characterUpdateDto);
            if (id != character.Id)
            {
                return BadRequest();
            }
            await _characterRepository.UpdateCharacter(character);
            return NoContent();
        }
        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }
    }
}
