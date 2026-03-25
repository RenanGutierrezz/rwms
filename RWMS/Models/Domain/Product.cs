namespace RWMS.Models.Domain;

/// <summary>
/// A wholesale product the restaurant makes available for clients to order.
/// Managed by Owner and Manager only.
/// </summary>
public class Product
{
    public int Id { get; set; }

    // The display name shown to clients on the product listing page (FR-4)
    public string Name { get; set; } = string.Empty;

    // Optional longer description for the product
    public string? Description { get; set; }

    // Price per unit in CAD
    public decimal Price { get; set; }

    // How many units are currently available (drives low-stock alerts)
    public int StockQuantity { get; set; }

    // Unit of measure: "kg", "box", "case", "each" etc.
    public string Unit { get; set; } = string.Empty;

    // Category for grouping on the product listing page (e.g. "Produce", "Dairy")
    public string? Category { get; set; }

    // Soft delete — we never hard-delete products because past orders reference them
    public bool IsActive { get; set; } = true;
    public DateTime? DeletedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property — all order lines that include this product
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    // Navigation property — all supply entries linked to this product
    public ICollection<SupplyItem> SupplyItems { get; set; } = new List<SupplyItem>();
}
