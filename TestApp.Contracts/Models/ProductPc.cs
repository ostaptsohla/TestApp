using ProtoBuf;

namespace TestApp.Contracts.Models;

[ProtoContract]
public class ProductPc
{
    [ProtoMember(1)]
    public int Id  { get; set; }

    [ProtoMember(2)]
    public string Name { get; set; }

    [ProtoMember(3)]
    public string Description { get; set; }

    [ProtoMember(4)]
    public decimal Price { get; set; }
}