namespace ZyphCare.Api.Common.Extensions;

/// <summary>
/// Provides extension methods for working with dictionaries that have object keys and values.
/// </summary>
public static class ObjectDictionaryExtensions
{
    /// <summary>
    /// Retrieves the value associated with the specified key from the dictionary if it exists;
    /// otherwise, uses the provided value factory to create a new value, adds it to the dictionary,
    /// and returns the newly created value.
    /// </summary>
    /// <typeparam name="T">The type of the value to retrieve or create.</typeparam>
    /// <param name="dictionary">The dictionary to retrieve the value from or add the new value to.</param>
    /// <param name="key">The key of the value to retrieve or add.</param>
    /// <param name="valueFactory">The function used to create a new value if the key does not exist in the dictionary.</param>
    /// <returns>The value associated with the specified key, or the newly created value if the key does not exist.</returns>
    public static T GetOrAdd<T>(this IDictionary<object, object> dictionary, object key, Func<T> valueFactory)
    {
        if (dictionary.TryGetValue(key, out var value)) return (T)value;
        value = valueFactory();
        dictionary.Add(key, value!);
        return (T)value!;
    }

    /// <summary>
    /// Retrieves the value associated with the specified key from the dictionary.
    /// </summary>
    /// <typeparam name="T">The type of the value to retrieve.</typeparam>
    /// <param name="dictionary">The dictionary to retrieve the value from.</param>
    /// <param name="key">The key of the value to retrieve.</param>
    /// <returns>The value associated with the specified key.</returns>
    public static T Get<T>(this IDictionary<object, object> dictionary, object key) => (T)dictionary[key];
}