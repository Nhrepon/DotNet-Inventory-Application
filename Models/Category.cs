using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Inventory.Models
{
    [Index(nameof(CategoryName), IsUnique = true)]
    public class Category
    {
        public int Id{get; set;}
        [Required(ErrorMessage = "Category Name is required")]
        [MaxLength(100, ErrorMessage = "Category Name cannot exceed 100 characters")]
        public string CategoryName {get; set;} = "";
        [MaxLength(255)]
        public string CategoryDesc {get; set;} = "";
        [MaxLength(255)]
        public string CategoryImage {get; set;} = "";
        
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;
    }
}
