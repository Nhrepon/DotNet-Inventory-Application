using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventory.Models
{
    public class ProductDto
    {
        public Product Product {get; set;}
        public IEnumerable<SelectListItem> BrandOptions{get; set;}
        public IEnumerable<SelectListItem> CategoryOptions{get; set;}
        public List<MediaFile> Files{get; set;}
    }
}