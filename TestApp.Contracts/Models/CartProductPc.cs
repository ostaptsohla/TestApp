using ProtoBuf;

namespace TestApp.Contracts.Models;

[ProtoContract]
public class CartProductPc
{
    [ProtoMember(1)]
    public ProductPc Product { get; set; }
    [ProtoMember(2)]
    public int Quantity { get; set; }
}
