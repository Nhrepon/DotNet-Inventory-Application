

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Models
{
    [Index(nameof(BrandName), IsUnique = true)]
    public class Brand
    {

        public int Id { get; set; }
        [Required, MaxLength(50), NotNull]
        public required string BrandName { get; set; }
        [MaxLength(255)]
        public string BrandDesc { get; set; } = "";
        public string BrandImg { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;





        //public bool ValidateBrand() {

        //    bool status = false;
        //    string connectionString = _configuration.GetConnectionString("SqlConnection").ToString();
        //    SqlConnection connection = new SqlConnection(connectionString);
        //    connection.Open();

        //    try {

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = connection;
        //        cmd.CommandText = "GetAllBrands";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 1000;

        //        SqlDataReader reader = cmd.ExecuteReader();
        //        //cmd.ExecuteNonQuery();
        //        while (reader.Read())
        //        {
        //            if (reader.HasRows)
        //            {
        //                status = true;
        //            }
        //        }
        //        reader.Close();

        //        //DataTable dataTable = new DataTable();
        //        //SqlDataAdapter adapter = new SqlDataAdapter();
        //        //adapter.Fill(dataTable);

        //    }catch (Exception ex) {
        //        connection.Close();
        //        connection.Dispose();
        //    }finally {
        //        connection.Close();
        //        connection.Dispose();
        //    }


        //    return status;
        //}




    }
}