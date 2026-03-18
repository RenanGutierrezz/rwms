namespace RWMS.Models.Enums;

/// <summary>
/// Represents the current state of a wholesale order in the fulfillment workflow.
///
/// Flow:
///   Pending --> Accepted --> ReadyForPickup
///   Pending --> Rejected  (terminal state)
/// </summary>
public enum OrderStatus
{
    Pending,        // Order placed by client, awaiting review
    Accepted,       // Order confirmed by Owner or Manager
    ReadyForPickup, // Order prepared and ready for client
    Rejected        // Order declined by Owner or Manager (terminal)
}
