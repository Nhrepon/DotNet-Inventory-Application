namespace Inventory.Models
{
    public class Product
    {
        public int Id{ get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public string? Quantity { get; set; }
        public string? Price { get; set; }
        public string? Sku { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;
        
        public Category category {get; set;}
        public Brand brand {get; set;}
        public User user {get; set;}

    }
}
