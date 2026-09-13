using Shipment_Api.Models;

namespace Shipment_Api.Services;

public interface IShipmentService
{
    /// <summary>
    /// Gets a paged result of shipments ordered by shipment ID
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Maximum shipments per page</param>
    /// <returns>
    /// A paged result of shipments ordered by shipment ID
    /// </returns>
    PagedResult<Shipment> GetShipments(int pageNumber, int pageSize);
}
