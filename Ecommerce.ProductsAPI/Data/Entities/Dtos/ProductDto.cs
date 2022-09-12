namespace Ecommerce.ProductsAPI.Data.Entities.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Cost { get; set; }
        public string ImageUrl { get; set; }
    }
}
