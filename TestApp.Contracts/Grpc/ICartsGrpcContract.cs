using System.ServiceModel;
using ProtoBuf.Grpc;
using TestApp.Contracts.Models;
using TestApp.Contracts.Models.Requests;

namespace TestApp.Contracts.Grpc;

[ServiceContract]
public interface ICartsGrpcContract
{
    Task<CartPc> CreateAsync(CallContext callContext = default);
    Task AddProductToCartAsync(ProductQuantityRequest request, CallContext callContext = default);
    Task ChangeProductQuantityAsync(ProductQuantityRequest request, CallContext callContext = default);
    Task DeleteProductFromCartAsync(ProductCartRequest request, CallContext callContext = default);
    Task<CartPc> GetCartByIdAsync(GetCartByIdPcRequest request, CallContext callContext = default);
    Task<List<CartPc>> GetAllCartsAsync(CallContext callContext = default);

}
