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
                Product = new Product(),
                BrandOptions = await AppDbContext.brands.Select(b => new SelectListItem{
                Value = b.Id.ToString(),
                Text = b.BrandName
            }).ToListAsync(),
            CategoryOptions = await AppDbContext.categories.Select(c => new SelectListItem{
                Value = c.Id.ToString(),
                Text = c.CategoryName
            }).ToListAsync(),
            Files = await AppDbContext.files.OrderByDescending(m => m.Id).ToListAsync(),
            };
            //List<MediaFile> files = AppDbContext.files.OrderByDescending(m => m.Id).ToList();
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




        [Route("product-list")]
        public async Task<IActionResult> ProductList(){
            try
            {
                var product = await AppDbContext.products
                .Include(p => p.category)
                .Include(p => p.brand)
                .Include(p => p.user)
                .OrderByDescending(p => p.Id)
                .Select(p => new ProductView{
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    Color = p.Color,
                    Size = p.Size,
                    Sku = p.Sku,
                    Image = p.Image,
                    CategoryId = p.CategoryId,
                    BrandId = p.BrandId,
                    UserId = p.UserId,
                    CategoryName = p.category.CategoryName,
                    BrandName = p.brand.BrandName,
                    UserName = p.user.UserName,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .Skip(0)
                .Take(5)
                .ToListAsync();
                //return Ok(new { status = "success", message = "Product loaded successfully", data = product });

                return View(product);
            }
            catch (Exception e)
            {
                return Ok(new { status = "error", data = e.Message });
                //return View(e);
            }
        }







        [HttpGet]
        // GET: ProductController/Edit/5
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await AppDbContext.products
            .Include(p => p.category)
            .Include(p => p.brand)
            .FirstOrDefaultAsync(p => p.Id == id);
            if(product == null)return NotFound();
            var productDto = new ProductDto{
                Product = product,
                CategoryOptions = await AppDbContext.categories.Select(c => new SelectListItem{
                Value = c.Id.ToString(),
                Text = c.CategoryName
                }).ToListAsync(),
                BrandOptions = await AppDbContext.brands.Select(b => new SelectListItem{
                    Value = b.Id.ToString(),
                    Text = b.BrandName
                }).ToListAsync(),
                Files = await AppDbContext.files.OrderByDescending(m => m.Id).ToListAsync(),
            };
            
            return View(productDto);
        }



        // POST: ProductController/Edit/5
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Product product)
        {
            try
            {
                var productData = await AppDbContext.products.FindAsync(id);
                if(productData == null){
                    return NotFound();
                }else if (await AppDbContext.products.AnyAsync(p => p.Title == product.Title && p.Id != id)){
                    return Ok(new{status = "duplicate", message = "Product Title already exists"});
                }else{
                    if(productData.Title != product.Title 
                    || productData.Description != product.Description 
                    || productData.Price != product.Price 
                    || productData.Quantity != product.Quantity 
                    || productData.Color != product.Color
                    || productData.Size != product.Size
                    || productData.Sku != product.Sku
                    || productData.Image != product.Image
                    || productData.CategoryId != product.CategoryId
                    || productData.BrandId != product.BrandId
                    ){
                        productData.Title = product.Title;
                    productData.Description = product.Description;
                    productData.Price = product.Price;
                    productData.Quantity = product.Quantity;
                    productData.Color = product.Color;
                    productData.Size = product.Size;
                    productData.Sku = product.Sku;
                    productData.Image = product.Image;
                    productData.CategoryId = product.CategoryId;
                    productData.BrandId = product.BrandId;
                    productData.UserId = product.UserId;
                    productData.UpdatedAt = DateTime.Now;
                    
                    await AppDbContext.SaveChangesAsync();
                    return Ok(new{status = "success", message = "Product updated successfully"});
                    }else{
                        return Ok(new{status = "error", message = "You didn't change anything to update!"});
                    }
                }
            }
            catch(Exception e)
            {
                return Ok(new {status = "error", message = e.Message});
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
