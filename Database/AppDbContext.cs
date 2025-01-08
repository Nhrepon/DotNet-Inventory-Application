using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Database
{
    public class AppDbContext : DbContext
    {
        public readonly DbContextOptions Options;
        public AppDbContext(DbContextOptions options): base(options)
        {
            Options = options;
        }
        public DbSet<Category> categories{set; get;}
    }
}