namespace RWMS.Models.Domain;

/// <summary>
/// An item on the restaurant's internal supply list.
/// Tracks what needs to be purchased to fulfill client orders and maintain stock.
/// Managed by Owner and Manager (FR-11).
/// </summary>
public class SupplyItem
{
    public int Id { get; set; }

    // The name of the supply item (may or may not map to a product)
    public string Name { get; set; } = string.Empty;

    // Optional: links this supply item to a product in the catalog
    // Null if it's a general supply not tied to a specific product
    public int? ProductId { get; set; }

    // How many units need to be purchased
    public int QuantityNeeded { get; set; }

    // How many units have already been ordered from the supplier
    public int QuantityOrdered { get; set; }

    // Unit of measure: "kg", "box", "case" etc.
    public string Unit { get; set; } = string.Empty;

    // Estimated cost per unit for financial tracking (FR-12)
    public decimal? EstimatedUnitCost { get; set; }

    // Optional notes for the person placing the supply order
    public string? Notes { get; set; }

    // Whether this supply item has been fulfilled
    public bool IsFulfilled { get; set; } = false;

    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property — optional link to a product
    public Product? Product { get; set; }
}
