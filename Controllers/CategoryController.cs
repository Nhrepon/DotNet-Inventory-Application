using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
