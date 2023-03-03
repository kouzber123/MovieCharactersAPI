using MovieCharactersApp.Data.DTOs.CharacterDTOs;
using MovieCharactersApp.Data.DTOs.FranchiseDTOs;
using WebApplication1.Models;

namespace MovieCharactersApp.Repositories.InterfaceRepository
{
    public interface IFranchiseRepository
    {
        Task<IEnumerable<Franchise>> GetAllFranchises();
        Task<Franchise> CreateFranchise(Franchise franchise);
        Task<Franchise> GetFranchiseById(int id);
        Task DeleteFranchise(int id);
        Task<EditFranchiseDto> UpdateFranchise(EditFranchiseDto franchise);
        Task<IEnumerable<FranchiseCharacterDto>> CharactersInFranchise(int id);
    }
}