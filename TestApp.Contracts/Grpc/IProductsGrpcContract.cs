using System.ServiceModel;
using ProtoBuf.Grpc;
using TestApp.Contracts.Models;
using TestApp.Contracts.Models.Requests;

namespace TestApp.Contracts.Grpc;

[ServiceContract]
public interface IProductsGrpcContract
{
    Task<List<ProductPc>> GetProductsAsync(CallContext callContext = default);
    
    Task<ProductPc> GetByIdAsync(
        GetProductByIdPcRequest request, CallContext callContext = default);
    
    Task AddAsync(ProductPc model, CallContext callContext = default);

    Task UpdateAsync(ProductPc model, CallContext callContext = default);
}