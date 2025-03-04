using System.Reflection;
using ZyphCare.Web.Core.Contracts;

namespace ZyphCare.Web.Core.Services;

/// <summary>
/// Represents a dynamic proxy implementation that facilitates scoped dependency management
/// in a Blazor environment for a given API interface.
/// </summary>
/// <typeparam name="T">The type of the API interface.</typeparam>
/// <remarks>
/// This class is designed to work with DispatchProxy to enable dependency injection
/// for Blazor components and services during asynchronous operations, ensuring that
/// services adhere to the scoped lifecycle in Blazor.
/// </remarks>
public class BlazorScopedProxyApi<T> : DispatchProxy
{
    private T _decoratedApi = default!;
    private IBlazorServiceAccessor _blazorServiceAccessor = default!;
    private IServiceProvider _serviceProvider = default!;

    internal void Initialize(T decoratedApi, IBlazorServiceAccessor blazorServiceAccessor, IServiceProvider serviceProvider)
    {
        _decoratedApi = decoratedApi;
        _blazorServiceAccessor = blazorServiceAccessor;
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
    {
        _blazorServiceAccessor.Services = _serviceProvider;
        return targetMethod?.Invoke(_decoratedApi, args);
    }
}