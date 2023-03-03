using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace MovieCharactersApp.Data.DTOs.FranchiseDTOs
{

    public class FranchiseCharacterDto
    {
        
        public List<FranchiseCharacterSingleDto> Characters  { get; set; }

        



        

    }
}
