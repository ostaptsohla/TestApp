using ProtoBuf;

namespace TestApp.Contracts.Models;

[ProtoContract]
public class DiscountPc
{
    [ProtoMember(1)]
    public int Id { get; set; }
    [ProtoMember(2)]
    public decimal TotalPrice { get; set; }
    [ProtoMember(3)]
    public decimal DiscountAmount { get; set; }
}
