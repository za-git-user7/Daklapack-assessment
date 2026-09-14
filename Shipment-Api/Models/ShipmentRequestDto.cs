using System.ComponentModel.DataAnnotations;

namespace Shipment_Api.Models;

/// <summary>
/// Input query parameters for retrieving a paged list of shipments
/// </summary>
public class ShipmentRequestDto
{
    /// <summary>
    /// Page number of shipments to retrieve
    /// </summary>
    /// <remarks>Default page number is 1</remarks>
    [Range(1, int.MaxValue)]
    public int PageNumber { get; init; } = 1;

    /// <summary>
    /// Maximum shipments to return for a single page
    /// </summary>
    /// <remarks>Default is 10 and maximum is 50</remarks>
    [Range(1, 50)]
    public int PageSize { get; init; } = 10;

    public string? Filter { get; set; }
}
