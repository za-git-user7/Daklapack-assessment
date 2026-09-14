using Shipment_Api.Models;

namespace Shipment_Api.Services;

public class ShipmentService : IShipmentService
{
    /// <summary>
    /// Gets a paged result of shipments ordered by shipment ID
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Maximum shipments per page</param>
    /// <param name="filter">Value to filter shipments by</param>
    /// <returns>
    /// A paged result of shipments ordered by shipment ID
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="pageNumber"/> or <paramref name="pageSize"/> is less than 1
    /// </exception>
    public PagedResult<Shipment> GetShipments(int pageNumber, int pageSize, string? filter)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(pageNumber, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1);

        IEnumerable<Shipment> filteredShipments = Shipments;

        if (!string.IsNullOrEmpty(filter))
        {
            filter = filter.Trim();

            filteredShipments = filteredShipments
                            .Where(s => s.TrackingId.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                                s.Status.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                                s.Origin.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                                s.Destination.Contains(filter, StringComparison.OrdinalIgnoreCase));
        }

        var totalItems = filteredShipments.Count();

        var shipments = filteredShipments
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

        var pagedShipments = new PagedResult<Shipment>
        {
            Items = shipments,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = totalItems
        };

        return pagedShipments;
    }

    /// <summary>
    /// AI-generated in-memory sample shipment data
    /// </summary>
    private static readonly IReadOnlyList<Shipment> Shipments = new List<Shipment>
    {
        new() { ShipmentId = 1, TrackingId = "TRK-000001", Status = ShipmentStatus.Delivered, Weight = 2.5m, Destination = "Cape Town", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 1, 5, 9, 30, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 1, 8, 14, 20, 0, TimeSpan.Zero) },
        new() { ShipmentId = 2, TrackingId = "TRK-000002", Status = ShipmentStatus.Shipped, Weight = 5.2m, Destination = "Durban", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 1, 7, 10, 15, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 3, TrackingId = "TRK-000003", Status = ShipmentStatus.Ordered, Weight = 1.8m, Destination = "Pretoria", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 1, 9, 8, 45, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 4, TrackingId = "TRK-000004", Status = ShipmentStatus.Delivered, Weight = 12.4m, Destination = "Port Elizabeth", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 1, 10, 11, 0, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 1, 14, 16, 30, 0, TimeSpan.Zero) },
        new() { ShipmentId = 5, TrackingId = "TRK-000005", Status = ShipmentStatus.Cancelled, Weight = 3.1m, Destination = "Bloemfontein", Origin = "Durban", CreatedAt = new DateTimeOffset(2026, 1, 12, 13, 20, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 6, TrackingId = "TRK-000006", Status = ShipmentStatus.Shipped, Weight = 7.6m, Destination = "Cape Town", Origin = "Pretoria", CreatedAt = new DateTimeOffset(2026, 1, 14, 7, 50, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 7, TrackingId = "TRK-000007", Status = ShipmentStatus.Delivered, Weight = 4.3m, Destination = "Johannesburg", Origin = "Durban", CreatedAt = new DateTimeOffset(2026, 1, 15, 15, 10, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 1, 18, 12, 15, 0, TimeSpan.Zero) },
        new() { ShipmentId = 8, TrackingId = "TRK-000008", Status = ShipmentStatus.Ordered, Weight = 9.7m, Destination = "Kimberley", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 1, 17, 9, 0, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 9, TrackingId = "TRK-000009", Status = ShipmentStatus.Delivered, Weight = 0.9m, Destination = "George", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 1, 18, 10, 30, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 1, 20, 11, 45, 0, TimeSpan.Zero) },
        new() { ShipmentId = 10, TrackingId = "TRK-000010", Status = ShipmentStatus.Shipped, Weight = 15.8m, Destination = "East London", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 1, 20, 14, 40, 0, TimeSpan.Zero), ArrivedAt = null },

        new() { ShipmentId = 11, TrackingId = "TRK-000011", Status = ShipmentStatus.Delivered, Weight = 6.2m, Destination = "Durban", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 1, 22, 8, 10, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 1, 25, 15, 20, 0, TimeSpan.Zero) },
        new() { ShipmentId = 12, TrackingId = "TRK-000012", Status = ShipmentStatus.Ordered, Weight = 2.7m, Destination = "Cape Town", Origin = "Bloemfontein", CreatedAt = new DateTimeOffset(2026, 1, 23, 12, 0, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 13, TrackingId = "TRK-000013", Status = ShipmentStatus.Shipped, Weight = 11.5m, Destination = "Pretoria", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 1, 25, 9, 25, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 14, TrackingId = "TRK-000014", Status = ShipmentStatus.Delivered, Weight = 3.6m, Destination = "Polokwane", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 1, 27, 16, 0, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 1, 30, 10, 40, 0, TimeSpan.Zero) },
        new() { ShipmentId = 15, TrackingId = "TRK-000015", Status = ShipmentStatus.Cancelled, Weight = 8.4m, Destination = "Nelspruit", Origin = "Pretoria", CreatedAt = new DateTimeOffset(2026, 1, 29, 11, 35, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 16, TrackingId = "TRK-000016", Status = ShipmentStatus.Shipped, Weight = 5.9m, Destination = "George", Origin = "Durban", CreatedAt = new DateTimeOffset(2026, 2, 1, 7, 20, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 17, TrackingId = "TRK-000017", Status = ShipmentStatus.Delivered, Weight = 1.4m, Destination = "Cape Town", Origin = "George", CreatedAt = new DateTimeOffset(2026, 2, 2, 13, 15, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 2, 4, 9, 30, 0, TimeSpan.Zero) },
        new() { ShipmentId = 18, TrackingId = "TRK-000018", Status = ShipmentStatus.Ordered, Weight = 18.2m, Destination = "Durban", Origin = "East London", CreatedAt = new DateTimeOffset(2026, 2, 4, 10, 45, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 19, TrackingId = "TRK-000019", Status = ShipmentStatus.Delivered, Weight = 7.3m, Destination = "Johannesburg", Origin = "Port Elizabeth", CreatedAt = new DateTimeOffset(2026, 2, 6, 8, 30, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 2, 9, 14, 10, 0, TimeSpan.Zero) },
        new() { ShipmentId = 20, TrackingId = "TRK-000020", Status = ShipmentStatus.Shipped, Weight = 4.8m, Destination = "Cape Town", Origin = "Kimberley", CreatedAt = new DateTimeOffset(2026, 2, 8, 15, 25, 0, TimeSpan.Zero), ArrivedAt = null },

        new() { ShipmentId = 21, TrackingId = "TRK-000021", Status = ShipmentStatus.Ordered, Weight = 2.2m, Destination = "Durban", Origin = "Pretoria", CreatedAt = new DateTimeOffset(2026, 2, 10, 9, 10, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 22, TrackingId = "TRK-000022", Status = ShipmentStatus.Delivered, Weight = 10.6m, Destination = "Cape Town", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 2, 11, 11, 40, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 2, 15, 13, 25, 0, TimeSpan.Zero) },
        new() { ShipmentId = 23, TrackingId = "TRK-000023", Status = ShipmentStatus.Shipped, Weight = 6.7m, Destination = "Bloemfontein", Origin = "Durban", CreatedAt = new DateTimeOffset(2026, 2, 13, 14, 50, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 24, TrackingId = "TRK-000024", Status = ShipmentStatus.Delivered, Weight = 3.9m, Destination = "Pretoria", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 2, 15, 8, 5, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 2, 18, 16, 15, 0, TimeSpan.Zero) },
        new() { ShipmentId = 25, TrackingId = "TRK-000025", Status = ShipmentStatus.Cancelled, Weight = 14.1m, Destination = "Durban", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 2, 17, 12, 30, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 26, TrackingId = "TRK-000026", Status = ShipmentStatus.Shipped, Weight = 8.8m, Destination = "Port Elizabeth", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 2, 19, 10, 20, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 27, TrackingId = "TRK-000027", Status = ShipmentStatus.Delivered, Weight = 5.5m, Destination = "George", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 2, 21, 7, 45, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 2, 24, 12, 50, 0, TimeSpan.Zero) },
        new() { ShipmentId = 28, TrackingId = "TRK-000028", Status = ShipmentStatus.Ordered, Weight = 1.1m, Destination = "Kimberley", Origin = "Bloemfontein", CreatedAt = new DateTimeOffset(2026, 2, 22, 16, 10, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 29, TrackingId = "TRK-000029", Status = ShipmentStatus.Shipped, Weight = 13.5m, Destination = "Cape Town", Origin = "East London", CreatedAt = new DateTimeOffset(2026, 2, 24, 9, 35, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 30, TrackingId = "TRK-000030", Status = ShipmentStatus.Delivered, Weight = 9.2m, Destination = "Johannesburg", Origin = "Polokwane", CreatedAt = new DateTimeOffset(2026, 2, 26, 13, 55, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 3, 1, 10, 20, 0, TimeSpan.Zero) },

        new() { ShipmentId = 31, TrackingId = "TRK-000031", Status = ShipmentStatus.Ordered, Weight = 4.6m, Destination = "Pretoria", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 3, 1, 8, 40, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 32, TrackingId = "TRK-000032", Status = ShipmentStatus.Delivered, Weight = 7.9m, Destination = "Durban", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 3, 3, 11, 15, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 3, 6, 15, 45, 0, TimeSpan.Zero) },
        new() { ShipmentId = 33, TrackingId = "TRK-000033", Status = ShipmentStatus.Shipped, Weight = 2.8m, Destination = "Cape Town", Origin = "Pretoria", CreatedAt = new DateTimeOffset(2026, 3, 5, 14, 25, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 34, TrackingId = "TRK-000034", Status = ShipmentStatus.Delivered, Weight = 16.3m, Destination = "Bloemfontein", Origin = "Durban", CreatedAt = new DateTimeOffset(2026, 3, 7, 9, 50, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 3, 11, 11, 35, 0, TimeSpan.Zero) },
        new() { ShipmentId = 35, TrackingId = "TRK-000035", Status = ShipmentStatus.Cancelled, Weight = 3.4m, Destination = "George", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 3, 9, 12, 5, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 36, TrackingId = "TRK-000036", Status = ShipmentStatus.Shipped, Weight = 6.1m, Destination = "Johannesburg", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 3, 11, 7, 35, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 37, TrackingId = "TRK-000037", Status = ShipmentStatus.Delivered, Weight = 11.8m, Destination = "Durban", Origin = "Port Elizabeth", CreatedAt = new DateTimeOffset(2026, 3, 13, 15, 20, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 3, 17, 13, 10, 0, TimeSpan.Zero) },
        new() { ShipmentId = 38, TrackingId = "TRK-000038", Status = ShipmentStatus.Ordered, Weight = 2.6m, Destination = "East London", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 3, 15, 10, 10, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 39, TrackingId = "TRK-000039", Status = ShipmentStatus.Shipped, Weight = 9.5m, Destination = "Pretoria", Origin = "Kimberley", CreatedAt = new DateTimeOffset(2026, 3, 17, 13, 45, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 40, TrackingId = "TRK-000040", Status = ShipmentStatus.Delivered, Weight = 5.7m, Destination = "Cape Town", Origin = "George", CreatedAt = new DateTimeOffset(2026, 3, 19, 8, 25, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 3, 22, 14, 40, 0, TimeSpan.Zero) },

        new() { ShipmentId = 41, TrackingId = "TRK-000041", Status = ShipmentStatus.Ordered, Weight = 1.7m, Destination = "Johannesburg", Origin = "Durban", CreatedAt = new DateTimeOffset(2026, 3, 21, 11, 30, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 42, TrackingId = "TRK-000042", Status = ShipmentStatus.Delivered, Weight = 8.3m, Destination = "Cape Town", Origin = "Pretoria", CreatedAt = new DateTimeOffset(2026, 3, 23, 9, 15, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 3, 27, 16, 20, 0, TimeSpan.Zero) },
        new() { ShipmentId = 43, TrackingId = "TRK-000043", Status = ShipmentStatus.Shipped, Weight = 12.7m, Destination = "Durban", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 3, 25, 14, 0, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 44, TrackingId = "TRK-000044", Status = ShipmentStatus.Delivered, Weight = 4.2m, Destination = "Bloemfontein", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 3, 27, 7, 55, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 3, 30, 12, 30, 0, TimeSpan.Zero) },
        new() { ShipmentId = 45, TrackingId = "TRK-000045", Status = ShipmentStatus.Cancelled, Weight = 6.8m, Destination = "Pretoria", Origin = "Durban", CreatedAt = new DateTimeOffset(2026, 3, 29, 16, 40, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 46, TrackingId = "TRK-000046", Status = ShipmentStatus.Shipped, Weight = 3.5m, Destination = "George", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 4, 1, 10, 5, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 47, TrackingId = "TRK-000047", Status = ShipmentStatus.Delivered, Weight = 14.6m, Destination = "Johannesburg", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 4, 3, 12, 25, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 4, 7, 15, 55, 0, TimeSpan.Zero) },
        new() { ShipmentId = 48, TrackingId = "TRK-000048", Status = ShipmentStatus.Ordered, Weight = 7.1m, Destination = "East London", Origin = "Durban", CreatedAt = new DateTimeOffset(2026, 4, 5, 8, 35, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 49, TrackingId = "TRK-000049", Status = ShipmentStatus.Shipped, Weight = 2.3m, Destination = "Cape Town", Origin = "Bloemfontein", CreatedAt = new DateTimeOffset(2026, 4, 7, 13, 10, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 50, TrackingId = "TRK-000050", Status = ShipmentStatus.Delivered, Weight = 9.9m, Destination = "Durban", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 4, 9, 11, 45, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 4, 13, 10, 15, 0, TimeSpan.Zero) },

        new() { ShipmentId = 51, TrackingId = "TRK-000051", Status = ShipmentStatus.Ordered, Weight = 5.4m, Destination = "Pretoria", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 4, 11, 9, 20, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 52, TrackingId = "TRK-000052", Status = ShipmentStatus.Delivered, Weight = 1.9m, Destination = "Cape Town", Origin = "George", CreatedAt = new DateTimeOffset(2026, 4, 13, 14, 35, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 4, 15, 11, 50, 0, TimeSpan.Zero) },
        new() { ShipmentId = 53, TrackingId = "TRK-000053", Status = ShipmentStatus.Shipped, Weight = 10.2m, Destination = "Johannesburg", Origin = "Durban", CreatedAt = new DateTimeOffset(2026, 4, 15, 7, 40, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 54, TrackingId = "TRK-000054", Status = ShipmentStatus.Delivered, Weight = 6.5m, Destination = "Bloemfontein", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 4, 17, 15, 5, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 4, 21, 13, 45, 0, TimeSpan.Zero) },
        new() { ShipmentId = 55, TrackingId = "TRK-000055", Status = ShipmentStatus.Cancelled, Weight = 3.8m, Destination = "Kimberley", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 4, 19, 10, 50, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 56, TrackingId = "TRK-000056", Status = ShipmentStatus.Shipped, Weight = 13.9m, Destination = "Durban", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 4, 21, 12, 15, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 57, TrackingId = "TRK-000057", Status = ShipmentStatus.Delivered, Weight = 4.7m, Destination = "Port Elizabeth", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 4, 23, 8, 0, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 4, 27, 14, 25, 0, TimeSpan.Zero) },
        new() { ShipmentId = 58, TrackingId = "TRK-000058", Status = ShipmentStatus.Ordered, Weight = 8.1m, Destination = "Cape Town", Origin = "Pretoria", CreatedAt = new DateTimeOffset(2026, 4, 25, 16, 30, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 59, TrackingId = "TRK-000059", Status = ShipmentStatus.Shipped, Weight = 2.5m, Destination = "George", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 4, 27, 9, 45, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 60, TrackingId = "TRK-000060", Status = ShipmentStatus.Delivered, Weight = 17.4m, Destination = "Johannesburg", Origin = "Durban", CreatedAt = new DateTimeOffset(2026, 4, 29, 13, 25, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 5, 3, 16, 10, 0, TimeSpan.Zero) },

        new() { ShipmentId = 61, TrackingId = "TRK-000061", Status = ShipmentStatus.Ordered, Weight = 6.9m, Destination = "Pretoria", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 5, 1, 10, 10, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 62, TrackingId = "TRK-000062", Status = ShipmentStatus.Delivered, Weight = 3.2m, Destination = "Durban", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 5, 3, 7, 30, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 5, 6, 12, 40, 0, TimeSpan.Zero) },
        new() { ShipmentId = 63, TrackingId = "TRK-000063", Status = ShipmentStatus.Shipped, Weight = 11.1m, Destination = "Cape Town", Origin = "East London", CreatedAt = new DateTimeOffset(2026, 5, 5, 14, 20, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 64, TrackingId = "TRK-000064", Status = ShipmentStatus.Delivered, Weight = 5.6m, Destination = "Bloemfontein", Origin = "Durban", CreatedAt = new DateTimeOffset(2026, 5, 7, 11, 55, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 5, 10, 15, 30, 0, TimeSpan.Zero) },
        new() { ShipmentId = 65, TrackingId = "TRK-000065", Status = ShipmentStatus.Cancelled, Weight = 9.4m, Destination = "Cape Town", Origin = "Pretoria", CreatedAt = new DateTimeOffset(2026, 5, 9, 8, 15, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 66, TrackingId = "TRK-000066", Status = ShipmentStatus.Shipped, Weight = 1.6m, Destination = "Johannesburg", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 5, 11, 16, 45, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 67, TrackingId = "TRK-000067", Status = ShipmentStatus.Delivered, Weight = 7.8m, Destination = "George", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 5, 13, 9, 35, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 5, 16, 13, 20, 0, TimeSpan.Zero) },
        new() { ShipmentId = 68, TrackingId = "TRK-000068", Status = ShipmentStatus.Ordered, Weight = 12.6m, Destination = "Durban", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 5, 15, 12, 10, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 69, TrackingId = "TRK-000069", Status = ShipmentStatus.Shipped, Weight = 4.4m, Destination = "Pretoria", Origin = "Bloemfontein", CreatedAt = new DateTimeOffset(2026, 5, 17, 7, 50, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 70, TrackingId = "TRK-000070", Status = ShipmentStatus.Delivered, Weight = 15.2m, Destination = "Cape Town", Origin = "Durban", CreatedAt = new DateTimeOffset(2026, 5, 19, 14, 5, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 5, 23, 11, 15, 0, TimeSpan.Zero) },

        new() { ShipmentId = 71, TrackingId = "TRK-000071", Status = ShipmentStatus.Ordered, Weight = 2.1m, Destination = "Johannesburg", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 5, 21, 10, 25, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 72, TrackingId = "TRK-000072", Status = ShipmentStatus.Delivered, Weight = 8.7m, Destination = "Durban", Origin = "Pretoria", CreatedAt = new DateTimeOffset(2026, 5, 23, 15, 40, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 5, 27, 14, 5, 0, TimeSpan.Zero) },
        new() { ShipmentId = 73, TrackingId = "TRK-000073", Status = ShipmentStatus.Shipped, Weight = 5.3m, Destination = "Cape Town", Origin = "George", CreatedAt = new DateTimeOffset(2026, 5, 25, 8, 20, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 74, TrackingId = "TRK-000074", Status = ShipmentStatus.Delivered, Weight = 10.9m, Destination = "Bloemfontein", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 5, 27, 13, 35, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 5, 31, 16, 25, 0, TimeSpan.Zero) },
        new() { ShipmentId = 75, TrackingId = "TRK-000075", Status = ShipmentStatus.Cancelled, Weight = 3.7m, Destination = "Port Elizabeth", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 5, 29, 11, 5, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 76, TrackingId = "TRK-000076", Status = ShipmentStatus.Shipped, Weight = 6.4m, Destination = "Johannesburg", Origin = "Durban", CreatedAt = new DateTimeOffset(2026, 6, 1, 9, 45, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 77, TrackingId = "TRK-000077", Status = ShipmentStatus.Delivered, Weight = 14.8m, Destination = "Cape Town", Origin = "Pretoria", CreatedAt = new DateTimeOffset(2026, 6, 3, 16, 15, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 6, 7, 12, 55, 0, TimeSpan.Zero) },
        new() { ShipmentId = 78, TrackingId = "TRK-000078", Status = ShipmentStatus.Ordered, Weight = 1.3m, Destination = "George", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 6, 5, 7, 25, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 79, TrackingId = "TRK-000079", Status = ShipmentStatus.Shipped, Weight = 9.6m, Destination = "Durban", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 6, 7, 12, 50, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 80, TrackingId = "TRK-000080", Status = ShipmentStatus.Delivered, Weight = 4.9m, Destination = "Pretoria", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 6, 9, 10, 35, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 6, 12, 15, 10, 0, TimeSpan.Zero) },

        new() { ShipmentId = 81, TrackingId = "TRK-000081", Status = ShipmentStatus.Ordered, Weight = 7.2m, Destination = "Cape Town", Origin = "Durban", CreatedAt = new DateTimeOffset(2026, 6, 11, 14, 15, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 82, TrackingId = "TRK-000082", Status = ShipmentStatus.Delivered, Weight = 2.4m, Destination = "Johannesburg", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 6, 13, 8, 55, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 6, 16, 11, 35, 0, TimeSpan.Zero) },
        new() { ShipmentId = 83, TrackingId = "TRK-000083", Status = ShipmentStatus.Shipped, Weight = 13.2m, Destination = "Durban", Origin = "Bloemfontein", CreatedAt = new DateTimeOffset(2026, 6, 15, 11, 20, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 84, TrackingId = "TRK-000084", Status = ShipmentStatus.Delivered, Weight = 5.8m, Destination = "Cape Town", Origin = "George", CreatedAt = new DateTimeOffset(2026, 6, 17, 15, 45, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 6, 20, 13, 30, 0, TimeSpan.Zero) },
        new() { ShipmentId = 85, TrackingId = "TRK-000085", Status = ShipmentStatus.Cancelled, Weight = 11.7m, Destination = "Pretoria", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 6, 19, 9, 5, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 86, TrackingId = "TRK-000086", Status = ShipmentStatus.Shipped, Weight = 3.3m, Destination = "George", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 6, 21, 13, 40, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 87, TrackingId = "TRK-000087", Status = ShipmentStatus.Delivered, Weight = 8.5m, Destination = "Durban", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 6, 23, 7, 35, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 6, 27, 16, 45, 0, TimeSpan.Zero) },
        new() { ShipmentId = 88, TrackingId = "TRK-000088", Status = ShipmentStatus.Ordered, Weight = 6.6m, Destination = "Bloemfontein", Origin = "Pretoria", CreatedAt = new DateTimeOffset(2026, 6, 25, 12, 25, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 89, TrackingId = "TRK-000089", Status = ShipmentStatus.Shipped, Weight = 10.4m, Destination = "Cape Town", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 6, 27, 10, 50, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 90, TrackingId = "TRK-000090", Status = ShipmentStatus.Delivered, Weight = 1.8m, Destination = "Port Elizabeth", Origin = "Durban", CreatedAt = new DateTimeOffset(2026, 6, 29, 14, 30, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 7, 2, 12, 20, 0, TimeSpan.Zero) },

        new() { ShipmentId = 91, TrackingId = "TRK-000091", Status = ShipmentStatus.Ordered, Weight = 4.1m, Destination = "Johannesburg", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 7, 1, 8, 15, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 92, TrackingId = "TRK-000092", Status = ShipmentStatus.Delivered, Weight = 9.3m, Destination = "Durban", Origin = "Pretoria", CreatedAt = new DateTimeOffset(2026, 7, 3, 11, 50, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 7, 7, 14, 35, 0, TimeSpan.Zero) },
        new() { ShipmentId = 93, TrackingId = "TRK-000093", Status = ShipmentStatus.Shipped, Weight = 7.5m, Destination = "Cape Town", Origin = "East London", CreatedAt = new DateTimeOffset(2026, 7, 5, 16, 5, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 94, TrackingId = "TRK-000094", Status = ShipmentStatus.Delivered, Weight = 12.9m, Destination = "Pretoria", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 7, 7, 9, 25, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 7, 11, 11, 45, 0, TimeSpan.Zero) },
        new() { ShipmentId = 95, TrackingId = "TRK-000095", Status = ShipmentStatus.Cancelled, Weight = 2.9m, Destination = "Cape Town", Origin = "Durban", CreatedAt = new DateTimeOffset(2026, 7, 9, 13, 15, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 96, TrackingId = "TRK-000096", Status = ShipmentStatus.Shipped, Weight = 5.1m, Destination = "Johannesburg", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 7, 11, 7, 45, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 97, TrackingId = "TRK-000097", Status = ShipmentStatus.Delivered, Weight = 16.8m, Destination = "Durban", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 7, 13, 15, 35, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 7, 17, 13, 15, 0, TimeSpan.Zero) },
        new() { ShipmentId = 98, TrackingId = "TRK-000098", Status = ShipmentStatus.Ordered, Weight = 3.5m, Destination = "George", Origin = "Cape Town", CreatedAt = new DateTimeOffset(2026, 7, 15, 10, 20, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 99, TrackingId = "TRK-000099", Status = ShipmentStatus.Shipped, Weight = 8.9m, Destination = "Pretoria", Origin = "Durban", CreatedAt = new DateTimeOffset(2026, 7, 17, 12, 40, 0, TimeSpan.Zero), ArrivedAt = null },
        new() { ShipmentId = 100, TrackingId = "TRK-000100", Status = ShipmentStatus.Delivered, Weight = 6.3m, Destination = "Cape Town", Origin = "Johannesburg", CreatedAt = new DateTimeOffset(2026, 7, 19, 9, 10, 0, TimeSpan.Zero), ArrivedAt = new DateTimeOffset(2026, 7, 23, 15, 50, 0, TimeSpan.Zero) }
    };
}
