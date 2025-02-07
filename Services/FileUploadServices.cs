using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.DotNet.MSIdentity.Shared;

namespace Inventory.Services
{
    public interface IFileUploadServices
    {
        Task<string> UploadFile(IFormFile file);
    }
    public class FileUploadServices: IFileUploadServices
    {
        public readonly IWebHostEnvironment WebHostEnvironment;
        public FileUploadServices(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadFile(IFormFile file){
            if(file == null || file.Length == 0) throw new Exception("File is empty");
            if(!Directory.Exists(Path.Combine(WebHostEnvironment.WebRootPath, "uploads"))){
                Directory.CreateDirectory(Path.Combine(WebHostEnvironment.WebRootPath, "uploads"));
            }
            if(file.Length > 5*1024*1024){
                throw new Exception("File is too large");
            }
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" +Random.Shared.Next(1000, 9999)+ "_" + "DotNet_Inventory"+"_" + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "uploads/", fileName);
            using(var fileStream = new FileStream(filePath, FileMode.Create)){
                await file.CopyToAsync(fileStream);
            }
            return "/uploads/"+fileName;
        }

    }
}