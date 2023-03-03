using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace MovieCharactersApp.Data.DTOs.FranchiseDTOs
{

    public class FranchiseCharacterSingleDto
    {

        public string Franchise { get; set; }



        public IEnumerable<string> Fullname { get; set; }


    }
}