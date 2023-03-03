using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MovieCharactersApp.Data.DataContext;
using MovieCharactersApp.Repositories.ConcreteRepository;
using MovieCharactersApp.Repositories.InterfaceRepository;
using Swashbuckle.AspNetCore.Filters;

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
        options.JsonSerializerOptions.ReferenceHandler = null;
      });

      //swagger for documentation
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1",
          Title = "CHARACTER MOVIE API",
          Description = "Web api application to add characters , movies and fransises to sql database",

        });
        c.ExampleFilters();
        var xmlFile = "./bin/MovieCharacterApi.xml";
        var xmlPath = Path.Combine(xmlFile);
        c.IncludeXmlComments(xmlPath);
      });
      services.AddSwaggerExamplesFromAssemblyOf<Program>();
      services.AddCors();
      services.AddScoped<ICharacterRepository, CharacterRepository>();
      services.AddScoped<IMovieRepository, MovieRepository>();


      //automapper
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      services.AddAutoMapper(typeof(AppDomain));

      return services;
    }
  }
}