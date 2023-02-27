using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace Seed
{
  public class Seed
  {
    public static async Task SeedCharacters(CharactersDbContext context)
    {
      if (await context.Characters.AnyAsync()) return;

      var userData = await File.ReadAllTextAsync("Data/MigrationsInit/Seeding.json");

      var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

      var users = JsonSerializer.Deserialize<List<Character>>(userData);

      foreach (var user in users)
      {
        context.Characters.Add(user);
      }
      await context.SaveChangesAsync();
    }
  }
}