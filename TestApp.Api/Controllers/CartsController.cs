using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TestApp.Contracts.Grpc;
using TestApp.Contracts.Models;
using TestApp.Contracts.Models.Requests;

namespace TestApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly ILogger<CartsController> _logger;
    private readonly ICartsGrpcContract _cartsGrpcContract;

    public CartsController(ILogger<CartsController> logger, ICartsGrpcContract cartsGrpcContract)
    {
        _logger = logger;
        _cartsGrpcContract = cartsGrpcContract;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllProducts()
    {
        var cartsPc = await _cartsGrpcContract.GetAllCartsAsync();
        return Ok(cartsPc);
    }

    [HttpGet]
    [Route("id/{id}")]
    public async Task<IActionResult> GetCartById([FromRoute] int id)
    {
        var cartsPc = await _cartsGrpcContract.GetCartByIdAsync(new GetCartByIdPcRequest()
        {
            Id = id
        });
        return Ok(cartsPc);
    }

    [HttpPost]
    public async Task<IActionResult> Create()
    {
        var newCart = await _cartsGrpcContract.CreateAsync();
        return Ok(newCart);
    }

    [HttpPut]
    public async Task<IActionResult> AddProductToCart([FromBody] ProductQuantityRequest request)
    {
        await _cartsGrpcContract.AddProductToCartAsync(request);
        return Ok();
    }

    [HttpPut]
    [Route("cartId/{cartId}/productId/{productId}/quantity/{quantity}")]
    public async Task<IActionResult> ChangeProductQuantity([FromRoute] int cartId, [FromRoute] int productId, [FromRoute] int quantity)
    {
        await _cartsGrpcContract.ChangeProductQuantityAsync(new ProductQuantityRequest
        {
            CartId = cartId,
            ProductId = productId,
            Quantity = quantity
        });
        return Ok();
    }

    [HttpDelete]
    [Route("cartId/{cartId}/productId/{productId}")]
    public async Task<IActionResult> DeleteProductFromCart([FromRoute] int cartId, [FromRoute] int productId)
    {
        await _cartsGrpcContract.DeleteProductFromCartAsync(new ProductCartRequest
        {
            CartId = cartId,
            ProductId = productId
        });
        return Ok();
    }

    [HttpGet]
    [Route("discounts/all")]
    public async Task<IActionResult> GetAllDiscounts()
    {
        var discounts =  await _cartsGrpcContract.GetAllDiscountsAsync();
        return Ok(discounts);
    }

    [HttpPost]
    [Route("discounts")]
    public async Task<IActionResult> AddDiscount([FromBody] DiscountRequest request)
    {
        await _cartsGrpcContract.AddDiscountAsync(request);
        return Ok();
    }
}
