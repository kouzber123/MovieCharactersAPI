using Microsoft.EntityFrameworkCore;
using MovieCharactersApp.Data.DataContext;
using MovieCharactersApp.Data.DTOs.FranchiseDTOs;
using MovieCharactersApp.Repositories.InterfaceRepository;
using WebApplication1.Models;
using MovieCharactersApp.Exceptions;



namespace MovieCharactersApp.Repositories.ConcreteRepository
{
    public class FranchiseRepository : IFranchiseRepository
    {
        private readonly DataContext _context;

        public FranchiseRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Franchise>> GetAllFranchises()
        {
            return await _context.Franchises.ToListAsync();
        }

        public async Task<Franchise> CreateFranchise(Franchise franchise)
        {
            await _context.Franchises.AddAsync(franchise);
            await _context.SaveChangesAsync();
            return franchise;
        }

        public async Task<Franchise> GetFranchiseById(int id)
        {
            var franchise = await _context.Franchises.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);
            if (franchise == null)
            {
                throw new FranchiseNotFoundException(id);
            }
            return franchise;
        }
        public async Task DeleteFranchise(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            if (franchise == null) throw new FranchiseNotFoundException(id);
            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();
        }

        public async Task<Franchise> UpdateFranchise(Franchise franchise)
        {
            var foundFranchise = await _context.Franchises.AnyAsync(x => x.Id == franchise.Id);
            if (!foundFranchise) throw new FranchiseNotFoundException(franchise.Id);

            _context.Entry(franchise).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return franchise;
        }
    }
}