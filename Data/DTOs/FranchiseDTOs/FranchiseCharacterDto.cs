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
        [Required]
        public string Franchise { get; set; }

        

        public string FullName { get; set; }



        

    }
}
