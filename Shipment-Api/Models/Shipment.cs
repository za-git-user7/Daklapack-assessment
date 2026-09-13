namespace Shipment_Api.Models;

/// <summary>
/// A shipment
/// </summary>
public class Shipment
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public int ShipmentId { get; init; }
    
    /// <summary>
    /// Tracking number assigned to a shipment
    /// </summary>
    public string TrackingId { get; init; } = string.Empty;
    
    /// <summary>
    /// Current status of a shipment
    /// </summary>
    public ShipmentStatus Status { get; init; }
    
    /// <summary>
    /// Shipment weight
    /// </summary>
    public decimal Weight { get; init; }
    
    /// <summary>
    /// Shipment destination
    /// </summary>
    public string Destination { get; init; } = string.Empty;
    
    /// <summary>
    /// Shipment origin
    /// </summary>
    public string Origin { get; init; } = string.Empty;
    
    /// <summary>
    /// Date and time when shipment was created
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }
    
    /// <summary>
    /// Date and time when shipment arrived, or null if not arrived yet
    /// </summary>
    public DateTimeOffset? ArrivedAt { get; init; }
}
