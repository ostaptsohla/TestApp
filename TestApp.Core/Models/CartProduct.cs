using System.ComponentModel.DataAnnotations;

namespace TestApp.Models;

public class CartProduct
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
}
