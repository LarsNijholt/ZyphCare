using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Refit;
using ZyphCare.Api.Client.Options;
using ZyphCare.Api.Client.Users.Contracts;
using static ZyphCare.Api.Client.Helpers.RefitSettingsHelper;

namespace ZyphCare.Api.Client.Extensions;

/// <summary>
/// Provides extension methods for integrating ZyphCare API client functionality into the dependency injection container.
/// These methods help configure and register ZyphCare API clients with various customization options.
/// </summary>
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Adds default ZyphCare API clients using an API key for authentication.
    /// </summary>
    /// <param name="services">The dependency injection service collection to which the ZyphCare API clients are added.</param>
    /// <param name="configureOptions">A delegate to configure the <see cref="ZyphCareClientOptions"/> instance.</param>
    /// <returns>The updated service collection with the configured ZyphCare API clients.</returns>
    public static IServiceCollection AddDefaultApiClientsUsingApiKey(this IServiceCollection services, Action<ZyphCareClientOptions> configureOptions)
    {
        var options = new ZyphCareClientOptions();
        configureOptions(options);

        return services.AddDefaultApiClients(client =>
        {
            client.BaseAddress = options.BaseAddress;
            client.ApiKey = options.ApiKey;
            client.ConfigureHttpClient = options.ConfigureHttpClient;
        });
    }

    /// Adds default Elsa API clients.
    public static IServiceCollection AddDefaultApiClients(this IServiceCollection services, Action<ZyphCareClientBuilderOptions>? configureClient = null)
    {
        return services.AddApiClients(configureClient, builderoptions =>
        {
            var builderOptionsWithoutRetryPolicy = new ZyphCareClientBuilderOptions
                {
                    ApiKey = builderoptions.ApiKey,
                    AuthenticationHandler = builderoptions.AuthenticationHandler,
                    BaseAddress = builderoptions.BaseAddress,
                    ConfigureHttpClient = builderoptions.ConfigureHttpClient,
                    ConfigureHttpClientBuilder = builderoptions.ConfigureHttpClientBuilder,
                    ConfigureRetryPolicy = null
                };

            services.AddApi<IUserApi>();
        });
    }

    /// <summary>
    /// Adds an API client to the service collection. Requires AddElsaClient to be called exactly once.
    /// </summary>
    public static IServiceCollection AddApiClient<T>(this IServiceCollection services, Action<ZyphCareClientBuilderOptions>? configureClient = null) where T : class
    {
        return services.AddApiClients(configureClient, builderOptions => services.AddApi<T>(builderOptions));
    }

    /// Adds the Elsa client to the service collection.
    public static IServiceCollection AddApiClients(this IServiceCollection services, Action<ZyphCareClientBuilderOptions>? configureClient = null, Action<ZyphCareClientBuilderOptions>? configureServices = null)
    {
        var builderOptions = new ZyphCareClientBuilderOptions();
        configureClient?.Invoke(builderOptions);
        builderOptions.ConfigureHttpClientBuilder += builder => builder.AddHttpMessageHandler(sp => (DelegatingHandler)sp.GetRequiredService(builderOptions.AuthenticationHandler));

        services.TryAddScoped(builderOptions.AuthenticationHandler);

        services.Configure<ZyphCareClientOptions>(options =>
        {
            options.BaseAddress = builderOptions.BaseAddress;
            options.ConfigureHttpClient = builderOptions.ConfigureHttpClient;
            options.ApiKey = builderOptions.ApiKey;
        });

        configureServices?.Invoke(builderOptions);
        return services;
    }

    /// <summary>
    /// Adds a refit client for the specified API type.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="httpClientBuilderOptions">An options object that can be used to configure the HTTP client builder.</param>
    /// <typeparam name="T">The type representing the API.</typeparam>
    public static IServiceCollection AddApi<T>(this IServiceCollection services, ZyphCareClientBuilderOptions? httpClientBuilderOptions = default) where T : class
    {
        return services.AddApi(typeof(T), httpClientBuilderOptions);
    }

    /// <summary>
    /// Adds a refit client for the specified API type.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="apiType">The type representing the API</param>
    /// <param name="httpClientBuilderOptions">An options object that can be used to configure the HTTP client builder.</param>
    public static IServiceCollection AddApi(this IServiceCollection services, Type apiType, ZyphCareClientBuilderOptions? httpClientBuilderOptions = default)
    {
        var builder = services.AddRefitClient(apiType, _ => CreateRefitSettings(), apiType.Name).ConfigureHttpClient(ConfigureElsaApiHttpClient);
        httpClientBuilderOptions?.ConfigureHttpClientBuilder(builder);
        httpClientBuilderOptions?.ConfigureRetryPolicy?.Invoke(builder);
        return services;
    }

    /// <summary>
    /// Adds a refit client for the specified API type.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="httpClientBuilderOptions">An options object that can be used to configure the HTTP client builder.</param>
    /// <typeparam name="T">The type representing the API.</typeparam>
    public static void AddApiWithoutRetryPolicy<T>(this IServiceCollection services, ZyphCareClientBuilderOptions? httpClientBuilderOptions = default) where T : class
    {
        var builder = services
            .AddRefitClient<T>(_ => CreateRefitSettings(), typeof(T).Name)
            .ConfigureHttpClient(ConfigureElsaApiHttpClient);
        httpClientBuilderOptions?.ConfigureHttpClientBuilder(builder);
    }

    /// Creates an API client for the specified API type.
    public static T CreateApi<T>(this IServiceProvider serviceProvider, Uri baseAddress) where T : class
    {
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(typeof(T).Name);
        httpClient.BaseAddress = baseAddress;
        return CreateApi<T>(serviceProvider, httpClient);
    }

    /// Creates an API client for the specified API type.
    public static T CreateApi<T>(this IServiceProvider serviceProvider, HttpClient httpClient) where T : class
    {
        return RestService.For<T>(httpClient, CreateRefitSettings());
    }

    private static void ConfigureElsaApiHttpClient(IServiceProvider serviceProvider, HttpClient httpClient)
    {
        var options = serviceProvider.GetRequiredService<IOptions<ZyphCareClientOptions>>().Value;
        httpClient.BaseAddress = options.BaseAddress;
        options.ConfigureHttpClient?.Invoke(serviceProvider, httpClient);
    }
}