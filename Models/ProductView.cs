using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Models
{
    public class ProductView:Product
    {
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string UserName { get; set; }
    }
}