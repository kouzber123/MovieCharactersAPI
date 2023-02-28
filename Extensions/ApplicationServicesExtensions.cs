using Microsoft.EntityFrameworkCore;
using MovieCharactersApp.Repositories.ConcreteRepository;
using MovieCharactersApp.Data.DataContext;
using MovieCharactersApp.Repositories.ConcreteRepository;
using MovieCharactersApp.Repositories.InterfaceRepository;
using System.Text.Json.Serialization;

namespace MovieCharactersApp.Extensions
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
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

            services.AddCors();
      services.AddScoped<ICharacterRepository, CharacterRepository>();

      services.AddScoped<IMovieRepository, MovieRepository>();

      //-------------AUTOMAPPER
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      services.AddAutoMapper(typeof(AppDomain));

      return services;
    }
  }
}