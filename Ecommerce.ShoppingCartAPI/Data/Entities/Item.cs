namespace Ecommerce.ShoppingCartAPI.Data.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
