using ProtoBuf;

namespace TestApp.Contracts.Models.Requests;

[ProtoContract]
public class DiscountRequest
{
    [ProtoMember(1)]
    public decimal TotalPrice { get; set; }
    [ProtoMember(2)]
    public decimal DiscountAmount { get; set; }
}
