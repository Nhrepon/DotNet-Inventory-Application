using Inventory.Database;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    public class CategoryController : Controller
    {

        public CategoryController(AppDbContext context)
        {
            Context = context;
        }

        public AppDbContext Context { get; }

        public IActionResult Index()
        {
            var categories = Context.category.ToList();
            return View(categories);
        }
    }
}
