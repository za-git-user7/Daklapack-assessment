namespace Shipment_Api.Models;

/// <summary>
/// A single page of results
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; init; } = [];

    public int PageNumber { get; init; }

    /// <summary>
    /// Maximum items per page
    /// </summary>
    public int PageSize { get; init; }

    /// <summary>
    /// Total items for all pages
    /// </summary>
    public int TotalItems { get; init; }

    /// <summary>
    /// Total pages calculated from page size. Returns zero when there are no items
    /// </summary>
    public int TotalPages => PageSize > 0
        ? (int)Math.Ceiling((double)TotalItems / PageSize)
        : 0;
}
