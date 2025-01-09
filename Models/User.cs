
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace Inventory.Models
{
    [Index(nameof(UserEmail), IsUnique = true)]
    public class User
    {
        public int Id{get; set;}
        [MaxLength(50)]
        public string UserName{get; set;} = "";
        [Required]
        [EmailAddress]
        public string? UserEmail{get; set;}
        [Required]
        [MinLength(4)]
        public string? PasswordHash{get; set;}
        public string UserAddress{get; set;} = "";
        public enum UserRole{Admin=1, User=0}
        public DateTime CreatedAt{get; set;} = DateTime.Now;
        public DateTime UpdatedAt{get; set;} = DateTime.Now;
    }
}