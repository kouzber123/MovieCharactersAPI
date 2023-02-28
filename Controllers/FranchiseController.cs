using MovieCharactersAPI.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace MovieCharactersAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FranchiseController : ControllerBase
    {
        private readonly IFranchiseService _franchiseService;

        public FranchiseController(IFranchiseService franchiseService) => _franchiseService = franchiseService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Franchise>>> GetAllFranchises()
        {
            return Ok(await _franchiseService.GetAllFranchises());
        }

    }

}