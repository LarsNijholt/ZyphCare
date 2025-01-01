using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using ZyphCare.Aspects.Contracts;

namespace ZyphCare.Api.Common.Extensions;

/// <summary>
/// Extensions to enable the generation of Swagger API Documentation and associated Swagger UI.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Registers Swagger document generator.
    /// </summary>
    public static IUnit AddSwagger(this IUnit unit)
    {
        Version ver = new(1, 0);

        // Swagger API documentation
        unit.Services.SwaggerDocument(o =>
        {
            o.EnableJWTBearerAuth = true;
            o.DocumentSettings = s =>
            {
                s.DocumentName = $"v{ver.Major}";
                s.Title = "ZyphCare API";
                s.Version = $"v{ver.Major}.{ver.Minor}";
            };
        });

        return unit;
    }

    /// <summary>
    /// Adds middleware to enable the Swagger UI at '/swagger'
    /// </summary>
    public static IApplicationBuilder UseSwaggerUi(this IApplicationBuilder app)
    {
        return app.UseSwaggerGen();
    }

}