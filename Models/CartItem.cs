namespace ShoppingCart.Models;

public class CartItem
{
    public int Id { get; set; }

    public string? TokenCart { get; set; }

    public int ExternalId { get; set; }

    public string? Name { get; set; }

    public double Price { get; set; }

    public int Amount { get; set; }

    public string? Variable { get; set; }
}

