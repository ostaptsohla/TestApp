using ProtoBuf;

namespace TestApp.Contracts.Models.Requests;

[ProtoContract]
public record GetCartByIdPcRequest
{
    [ProtoMember(1)]
    public int Id { get; set; }
}
