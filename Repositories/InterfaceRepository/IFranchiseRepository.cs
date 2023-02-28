using MovieCharactersApp.Data.DTOs.CharacterDTOs;
using WebApplication1.Models;

namespace MovieCharactersApp.Repositories.InterfaceRepository
{
    public interface IFranchiseRepository
    {
        Task<IEnumerable<Franchise>> GetAllFranchises();
        Task<Franchise> CreateFranchise(Franchise franchise);
        Task<Franchise> GetFranchiseById(int id);
        Task DeleteFranchise(int id);
        Task<Franchise> UpdateFranchise(Franchise franchise);
    }
}