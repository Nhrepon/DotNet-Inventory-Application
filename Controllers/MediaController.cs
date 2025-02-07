using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Database;
using Inventory.Models;
using Inventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory.Controllers
{
    //[Route("[controller]")]
        
    public class MediaController : Controller
    {
        public readonly AppDbContext AppDbContext;
        public readonly IFileUploadServices FileUploadServices;
        public readonly IWebHostEnvironment WebHostEnvironment;
    public MediaController(AppDbContext appDbContext, IFileUploadServices fileUploadServices, IWebHostEnvironment webHostEnvironment)
    {
            FileUploadServices = fileUploadServices;
            AppDbContext = appDbContext;
            WebHostEnvironment = webHostEnvironment;
        
    }
    

        public async Task<IActionResult> Index()
        {
            var files = await AppDbContext.files.OrderByDescending(m => m.Id).ToListAsync();
            return View(files);
        }

        [HttpPost]
        public async Task<IActionResult> upload(IFormFile file){
            if (file == null){
                return Ok(new {status="failed", message="File should not be empty"});
            }{
                using(var transaction = AppDbContext.Database.BeginTransaction()){
                    try{
                        string filePath = await FileUploadServices.UploadFile(file);
                        if(filePath != null){
                            MediaFile media = new MediaFile(){
                                filePath=filePath,
                                CreatedAt=DateTime.Now,
                                UpdatedAt=DateTime.Now
                            };
                            AppDbContext.files.Add(media);
                            await AppDbContext.SaveChangesAsync();
                            transaction.Commit();
                        }
                        return Ok(new { status = "success", message = "File uploaded successfully." , filePath = filePath });

                    }catch(Exception e){
                        await transaction.RollbackAsync();
                        return Ok(new { status = "error", message = e.Message });
                    }

                    
                }
                
            }
        } 


        


        public IActionResult Delete(int id){
            var file = AppDbContext.files.Find(id);
            if(file == null){
                return RedirectToAction("Index", "Media");
            }
            string oldFilePath = Path.Combine(WebHostEnvironment.WebRootPath + file.filePath);
            if(System.IO.File.Exists(oldFilePath)){
                System.IO.File.Delete(oldFilePath);
            }
            AppDbContext.files.Remove(file);
            AppDbContext.SaveChanges();
            //return RedirectToAction("Index", "Media");
            return Ok(new {status="success", message = "file deleted successfully!"});
        }











    
    }
}