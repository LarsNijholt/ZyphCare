using Microsoft.Extensions.DependencyInjection;
using Polly;
using ZyphCare.Api.Client.HttpMessageHandlers;

namespace ZyphCare.Api.Client.Options;

public class ZyphCareClientBuilderOptions
{
    
    public Uri BaseAddress { get; set; } = default!;

    
    public string? ApiKey { get; set; }

    
    public Type AuthenticationHandler { get; set; } = typeof(ApiKeyHttpMessageHandler);

    
    public Action<IServiceProvider, HttpClient>? ConfigureHttpClient { get; set; }

    
    public Action<IHttpClientBuilder> ConfigureHttpClientBuilder { get; set; } = _ => { };

    
    public Action<IHttpClientBuilder>? ConfigureRetryPolicy { get; set; } = builder => builder.AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt))));
}