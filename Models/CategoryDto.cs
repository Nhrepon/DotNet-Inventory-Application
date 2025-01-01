
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Inventory.Models
{
    public class CategoryDto
    {
        
        [Required, MaxLength(100)]
        public string CategoryName {get; set;} = "";
        [MaxLength(255)]
        public string CategoryDesc {get; set;} = "";
        
        public IFormFile? CategoryImage {get; set;}
    }
}