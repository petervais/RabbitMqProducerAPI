using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMqProducerAPI.Models;
using RabbitMqProducerAPI.RabbitMQ;
using RabbitMqProducerAPI.Services;

namespace RabbitMqProducerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IRabbitMqProducer _rabbitMqProducer;

        public ProductController(IProductService productService, IRabbitMqProducer rabbitMqProducer)
        {
            _productService = productService;
            _rabbitMqProducer = rabbitMqProducer;
        }

        [HttpGet("products")]
        public IEnumerable<Product> ProductList()
        {
            var productList = _productService.GetProductList();
            return productList;
        }

        [HttpGet("product")]
        public Product GetProductById(int Id)
        {
            return _productService.GetProductById(Id);
        }

        [HttpPost("product")]
        public Product AddProduct(Product product)
        {
            var productData = _productService.AddProduct(product);
            //send the inserted product data to the queue and consumer will listening this data from queue
            _rabbitMqProducer.SendProductMessage(productData);
            return productData;
        }

        [HttpPut("product")]
        public Product UpdateProduct(Product product)
        {
            return _productService.UpdateProduct(product);
        }

        [HttpDelete("product")]
        public bool DeleteProduct(int Id)
        {
            return _productService.DeleteProduct(Id);
        }
    }
}
