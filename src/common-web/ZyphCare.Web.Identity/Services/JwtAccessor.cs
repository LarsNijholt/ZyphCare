using Blazored.LocalStorage;
using Microsoft.AspNetCore.Http;
using ZyphCare.Web.Identity.Contracts;

namespace ZyphCare.Web.Identity.Services;

/// <inheritdoc />
public class JwtAccessor : IJwtAccessor
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtAccessor"/>.
    /// </summary>
    /// <param name="localStorageService">The <see cref="ILocalStorageService"/>.</param>
    /// <param name="httpContextAccessor"></param>
    public JwtAccessor(ILocalStorageService localStorageService, IHttpContextAccessor httpContextAccessor)
    {
        _localStorageService = localStorageService;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc />
    public async ValueTask<string?> ReadTokenAsync(string name)
    {
        if (IsPreRendering())
            return null;
        
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

    private bool IsPreRendering() => _httpContextAccessor.HttpContext?.Response.HasStarted == false;
}