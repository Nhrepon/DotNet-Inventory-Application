using Inventory.Database;
using Inventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace Inventory.Controllers
{
    public class CategoryController : Controller
    {
        
        private readonly AppDbContext Context;
        public IWebHostEnvironment WebHostEnvironment;
        public CategoryController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.WebHostEnvironment = webHostEnvironment;
            Context = context;
        }



        public IActionResult Index()
        {
            var categories = Context.category.OrderByDescending(c => c.Id).ToList();
            return View(categories);
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto){
//Console.WriteLine("start invalid check ................... " + ModelState.IsValid);

    // if (!ModelState.IsValid)
    // {
    //     // Log detailed ModelState errors
    //     foreach (var state in ModelState)
    //     {
    //         Console.WriteLine($"Key: {state.Key}, Error: {string.Join(", ", state.Value.Errors.Select(e => e.ErrorMessage))}");
    //     }
    //     return View(categoryDto);
    // }

            // Check for duplicate category name 
            bool isDuplicate = Context.category.Any(c => c.CategoryName == categoryDto.CategoryName); 
            if (isDuplicate) { ModelState.AddModelError("CategoryName", "Category Name already exists");}


            if(!ModelState.IsValid){
                return View(categoryDto);
            }

            const long maxSize = 10 * 1024 * 1024; // 10 MB
                if(categoryDto.CategoryImage != null && categoryDto.CategoryImage.Length > 0 && categoryDto.CategoryImage.Length < maxSize){
                Console.WriteLine("Image is not null");
            
                    Category cat = new Category(){
                CategoryName = categoryDto.CategoryName,
                CategoryDesc = categoryDto.CategoryDesc?? "",
                CategoryImage = "",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
                };

                using(var transaction = await Context.Database.BeginTransactionAsync()){
                    try{
                        Context.category.Add(cat);
                        await Context.SaveChangesAsync();

                        string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(categoryDto.CategoryImage!.FileName);

                        string filePath = Path.Combine(WebHostEnvironment.WebRootPath + "/uploads/" + fileName);
                        Console.WriteLine("Image file path is: " + filePath);
                        using(var stream = new FileStream(filePath, FileMode.Create)) {
                            await categoryDto.CategoryImage.CopyToAsync(stream);
                        }

                        // Update the category with the correct image path 
                        cat.CategoryImage = "/uploads/" + fileName; 
                        Context.category.Update(cat); 
                        await Context.SaveChangesAsync(); 
                        // Commit the transaction 
                        await transaction.CommitAsync();


                    }catch(Exception ex){
                        await transaction.RollbackAsync();
                        ModelState.AddModelError("CategoryImage", "An error occurred while uploading the image.");
                        return View(categoryDto);
                    }
                }

            return RedirectToAction("Index", "Category");

            }
            return View(categoryDto);
        

    }




        public IActionResult Edit(int id){
            var category = Context.category.Find(id);

            if(category == null){
                return RedirectToAction("Index", "Category");
            }
            var categoryDto = new CategoryDto(){
                
                CategoryName = category.CategoryName,
                CategoryDesc = category.CategoryDesc,
            };
            ViewData["Id"] = category.Id;
            ViewData["CategoryImage"] = category.CategoryImage;
            ViewData["CreatedAt"] = category.CreatedAt.ToString("dd/MM/yyyy");
            ViewData["UpdatedAt"] = category.UpdatedAt.ToString("dd/MM/yyyy");

            return View(categoryDto);
        }



        [HttpPost]
        public IActionResult Edit (int id, CategoryDto categoryDto){
            var category = Context.category.Find(id);

            if(category == null){
                return RedirectToAction("Index", "Category");
            }

            if(!ModelState.IsValid){
                ViewData["Id"] = category.Id;
                ViewData["CategoryImage"] = category.CategoryImage;
                ViewData["CreatedAt"] = category.CreatedAt.ToString("dd/MM/yyyy");
                ViewData["UpdatedAt"] = category.UpdatedAt.ToString("dd/MM/yyyy");
                return View(categoryDto);
            }


            category.CategoryName = categoryDto.CategoryName;
            category.CategoryDesc = categoryDto.CategoryDesc;
            category.UpdatedAt = DateTime.Now;
            Context.category.Update(category);
            Context.SaveChanges();
            return RedirectToAction("Index", "Category");

        }














    }
}
