using Inventory.Database;
using Inventory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        public readonly AppDbContext AppDbContext;
        public ProductController(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
            
        }
        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductController/Details/5
        [Route("details/{id}")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        [Route("create")]
        public async Task<ActionResult> Create()
        {
            var data = new ProductDto{
                Product = new Product()
            };
            data.BrandOptions =await AppDbContext.brands.Select(b => new SelectListItem{
                Value = b.Id.ToString(),
                Text = b.BrandName
            }).ToListAsync();
            data.CategoryOptions =await AppDbContext.categories.Select(c => new SelectListItem{
                Value = c.Id.ToString(),
                Text = c.CategoryName
            }).ToListAsync();
            //List<MediaFile> files = AppDbContext.files.OrderByDescending(m => m.Id).ToList();
            data.Files =await AppDbContext.files.OrderByDescending(m => m.Id).ToListAsync();
            //var files = AppDbContext.files.OrderByDescending(m => m.Id).ToList();
            return View(data);
        }

        // POST: ProductController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            try{
                await AppDbContext.products.AddAsync(product);
                await AppDbContext.SaveChangesAsync();
                return Ok(new { status = "success", message = "Product created successfully"});
            }
            catch(Exception e){
                return Ok(new { status = "error", message = e.Message });
            }
        }

        // GET: ProductController/Edit/5
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
