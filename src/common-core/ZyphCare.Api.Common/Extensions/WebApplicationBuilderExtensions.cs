using System.Globalization;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;

namespace ZyphCare.Api.Common.Extensions;

/// <summary>
/// Provides extension methods for configuring the WebApplicationBuilder.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Configures the application to use the ZyphCare API with specific endpoint configurations and custom settings.
    /// </summary>
    /// <param name="app">
    /// The application builder instance used to configure the application pipeline.
    /// </param>
    /// <param name="routePrefix">
    /// The route prefix to be applied to all ZyphCare API endpoints. Defaults to "zyphcare/api".
    /// </param>
    /// <returns>
    /// The configured <see cref="IApplicationBuilder"/> instance.
    /// </returns>
    public static IApplicationBuilder UseZyphCareApi(this IApplicationBuilder app, string routePrefix = "zyphcare/api")
    {
        return app.UseFastEndpoints(config =>
        {
            config.Endpoints.RoutePrefix = routePrefix;
            config.Binding.ValueParserFor<DateTimeOffset>(s =>
                new(DateTimeOffset.TryParse(s.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var result), result));
        });
    }
}