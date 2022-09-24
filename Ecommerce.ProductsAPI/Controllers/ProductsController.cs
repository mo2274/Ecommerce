using Ecommerce.ProductsAPI.Data.Entities.Dtos;
using Ecommerce.ProductsAPI.RabbitMQ;
using Ecommerce.ProductsAPI.Repositories;
using Ecommerce.ProductsAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Ecommerce.ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IMessageProducer messageProducer;

        public ProductsController(IProductRepository productRepository, IMessageProducer messageProducer)
        {
            this.productRepository = productRepository;
            this.messageProducer = messageProducer;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> Get()
        {
            return Ok(await productRepository.GetProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await productRepository.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost("cart")]
        [Authorize]
        public async Task AddProductToShoppingCart(int productId)
        {
            var userName = User.Identity.Name;
            var product = await productRepository.GetProductByIdAsync(productId);
            var message = JsonSerializer.Serialize(new ItemModel()
            {
                UserName = userName,
                ProductName = product.Name,
                Price = product.Price
            }); ;
            messageProducer.SendMessage(message);
        }
    }
}
