using ZyphCare.Web.Core.Contracts;

namespace ZyphCare.Web.Core.Services;

/// <inheritdoc />
public class DefaultAspectService : IAspectService
{
    private readonly IEnumerable<IAspect> _aspects;

    /// <summary>
    /// Default implementation of the <see cref="IAspectService"/> interface. This class manages and handles
    /// operations for aspects, such as their retrieval and initialization.
    /// </summary>
    public DefaultAspectService(IEnumerable<IAspect> aspects)
    {
        _aspects = aspects;
    }

    /// <inheritdoc />
    public event Action? Initialized;

    /// <inheritdoc />
    public IEnumerable<IAspect> GetAspects()
    {
        return _aspects.ToList();
    }

    /// <inheritdoc />
    public async Task InitializeAspectsAsync(CancellationToken cancellationToken = default)
    {
        foreach (var aspect in GetAspects())
        {
            await aspect.InitializeAsync(cancellationToken);
        }

        OnInitialized();
    }

    private void OnInitialized()
    {
        Initialized?.Invoke();
    }
}