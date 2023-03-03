using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace MovieCharactersApp.Data.DataContext
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    

    public DbSet<Character> Characters { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Franchise> Franchises { get; set; }
    public DbSet<CharacterMovie> CharacterMovies { get; set; }
   

    }
}