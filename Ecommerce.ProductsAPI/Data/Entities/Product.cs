using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ProductsAPI.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public double Price { get; set; }
        public double Cost { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
