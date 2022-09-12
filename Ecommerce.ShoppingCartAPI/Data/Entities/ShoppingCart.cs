namespace Ecommerce.ShoppingCartAPI.Data.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsOpened { get; set; }
        public List<Item> Items { get; set; }
    }
}
