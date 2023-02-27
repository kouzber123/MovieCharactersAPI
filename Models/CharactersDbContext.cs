using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class CharactersDbContext: DbContext
    {
        public DbSet <Character> Characters { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Franchise> Franchises { get; set; } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }

        private static string GetConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "localhost\\SQLEXPRESS",
                InitialCatalog = "Characters",
                IntegratedSecurity = true,
                TrustServerCertificate = true
            };
            return builder.ConnectionString;
        }
    }
}
