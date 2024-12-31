using ZyphCare.Web.Core.Contracts;

namespace ZyphCare.Web.Core.Services;

/// <inheritdoc />
public class BaseAspect : IAspect
{
    /// <inheritdoc />
    public virtual ValueTask InitializeAsync(CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
}