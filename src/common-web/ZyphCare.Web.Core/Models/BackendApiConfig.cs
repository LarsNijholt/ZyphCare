using Microsoft.Extensions.DependencyInjection;
using ZyphCare.Api.Client.Options;
using ZyphCare.Web.Core.Options;

namespace ZyphCare.Web.Core.Models;

/// <summary>
/// Represents configuration for interacting with the backend API.
/// </summary>
/// <remarks>
/// This class allows configuration of the HTTP client builder and backend-specific options
/// required for establishing connections and customizing behavior while interacting with
/// the backend API.
/// </remarks>
public class BackendApiConfig
{
    /// <summary>
    /// Gets or sets an action to configure the <see cref="ZyphCareClientBuilderOptions"/> for the HTTP client builder.
    /// </summary>
    /// <remarks>
    /// This property allows customization of the HTTP client builder behavior, such as setting a base address,
    /// adding custom handlers, or defining retry policies for requests made to the backend API.
    /// The provided action is executed during the configuration of the client's <see cref="IHttpClientBuilder"/>.
    /// </remarks>
    public Action<ZyphCareClientBuilderOptions>? ConfigureHttpClientBuilder { get; set; }

    /// <summary>
    /// Gets or sets an action to configure the <see cref="BackendOptions"/> for the backend API configuration.
    /// </summary>
    /// <remarks>
    /// This property enables customization of the backend-specific options, such as setting the backend's base URL
    /// or other configuration parameters necessary for seamless interaction with the backend API.
    /// The provided action is executed during the setup of the backend API connection.
    /// </remarks>
    public Action<BackendOptions>? ConfigureBackendOptions { get; set; }
}