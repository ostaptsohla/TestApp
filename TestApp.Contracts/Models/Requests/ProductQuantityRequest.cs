using ProtoBuf;

namespace TestApp.Contracts.Models.Requests;

[ProtoContract]
public class ProductQuantityRequest
{
    [ProtoMember(1)]
    public int CartId { get; set; }
    [ProtoMember(2)]
    public int ProductId { get; set; }
    [ProtoMember(3)]
    public int Quantity { get; set; }
}
