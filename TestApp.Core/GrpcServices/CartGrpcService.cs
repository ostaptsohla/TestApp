using ProtoBuf.Grpc;
using TestApp.Contracts.Grpc;
using TestApp.Contracts.Models;
using TestApp.Contracts.Models.Requests;
using TestApp.Models;
using TestApp.Services;


namespace TestApp.GrpcServices;

public class CartGrpcService : ICartsGrpcContract
{
    private readonly ICartService _cartService;

    public CartGrpcService(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<CartPc> CreateAsync(CallContext callContext = default)
    {
        return await _cartService.CreateAsync(callContext.CancellationToken);
    }
    public async Task AddProductToCartAsync(ProductQuantityRequest request, CallContext callContext = default)
    {
        await _cartService.AddProductToCartAsync(request, callContext.CancellationToken);
    }
    public async Task ChangeProductQuantityAsync(ProductQuantityRequest request, CallContext callContext = default)
    {
        await _cartService.ChangeProductQuantityAsync(request, callContext.CancellationToken);
    }
    public async Task DeleteProductFromCartAsync(ProductCartRequest request, CallContext callContext = default)
    {
        await _cartService.DeleteProductFromCartAsync(request, callContext.CancellationToken);
    }
    public async Task<CartPc> GetCartByIdAsync(GetCartByIdPcRequest request, CallContext callContext = default)
    {
        return await _cartService.GetCartByIdAsync(request, callContext.CancellationToken);
    }
    public async Task<List<CartPc>> GetAllCartsAsync(CallContext callContext = default)
    {
        return await _cartService.GetAllCartsAsync(callContext.CancellationToken);
    }
}
