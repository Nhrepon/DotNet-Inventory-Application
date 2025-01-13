using Inventory.Database;
using Inventory.Models;
using Inventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Controllers
{
    [Route("[controller]")]
    public class BrandController : Controller
    {
        // private readonly IConfiguration _configuration;
        // public BrandController(IConfiguration configuration)
        // {
        //     _configuration = configuration;
        // }
        // [HttpGet]
        // public async Task<IActionResult> BrandList()
        // {
        //     try
        //     {
        //         string connectionString = _configuration.GetConnectionString("SqlConnection");
        //         using (var connection = new SqlConnection(connectionString))
        //         {
        //             //connection.Open();
        //             await connection.OpenAsync();

        //             string query = "Select * from Brands";
        //             using (var command = new SqlCommand(query, connection))
        //             {
        //                 using (var reader = await command.ExecuteReaderAsync())
        //                 {
        //                     var brands = new List<Brand>();
        //                     while (await reader.ReadAsync()) {
        //                         var brand = new Brand{ Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id")), 
        //                         BrandName = reader.IsDBNull(reader.GetOrdinal("BrandName")) ? string.Empty : reader.GetString(reader.GetOrdinal("BrandName")), 
        //                         BrandImg = reader.IsDBNull(reader.GetOrdinal("BrandImg")) ? string.Empty : reader.GetString(reader.GetOrdinal("BrandImg")), 
        //                         BrandDesc = reader.IsDBNull(reader.GetOrdinal("BrandDesc")) ? string.Empty : reader.GetString(reader.GetOrdinal("BrandDesc")),
        //                         CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
        //                         UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt")) };
                                
        //                         // {
        //                         //     Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                         //     BrandName = reader.GetString(reader.GetOrdinal("BrandName")),
        //                         //     BrandImg = reader.GetString(reader.GetOrdinal("BrandImg")),
        //                         //     BrandDesc = reader.GetString(reader.GetOrdinal("BrandDesc"))
        //                         // };
        //                         brands.Add(brand);
        //                     }
        //                     //return Ok(new { status = "success", data = brands });
        //                     return View(brands);
        //                 }
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         return Ok(new { status = "error", message = ex.Message });
        //     }
        // }


        // [HttpPost]
        // public async Task<IActionResult> CreateBrand([FromBody] Brand newBrand)
        // {
        //     if (newBrand == null)
        //     {
        //         return BadRequest(new { status = "error", message = "Invalid brand data." });
        //     }
        //     try
        //     {
        //         string connectionString = _configuration.GetConnectionString("SqlConnection");
        //         using (var connection = new SqlConnection(connectionString))
        //         {
        //             await connection.OpenAsync();
        //             string query =
        //                 "INSERT INTO Brands (BrandName, BrandImg, BrandDesc, CreatedAt, UpdatedAt) VALUES (@BrandName, @BrandImg, @BrandDesc, GETDATE(), GETDATE()); SELECT SCOPE_IDENTITY();";
        //             using (var command = new SqlCommand(query, connection))
        //             {
        //                 command.Parameters.AddWithValue("@BrandName", newBrand.BrandName);
        //                 command.Parameters.AddWithValue("@BrandImg", newBrand.BrandImg);
        //                 command.Parameters.AddWithValue("@BrandDesc", newBrand.BrandDesc);
        //                 var newBrandId = await command.ExecuteScalarAsync();
        //                 if (newBrandId != null)
        //                 {
        //                     newBrand.Id = Convert.ToInt32(newBrandId);
        //                     return Ok(
        //                         new
        //                         {
        //                             status = "success",
        //                             message = "Brand created successfully.",
        //                             data = newBrand,
        //                         }
        //                     );
        //                 }
        //                 else
        //                 {
        //                     return Ok(
        //                         new { status = "error", message = "Failed to create brand." }
        //                     );
        //                 }
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         return Ok(new { status = "error", message = ex.Message });
        //     }
        // }
    
    // --------------------------------------------------------------------------------------


    public readonly AppDbContext AppDbContext;
    public readonly IFileUploadServices fileUpload;
    public BrandController(AppDbContext appDbContext, IFileUploadServices fileUploadServices)
    {
            AppDbContext = appDbContext;
            fileUpload = fileUploadServices;
        }

        





        public IActionResult Index()
        {
            var brands = AppDbContext.brands.OrderByDescending(b => b.Id).ToList();
           return View(brands);
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] BrandDto brandDto){
            bool isDuplicate = AppDbContext.brands.Any(b => b.BrandName == brandDto.BrandName);
            if(isDuplicate){
                return Ok(new { status = "duplicate", message = "Brand Name already exists. Please try with another" });
            }

            
            using(var transaction = AppDbContext.Database.BeginTransaction()){
                try{
                    string filePath = await fileUpload.UploadFile(brandDto.File!);
                    if(filePath == null){
                        return Ok(new { status = "error", message = "File upload failed" });
                    }
                    
                    Brand newBrand = new Brand(){
                        BrandName = brandDto.BrandName,
                        BrandImg = filePath,
                        BrandDesc = brandDto.BrandDesc,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    AppDbContext.brands.Add(newBrand);
                    await AppDbContext.SaveChangesAsync();
                    
                    transaction.Commit();

                }catch(Exception ex){
                    await transaction.RollbackAsync();
                    return Ok(new { status = "error", message = ex.Message });
            }


            return Ok(new { status = "success", message = "Brand created successfully." });
        }
    
    
    
    
    
    }
}
}