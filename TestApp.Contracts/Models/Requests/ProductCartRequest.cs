using ProtoBuf;

namespace TestApp.Contracts.Models.Requests;

[ProtoContract]
public class ProductCartRequest
{
    [ProtoMember(1)]
    public int CartId { get; set; }
    [ProtoMember(2)]
    public int ProductId { get; set; }
}
