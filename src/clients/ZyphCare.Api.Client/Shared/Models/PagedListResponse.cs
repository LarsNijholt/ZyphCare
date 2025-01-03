namespace ZyphCare.Api.Client.Shared.Models;

/// <summary>
/// Represents a paged response containing a collection of items and the total count of items.
/// </summary>
/// <typeparam name="T">The type of elements in the collection.</typeparam>
public class PagedListResponse<T>
{
    /// <summary>
    /// Gets or sets the collection of items contained in the paged response.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    public ICollection<T> Items { get; set; } = default!;

    /// <summary>
    /// Gets or sets the total count of items in the paged response.
    /// </summary>
    public long TotalCount { get; set; }
}