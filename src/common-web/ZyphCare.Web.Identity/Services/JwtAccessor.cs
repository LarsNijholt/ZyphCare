using Blazored.LocalStorage;
using ZyphCare.Web.Identity.Contracts;

namespace ZyphCare.Web.Identity.Services;

/// <inheritdoc />
public class JwtAccessor : IJwtAccessor
{
    private readonly ILocalStorageService _localStorageService;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="JwtAccessor"/>.
    /// </summary>
    /// <param name="localStorageService">The <see cref="ILocalStorageService"/>.</param>
    public JwtAccessor(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    /// <inheritdoc />
    public async ValueTask<string?> ReadTokenAsync(string name)
    {
        return await _localStorageService.GetItemAsync<string>(name);
    }

    /// <inheritdoc />
    public async ValueTask WriteTokenAsync(string name, string token)
    {
        await _localStorageService.SetItemAsStringAsync(name, token);
    }

    /// <inheritdoc />
    public async ValueTask RemoveTokenAsync(string name)
    {
        await _localStorageService.RemoveItemAsync(name);
    }
}