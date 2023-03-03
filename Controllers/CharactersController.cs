using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCharactersApp.Data.DataContext;
using MovieCharactersApp.Data.DTOs.CharacterDTOs;
using MovieCharactersApp.Repositories.InterfaceRepository;
using MovieCharactersApp.Models;
using MovieCharactersApp.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

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

        /// <summary>
        /// Returns a list of all the characters and their movies
        /// </summary>
        /// <returns>Ok Object</returns>
        /// <response code="200">Query was successful</response>
        /// <response code="400">Bad request</response>
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(List<CharacterReadDto>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<ActionResult<List<CharacterReadDto>>> GetAllCharacters()
        {
            try
            {
                return Ok(_mapper.Map<List<CharacterReadDto>>(await _characterRepository.GetAllCharacters()));
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
           
        }

        /// <summary>
        /// Find a character with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok Object</returns>
        /// <response code="200">Query was successful</response>
        /// <response code="404">Character with that id was not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CharacterReadDto), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
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

        /// <summary>
        /// Create a new character
        /// </summary>
        /// <param name="characterCreateDto"></param>
        /// <returns>CreatedAtAction</returns>
        /// /// <response code="200">Character was created in the database</response>
        /// <response code="400">Bad request</response>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(CharacterReadDto), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<ActionResult<Character>> CreateCharacter(CharacterCreateDto characterCreateDto)
        {
            try
            {
                var character = _mapper.Map<Character>(characterCreateDto);
                await _characterRepository.AddCharacter(character);
                return CreatedAtAction(nameof(GetCharacterById), new { id = character.Id }, character);
            }
            catch(System.Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
          
        }

        /// <summary>
        /// Deletes a character with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NoContent</returns>
        /// <response code="204">Character deleted succesfully</response>
        /// <response code="404">No character found with the id</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
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


        /// <summary>
        /// Update a character.
        /// </summary>
        /// <param name="characterUpdateDto"></param>
        /// <returns>NoContent</returns>
        /// <response code="204">Character updated succesfully</response>
        /// <response code="404">No character found with the id</response>
        [HttpPut("UpdateCharacter")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> UpdateCharacter(CharacterUpdateDto characterUpdateDto)
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
    }
}
