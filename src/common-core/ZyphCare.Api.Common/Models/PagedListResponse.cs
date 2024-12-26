using ZyphCare.Common.Models;

namespace ZyphCare.Api.Common.Models;

/// <summary>
/// Represents a response containing a paginated list of items.
/// </summary>
/// <typeparam name="T">The type of the items in the response.</typeparam>
public record PagedListResponse<T>
{
    /// <summary>
    /// Represents a response containing a paginated list of items.
    /// </summary>
    /// <typeparam name="T">The type of the items in the response.</typeparam>
    public PagedListResponse()
    {
    }

    /// <summary>
    /// Represents a response containing a paginated list of items.
    /// </summary>
    /// <typeparam name="T">The type of the items in the response.</typeparam>
    public PagedListResponse(Page<T> page)
    {
        Items = page.Items;
        TotalCount = page.TotalCount;
    }

    /// <summary>
    /// Gets or sets the collection of items in the paginated response.
    /// </summary>
    /// <typeparam name="T">The type of the items in the collection.</typeparam>
    public ICollection<T> Items { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the total number of items across all pages in the paginated response.
    /// </summary>
    public long TotalCount { get; set; }
}