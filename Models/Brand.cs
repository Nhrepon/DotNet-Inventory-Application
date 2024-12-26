

namespace Inventory.Models
{
    public class Brand
    {

        public int Id { get; set; }
        public string? BrandName { get; set; }
        public string? BrandImg { get; set; }
        public string? BrandDesc { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }





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