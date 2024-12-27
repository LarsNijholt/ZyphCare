using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Refit;
using static ZyphCare.Api.Client.Helpers.RefitSettingsHelper;
using ZyphCare.Api.Client.Options;

namespace ZyphCare.Api.Client;

public static class DependencyInjectionExtensions
{
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
        return services.AddApiClients(configureClient);
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