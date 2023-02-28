using MovieCharactersAPI.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using MovieCharactersAPI.Data.DTOs.FranchiseDTOs;
using AutoMapper;
using MovieCharactersAPI.Exceptions;


namespace MovieCharactersAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FranchiseController : ControllerBase
    {
        private readonly IFranchiseService _franchiseService;
        private readonly IMapper _mapper;

        public FranchiseController(IFranchiseService franchiseService, IMapper mapper)
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


    }

}