using Microsoft.EntityFrameworkCore;
using MovieCharactersApp.Data.DataContext;

namespace MovieCharactersAPI.Extensions
{
  public static class ApplicationServicesExtensions
  {

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {

      services.AddDbContext<DataContext>(option =>
      {
        option.UseSqlServer(config.GetConnectionString("Db"));

      });

      //ADD INTERFACE REPO / CONCRETE REPO

      services.AddCors();
      //   services.AddScoped<ICharacterRepository, CharacterRepository>();

      //   services.AddScoped<IMovieRepository, MovieRepository>();

      //-------------AUTOMAPPER
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      services.AddAutoMapper(typeof(AppDomain));

      return services;
    }
  }
}