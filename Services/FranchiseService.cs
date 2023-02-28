using System;
using WebApplication1.Models;
using MovieCharactersApp.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Exceptions;

namespace MovieCharactersAPI.Services
{



    public class FranchiseService : IFranchiseService
    {
        private readonly DataContext _context;

        public FranchiseService(DataContext context)
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
            var franchise = await _context.Franchises.Include(x =>x.Movies).FirstOrDefaultAsync(x => x.Id == id);
            if (franchise == null)
            {
                throw new FranchiseNotFoundException(id);
            }
            return franchise;
        }




    }

}
