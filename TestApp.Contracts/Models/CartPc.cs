using ProtoBuf;

namespace TestApp.Contracts.Models;

[ProtoContract]
public class CartPc
{
    [ProtoMember(1)]
    public int Id { get; set; }
    [ProtoMember(2)]
    public decimal TotalPrice { get; set; }
    [ProtoMember(3)]
    public ICollection<CartProductPc>? CartProducts { get; set; }
}
