using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCharactersApp.Data.DataContext;
using MovieCharactersApp.Data.DTOs.CharacterDTOs;
using MovieCharactersApp.Repositories.InterfaceRepository;
using WebApplication1.Models;
using MovieCharactersApp.Exceptions;

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
            try
            {
                return Ok(_mapper.Map<CharacterReadDto>(await _characterRepository.GetCharacterById(id)));
            }
            catch(CharacterNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
           
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
            try
            {
                await _characterRepository.DeleteCharacter(id);
            }
            catch (CharacterNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
            return NoContent();
        }

        [HttpPut("UpdateCharacter/{id}")]
        public async Task<IActionResult> UpdateCharacter(int id, CharacterUpdateDto characterUpdateDto)
        {
            var character = _mapper.Map<Character>(characterUpdateDto);

            try
            {
                await _characterRepository.UpdateCharacter(character);
            }
            catch (CharacterNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
            return NoContent();
        }

        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }
    }
}
