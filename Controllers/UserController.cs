
using Inventory.Database;
using Inventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Inventory.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        public readonly AppDbContext AppDbContext;

        public UserController(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }




        // private readonly IConfiguration _configuration;
        // public UserController(IConfiguration configuration)
        // {
        //     _configuration = configuration;
        // }

        public IActionResult Index(){
            var user = AppDbContext.users.OrderByDescending(u => u.Id).ToList();
            return View(user);
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        // [HttpPost]
        // [Route("login")]
        // public async Task<IActionResult> Logins([FromBody] User user)
        // {
        //     try
        //     {

        //         string connectionString = _configuration.GetConnectionString("SqlConnection");
        //         using (var connection = new SqlConnection(connectionString)) {
        //             //connection.Open();
        //             await connection.OpenAsync();

        //             string query = @"SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password";
        //             var data = await connection.QueryFirstOrDefaultAsync<User>(query, 
        //                 new { UserName = user.UserName, Password = user.Password });

        //             if (data != null) 
        //             {
        //                 return Ok(new { status = "success", message = "Login successful" , data = data}); 
        //             }
        //             else
        //             {
        //                 return Unauthorized(new { status = "error", message = "Invalid username or password" });
        //             }


        //             // using(var command = new SqlCommand(query, connection)) {
        //             //     using (var reader = await command.ExecuteReaderAsync()) {
        //             //         var user = new List<User>();
        //             //         while (await reader.ReadAsync()) {
        //             //             var data = new User{ Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id")), 
        //             //             UserName = reader.IsDBNull(reader.GetOrdinal("UserName")) ? string.Empty : reader.GetString(reader.GetOrdinal("UserName")), 
        //             //             UserEmail = reader.IsDBNull(reader.GetOrdinal("UserEmail")) ? string.Empty : reader.GetString(reader.GetOrdinal("UserEmail")), 
        //             //             Password = reader.IsDBNull(reader.GetOrdinal("Password")) ? string.Empty : reader.GetString(reader.GetOrdinal("Password")),
        //             //             CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
        //             //             UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt")) };
                                
                                
        //             //             // {
        //             //             //     Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //             //             //     BrandName = reader.GetString(reader.GetOrdinal("BrandName")),
        //             //             //     BrandImg = reader.GetString(reader.GetOrdinal("BrandImg")),
        //             //             //     BrandDesc = reader.GetString(reader.GetOrdinal("BrandDesc"))
        //             //             // };
        //             //             user.Add(data);
        //             //         }
        //             //         return Ok(new { status = "success", data = user });
        //             //         //return View(user);
        //             //     }
        //             // }
                    
        //         }
                    
        //     }
        //     catch (Exception ex) {
        //         return Ok(new { status = "error", message = ex.Message });
        //     }
        // }

        [HttpPost]
        [Route("userLogin")]
        public async Task<IActionResult> UserLogin([FromBody] User user){
            var data = await AppDbContext.users.FirstOrDefaultAsync(u => u.UserEmail == user.UserEmail && u.PasswordHash == user.PasswordHash);
            if (data != null && data.UserEmail == user.UserEmail) {
                HttpContext.Session.SetString("UserName", data.UserName);
                HttpContext.Session.SetString("UserEmail", data.UserEmail);
                //HttpContext.Session.SetString("UserRole", data.UserRole.ToString());
                
                return Ok(new { status = "success", message = "Login successful" , data = data});
            }
                return Ok(new { status = "error", message = "Invalid username or password" });

            
        }
        // public async Task<IActionResult> Logins([FromBody] User user)
        // {
        //     if (user == null)
        //     {
        //         return BadRequest(new { status = "error", message = "Invalid user data." });
        //     }

        //     try
        //     {
        //         string connectionString = _configuration.GetConnectionString("SqlConnection");
        //         using (var connection = new SqlConnection(connectionString))
        //         {
        //             await connection.OpenAsync();

        //             string query = @"SELECT * FROM Users WHERE UserEmail = @UserEmail AND Password = @Password";
        //             using (var command = new SqlCommand(query, connection))
        //             {
        //                 command.Parameters.AddWithValue("@UserEmail", user.UserEmail);
        //                 command.Parameters.AddWithValue("@Password", user.Password);

        //                 using (var reader = await command.ExecuteReaderAsync())
        //                 {
        //                     if (await reader.ReadAsync())
        //                     {
        //                         var loggedInUser = new User
        //                         {
        //                             Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                             UserName = reader.GetString(reader.GetOrdinal("UserName")),
        //                             UserEmail = reader.GetString(reader.GetOrdinal("UserEmail")),
        //                             //Password = reader.GetString(reader.GetOrdinal("Password")),
        //                             CreatedAt = reader.GetDateTime(reader.GetOrdinal("createdAt")),
        //                             UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updatedAt")),
        //                             // Include other user properties as needed
        //                         };

        //                         return Ok(new { status = "success", message = "Login successful", data = loggedInUser });
        //                     }
        //                     else
        //                     {
        //                         return Unauthorized(new { status = "error", message = "Invalid username or password" });
        //                     }
        //                 }
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         return Ok(new { status = "error", message = ex.Message });
        //     }
        // }





        [HttpGet]
        [Route("registration")]
        public IActionResult Registration()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] User user){

            bool isDuplicate = AppDbContext.users.Any(c => c.UserEmail == user.UserEmail); 
            if(isDuplicate) {
                    return Ok(new { status = "duplicate", message = "Email already exists. Please try to login" });
                }

                if(user.UserEmail != null && user.PasswordHash != null){
                    try{
                    User newUser = new User{
                        UserName = user.UserName,
                        UserEmail = user.UserEmail,
                        PasswordHash = user.PasswordHash
                    };
                    AppDbContext.users.Add(newUser);
                    await AppDbContext.SaveChangesAsync();
                    return Ok(new { status = "success", message = "Registration successful", data = newUser });
                }catch(Exception ex){
                    return Ok(new { status = "error", message = ex.Message });
                }
                }

            return View(user);
            
        }

        // public async Task<IActionResult> Registration([FromBody] User user)
        // {
        //     if (user == null)
        //     {
        //         return BadRequest(new
        //         {
        //             status = "error",
        //             message = "Invalid user data."
        //         });
        //     }
        //     try
        //     {
        //         string connectionString = _configuration.GetConnectionString("SqlConnection");
        //         using (var connection = new SqlConnection(connectionString))
        //         {
        //             DateTime dateTime = DateTime.Now;
        //             await connection.OpenAsync();
        //             string query = "INSERT INTO Users (UserName, UserEmail, Password, CreatedAt, UpdatedAt) VALUES (@UserName, @UserEmail, @Password, GETDATE(), GETDATE() ); SELECT SCOPE_IDENTITY();";
        //             using (var command = new SqlCommand(query, connection))
        //             {
        //                 command.Parameters.AddWithValue("@UserName", user.UserName);
        //                 command.Parameters.AddWithValue("@UserEmail", user.UserEmail);
        //                 command.Parameters.AddWithValue("@Password", user.Password);
        //                 var userId = await command.ExecuteScalarAsync();
        //                 if (userId != null)
        //                 {
        //                     user.Id = Convert.ToInt32(userId);
        //                     return Ok(new { status = "success", message = "Brand created successfully.", data = user });
        //                     //return View(user);
        //                 }
        //                 else
        //                 {
        //                     return Ok(new { status = "error", message = "Failed to create brand." });
        //                 }
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         return Ok(new { status = "error", message = ex.Message});
        //     }
        // }

















    }
}
