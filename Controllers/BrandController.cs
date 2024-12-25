using Inventory.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Controllers
{
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;
        public BrandController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Brand()
        {
            var brands = await _context.brands.ToListAsync();
            return Ok(brands);
        }




        public IActionResult Index()
        {
            return View();
        }
    }
}
