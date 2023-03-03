using Microsoft.EntityFrameworkCore;
using MovieCharactersApp.Models;

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


    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterMovie>().HasKey(cm => new {cm.CharactersId, cm.MoviesId});
        }


    }
}