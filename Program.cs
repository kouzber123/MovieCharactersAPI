using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Extensions;
using MovieCharactersAPI.Services;
using MovieCharactersApp.Data.DataContext;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddTransient<IFranchiseService, FranchiseService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateAsyncScope();
var services = scope.ServiceProvider;
try
{
  var context = services.GetRequiredService<DataContext>();
  await context.Database.MigrateAsync();
  await Seed.Seed.SeedCharacters(context);
}
catch (Exception ex)
{
  var logger = services.GetService<ILogger<Program>>();
  logger.LogError(ex, "An Error occured during migration");
}

app.Run();
