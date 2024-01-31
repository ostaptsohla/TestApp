using ProtoBuf.Grpc;
using TestApp.Contracts.Grpc;
using TestApp.Contracts.Models;
using TestApp.Contracts.Models.Requests;
using TestApp.Services;

namespace TestApp.GrpcServices;

public class ProductGrpcService : IProductsGrpcContract
{
    private readonly IProductService _productService;
    
    public ProductGrpcService(
        IProductService productService)
    {
        _productService = productService;
    }

    public async Task<List<ProductPc>> GetProductsAsync(CallContext callContext = default)
    {
        return await _productService.GetAllAsync(callContext.CancellationToken);
    }

    public async Task<ProductPc> GetByIdAsync(
        GetProductByIdPcRequest request, CallContext callContext = default)
    {
        return await _productService.GetByIdAsync(request, callContext.CancellationToken);
    }

    public async Task AddAsync(ProductPc model, CallContext callContext = default)
    {
        await _productService.AddAsync(model, callContext.CancellationToken);
    }

    public async Task UpdateAsync(ProductPc model, CallContext callContext = default)
    {
        await _productService.UpdateAsync(model, callContext.CancellationToken);
    }
}