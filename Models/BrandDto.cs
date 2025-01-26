
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Inventory.Models
{
    public class BrandDto
    {
        public required string BrandName { get; set; }
        [MaxLength(255)]
        public string BrandDesc { get; set; } = "";
        public IFormFile? File {get; set;}
    }
}