using System.Text.Json;
using System.Text.Json.Serialization;
using Refit;

namespace ZyphCare.Api.Client.Helpers;

/// <summary>
/// A helper class for creating and configuring Refit settings and JSON serialization options.
/// </summary>
public static class RefitSettingsHelper
{
    private static JsonSerializerOptions? _jsonSerializerOptions;

    /// <summary>
    /// Creates and returns a <see cref="RefitSettings"/> instance with a custom JSON serializer configuration.
    /// </summary>
    /// <returns>
    /// A configured <see cref="RefitSettings"/> object with a content serializer using JSON settings.
    /// </returns>
    public static RefitSettings CreateRefitSettings()
    {
        var settings = new RefitSettings
            {
                ContentSerializer = new SystemTextJsonContentSerializer(CreateJsonSerializerOptions())
            };

        return settings;
    }


    /// <summary>
    /// Creates and returns a configured <see cref="JsonSerializerOptions"/> instance with specific serialization settings including camel case naming policy and string enumeration conversion.
    /// </summary>
    /// <returns>
    /// A <see cref="JsonSerializerOptions"/> object with custom serialization settings.
    /// </returns>
    public static JsonSerializerOptions CreateJsonSerializerOptions()
    {
        if (_jsonSerializerOptions != null)
            return _jsonSerializerOptions;

        var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

        options.Converters.Add(new JsonStringEnumConverter());

        return _jsonSerializerOptions = options;
    }
}