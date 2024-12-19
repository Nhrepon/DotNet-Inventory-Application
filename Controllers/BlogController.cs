using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
