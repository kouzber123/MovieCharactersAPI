using Microsoft.AspNetCore.Mvc;
using MovieCharactersApp.Models;
using MovieCharactersApp.Data.DTOs.FranchiseDTOs;
using AutoMapper;
using MovieCharactersApp.Exceptions;
using MovieCharactersApp.Repositories.InterfaceRepository;



namespace MovieCharactersApp.Controllers
{
    [Route("api/v1/[controller]")]
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseDto>>> GetAllFranchises()
        {
            return Ok(_mapper.Map<IEnumerable<FranchiseDto>>(await _franchiseService.GetAllFranchises()));
        }

        [HttpGet("{id}")]
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

        [HttpPost]
        public async Task<ActionResult<Franchise>> CreateFranchise(CreateFranchiseDto createFranchiseDto)
        {
            var franchise = _mapper.Map<Franchise>(createFranchiseDto);
            await _franchiseService.CreateFranchise(franchise);
            return CreatedAtAction(nameof(GetFranchiseById), new { id = franchise.Id }, franchise);
        }

        [HttpDelete("{id}")]
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

        [HttpPut("{id}")]
        public async Task<ActionResult> PutFranchise(int id, EditFranchiseDto editFranchiseDto)
        {
            var franchise = _mapper.Map<Franchise>(editFranchiseDto);

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



    }

}