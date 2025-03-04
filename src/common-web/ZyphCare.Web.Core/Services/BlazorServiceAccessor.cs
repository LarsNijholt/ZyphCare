using ZyphCare.Web.Core.Contracts;

namespace ZyphCare.Web.Core.Services;

/// <summary>
/// A Blazor service accessor implementation for managing asynchronous access
/// to an <see cref="IServiceProvider"/> throughout a Blazor application context.
/// This class enables dependency management across execution contexts that support async behavior.
/// </summary>
/// <remarks>
/// This class leverages <see cref="AsyncLocal{T}"/> to ensure that the service provider can be
/// set and retrieved appropriately within the execution contexts of Blazor components or services.
/// </remarks>
public class BlazorServiceAccessor : IBlazorServiceAccessor
{
    private static readonly AsyncLocal<BlazorServiceHolder> CurrentServiceHolder = new();
    
    /// <inheritdoc />
    public IServiceProvider Services
    {
        get => CurrentServiceHolder.Value!.Services!;
        set
        {
            if (CurrentServiceHolder.Value is { } holder)
            {
                // Clear the current IServiceProvider trapped in the AsyncLocal.
                holder.Services = null;
            }
            
            // Use object indirection to hold the IServiceProvider in an AsyncLocal
            // so it can be cleared in all ExecutionContexts when it's cleared.
            CurrentServiceHolder.Value = new() { Services = value };
        }
    }
    
    private sealed class BlazorServiceHolder
    {
        public IServiceProvider? Services { get; set; }
    }
}