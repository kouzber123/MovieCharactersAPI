using Microsoft.AspNetCore.Mvc;
using MovieCharactersApp.Models;
using MovieCharactersApp.Data.DTOs.FranchiseDTOs;
using AutoMapper;
using MovieCharactersApp.Exceptions;
using MovieCharactersApp.Repositories.InterfaceRepository;



namespace MovieCharactersApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FranchiseController : ControllerBase
    {
        private readonly IFranchiseRepository _franchiseService;
        private readonly IMapper _mapper;

        public FranchiseController(IFranchiseRepository franchiseService, IMapper mapper)
        {
            _franchiseService= franchiseService;
            _mapper = mapper;
        }
        /// <summary>
        /// Returns list of Franchises
        /// </summary>
        /// <response code="200">Query was successful</response>
        /// <response code="400">Bad request something went wrong</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<FranchiseDto>),200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<ActionResult<IEnumerable<FranchiseDto>>> GetAllFranchises()
        {
            return Ok(_mapper.Map<IEnumerable<FranchiseDto>>(await _franchiseService.GetAllFranchises()));
        }

        /// <summary>
        /// Returns Franchise by id and movies in Franchise
        /// </summary>
        /// <response code="200">Query was successful</response>
        /// <response code="400">Bad request something went wrong</response>
        [HttpGet("{id}")]
         [ProducesResponseType(typeof(FranchiseDto), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 400)]
        public async Task<ActionResult<FranchiseDto>> GetFranchiseById(int id)
        {
            try
            {
                return Ok(_mapper.Map<FranchiseDto>(await _franchiseService.GetFranchiseById(id)));
            }
            catch (FranchiseNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        ///<summary>
        /// Post Franchise by name.
        /// </summary>

        [HttpPost]
        [ProducesResponseType(typeof(CreateFranchiseDto), 200)]
        public async Task<ActionResult<Franchise>> CreateFranchise(CreateFranchiseDto createFranchiseDto)
        {
            var franchise = _mapper.Map<Franchise>(createFranchiseDto);
            await _franchiseService.CreateFranchise(franchise);
            return CreatedAtAction(nameof(GetFranchiseById), new { id = franchise.Id }, franchise);
        }
        ///<summary>
        /// Delete Franchise by ID.
        /// </summary>
        /// <response code="200">Query was successful</response>
        /// <response code="404">Franchise not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            try
            {
                await _franchiseService.DeleteFranchise(id);
            }
            catch (FranchiseNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }

            return NoContent();
        }
        /// <summary>
         /// Update franchise and movies linked to it.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editFranchiseDto"></param>
        /// <response code="200">Query was successful</response>
        /// <response code="404">Incorrect Id</response>
        [HttpPut]
        [ProducesResponseType(typeof(EditFranchiseDto), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404 )]
        public async Task<ActionResult> PutFranchise(int id, EditFranchiseDto editFranchiseDto)
        {
            var franchise = _mapper.Map<EditFranchiseDto>(editFranchiseDto);

            if (id != franchise.Id) return BadRequest();

            try
            {
                await _franchiseService.UpdateFranchise(franchise);
            }
            catch (FranchiseNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }

            return NoContent();



        }
        /// <summary>
        /// Find characters in selected Franchise.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Query was successful</response>
        /// <response code="404">Incorrect Id</response>
        [ProducesResponseType(typeof(FranchiseCharacterDto), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404 )]
        [HttpGet("characters/{id}")]
        public async Task<ActionResult<FranchiseCharacterDto>> GetFranchiseCharacters(int id)
        {
            try
            {
                return Ok(_mapper.Map<FranchiseCharacterDto>(await _franchiseService.CharactersInFranchise(id)));
            }
            catch (FranchiseNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }




    }

}