using Microsoft.EntityFrameworkCore;
using MovieCharactersApp.Data.DataContext;
using MovieCharactersApp.Data.DTOs.FranchiseDTOs;
using MovieCharactersApp.Repositories.InterfaceRepository;
using MovieCharactersApp.Models;
using MovieCharactersApp.Exceptions;
using AutoMapper;

namespace MovieCharactersApp.Repositories.ConcreteRepository
{
    public class FranchiseRepository : IFranchiseRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FranchiseRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<EditFranchiseDto> UpdateFranchise(EditFranchiseDto franchise)
        {
            var foundFranchise = await _context.Franchises.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == franchise.Id);
            if (foundFranchise == null)
            {
                throw new FranchiseNotFoundException(franchise.Id);
            }

            foreach (var movie in franchise.Movies.ToList())
            {

                var existingMovie = await _context.Movies.FindAsync(movie.Id);

                _mapper.Map(movie, existingMovie);
                
               foundFranchise.Movies.Add(existingMovie);
            }

                _context.Entry(foundFranchise).State = EntityState.Modified;
            await _context.SaveChangesAsync();
           
            return franchise;
        }

        public async Task<FranchiseCharacterDto> CharactersInFranchise(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);

            var movies = await _context.Movies
                .Include(c => c.Characters)
                .Where(m => m.FranchiseId== id)
                .ToListAsync();





            var result = new FranchiseCharacterDto();
            result.Characters = new List<FranchiseCharacterSingleDto>();

            var single = new FranchiseCharacterSingleDto();

            foreach(var character in movies)
            {
                
                single.Franchise = character.Franchise.Name;
                single.Fullname =  character.Characters.Select(c => c.FullName);
                result.Characters.Add(single);
              
                

            }

            return result;

        }

    }
}