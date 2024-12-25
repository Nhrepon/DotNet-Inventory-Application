using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace Inventory.Models
{
    public class Brand
    {
        int Id { get; set; }
        string? BrandName { get; set; }
        string? BrandImg { get; set; }
        string? BrandDesc { get; set; }
        
    }
}
