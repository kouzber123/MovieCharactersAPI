using System;
using WebApplication1.Models;
namespace MovieCharactersAPI.Services
{



    public interface IFranchiseService
    {   
        Task<IEnumerable<Franchise>> GetAllFranchises();
        Task<Franchise> CreateFranchise(Franchise franchise);
        Task<Franchise> GetFranchiseById(int id);
    }
}