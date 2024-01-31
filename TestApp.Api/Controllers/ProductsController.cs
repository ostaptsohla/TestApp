using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApp.Contracts.Grpc;
using TestApp.Contracts.Models.Api;

namespace TestApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductsGrpcContract _productsGrpcContract;

        public ProductsController(ILogger<ProductsController> logger, IProductsGrpcContract productsGrpcContract)
        {
            _logger = logger;
            _productsGrpcContract = productsGrpcContract;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var productsPc = await _productsGrpcContract.GetProductsAsync();
            return Ok(productsPc.Select(p => new ProductVm()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }));
        }
    }
}
