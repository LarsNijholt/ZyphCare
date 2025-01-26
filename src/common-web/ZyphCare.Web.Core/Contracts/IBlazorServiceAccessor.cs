namespace ZyphCare.Web.Core.Contracts;

/// <summary>
/// Provides access to an <see cref="IServiceProvider"/> that can be set or retrieved
/// to maintain dependencies required within an asynchronous Blazor execution context.
/// </summary>
public interface IBlazorServiceAccessor
{
    /// Gets or sets the current IServiceProvider.
    IServiceProvider Services { get; set; }
}