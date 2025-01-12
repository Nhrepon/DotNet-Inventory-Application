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
        public required DbSet<Category> categories{set; get;}
        public required DbSet<User> users{set; get;}
        public DbSet<Brand> brands{set; get;}
    }
}