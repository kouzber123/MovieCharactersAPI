using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;



namespace WebApplication1.Models
{
    public class FranchiseCharacter
    {

        public string Franchise { get; set; }



        public string FullName { get; set; }

    }

}
