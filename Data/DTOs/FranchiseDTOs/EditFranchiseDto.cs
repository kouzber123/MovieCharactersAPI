using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace MovieCharactersApp.Data.DTOs.FranchiseDTOs
{

    public class EditFranchiseDto
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<FranchiseMovieDto> Movies { get; set; }

    }
}