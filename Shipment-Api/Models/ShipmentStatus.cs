using System.Text.Json.Serialization;

namespace Shipment_Api.Models;

/// <summary>
/// Represents the status of a shipment
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ShipmentStatus
{
    /// <summary>
    /// Shipment in transit
    /// </summary>
    Shipped,

    /// <summary>
    /// Shipment arrived at the destination
    /// </summary>
    Delivered,

    /// <summary>
    /// Shipment cancelled
    /// </summary>
    Cancelled,

    /// <summary>
    /// Shipment ordered but not yet in transit
    /// </summary>
    Ordered
}
