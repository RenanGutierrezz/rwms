using RWMS.Models.Enums;

namespace RWMS.Models.Domain;

/// <summary>
/// A wholesale order placed by a client.
/// Clients create orders. Owners and Managers manage and fulfill them.
/// </summary>
public class Order
{
    public int Id { get; set; }

    // Foreign key — links this order to the client who placed it (ApplicationUser)
    public string ClientId { get; set; } = string.Empty;

    // Current status of the order in the fulfillment workflow
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    // The date the client requested for delivery or pickup (FR-6)
    public DateTime? RequestedDeliveryDate { get; set; }

    // Optional note the client can leave when placing the order
    public string? Notes { get; set; }

    // Calculated total — stored so financial reports (FR-12) don't need to recalculate
    public decimal TotalAmount { get; set; }

    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Soft delete — orders are never hard-deleted (financial and history record)
    public bool IsActive { get; set; } = true;

    // Navigation property — the client who placed this order
    public ApplicationUser Client { get; set; } = null!;

    // Navigation property — all line items in this order
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
