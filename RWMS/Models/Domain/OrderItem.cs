namespace RWMS.Models.Domain;

/// <summary>
/// One line item within an order — a specific product and the quantity ordered.
/// An order can have multiple OrderItems (one per product selected).
/// </summary>
public class OrderItem
{
    public int Id { get; set; }

    // Foreign key — which order this line belongs to
    public int OrderId { get; set; }

    // Foreign key — which product was ordered
    public int ProductId { get; set; }

    // Quantity the client ordered
    public int Quantity { get; set; }

    // Price per unit AT THE TIME of the order — important for financial accuracy.
    // Product.Price may change later; this preserves what was charged.
    public decimal UnitPrice { get; set; }

    // Navigation properties
    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
