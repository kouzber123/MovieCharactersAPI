using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication1.Models
{
  public class Franchise
  {
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(40)")]
    public string Name { get; set; }

    public string Description { get; set; }

    public ICollection<Movie> Movies { get; set; }

  }
}
