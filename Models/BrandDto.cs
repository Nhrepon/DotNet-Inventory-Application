
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Inventory.Models
{
    public class BrandDto:Brand
    {
        public IFormFile? File {get; set;}
    }
}