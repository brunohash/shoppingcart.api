using System;
namespace ShoppingCart.Models;

public class CartComplete
{
    public string? EntityId { get; set; }

    public string? TokenCart { get; set; }

    public DateTime Created_at { get; set; }

    public int Status { get; set; }

    public IEnumerable<CartItem>? CartItem { get; set; }
}

