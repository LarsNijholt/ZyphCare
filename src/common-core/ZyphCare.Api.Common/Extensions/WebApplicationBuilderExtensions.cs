using System.Globalization;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;

namespace ZyphCare.Api.Common.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static IApplicationBuilder UseZyphCareAPI(this IApplicationBuilder app, string routePrefix = "zyphcare/api")
    {
        return app.UseFastEndpoints(config =>
        {
            config.Endpoints.RoutePrefix = routePrefix;
            config.Binding.ValueParserFor<DateTimeOffset>(s =>
                new(DateTimeOffset.TryParse(s.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var result), result));
        });
    }
}