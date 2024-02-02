using System.ComponentModel.DataAnnotations;

namespace TestApp.Models;

public class Cart
{
    [Key]
    public int Id { get; set; }
    public ICollection<CartProduct>? CartProducts { get; set; }
}

