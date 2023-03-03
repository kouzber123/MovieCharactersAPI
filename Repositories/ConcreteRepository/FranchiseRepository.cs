using Microsoft.EntityFrameworkCore;
using MovieCharactersApp.Data.DataContext;
using MovieCharactersApp.Data.DTOs.FranchiseDTOs;
using MovieCharactersApp.Repositories.InterfaceRepository;
using WebApplication1.Models;
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

        public async Task<List<FranchiseCharacterDto>>CharactersInFranchise(int id)
        {
            
            


            var result = await (from c in _context.Characters
                          join mv in _context.CharacterMovies on c.Id equals mv.CharactersId
                          join m in _context.Movies on mv.MoviesId equals m.Id
                          join f in _context.Franchises on m.Id equals f.Id
                          where f.Id == id
                          select new
                          {
                              Franchise = f.Name,

                              
                              Fullname = c.FullName
                          }).ToListAsync();
            
           

            return _mapper.Map<List<FranchiseCharacterDto>>(result);
        }


    }
}