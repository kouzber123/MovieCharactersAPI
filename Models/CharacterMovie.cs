using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;



namespace MovieCharactersApp.Models
{
    public class CharacterMovie
    {
        
        public int CharactersId { get; set; }
        
        public int MoviesId { get; set; }

    }
   
}

