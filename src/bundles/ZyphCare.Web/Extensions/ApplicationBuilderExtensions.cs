using ZyphCare.Web.Core.Contracts;

namespace ZyphCare.Web.Extensions;

/// <summary>
/// Provides extension methods for configuring the application pipeline.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Add custom routing
    /// </summary>
    public static IApplicationBuilder AddRouting(this IApplicationBuilder builder,
        RazorComponentsEndpointConventionBuilder conventionBuilder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        var features = scope.ServiceProvider.GetServices<IAspect>()
            .Select(x => x.GetType().Assembly).Distinct().ToArray();
        conventionBuilder.AddAdditionalAssemblies(features);
        return builder;
    }
}