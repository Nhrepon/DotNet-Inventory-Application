using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Models
{
    public class User
    {
        public int Id{get; set;}
        public string? UserName{get; set;}
        [Required]
        public string? UserEmail{get; set;}
        public string? Password{get; set;}
        public string Role{get; set;} = "user";
        public DateTime CreatedAt{get; set;} = DateTime.Now;
        public DateTime UpdatedAt{get; set;} = DateTime.Now;
    }
}