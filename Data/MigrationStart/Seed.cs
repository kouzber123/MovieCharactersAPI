using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MovieCharactersApp.Data.DataContext;
using MovieCharactersApp.Models;

namespace Seed
{
  public class Seed
  {
    public static async Task SeedCharacters(DataContext context)
    {
      if (await context.Characters.AnyAsync()) return;

      var userData = await File.ReadAllTextAsync("Data/MigrationStart/Seeding.json");

      var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

      var users = JsonSerializer.Deserialize<List<Character>>(userData);

      foreach (var characterData in users)
      {

        context.Characters.Add(characterData);
      }

      await context.SaveChangesAsync();
    }
  }
}