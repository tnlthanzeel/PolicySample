using System.Text.Json;
using System.Text.Json.Serialization;

namespace Facets.SharedKernal.Helpers;

public static class Serializer
{
    private static JsonSerializerOptions _jsonSettings = new JsonSerializerOptions
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };

    public static string Serialize(object instance)
    {
        var serializedData = JsonSerializer.Serialize(instance, _jsonSettings);

        return serializedData;
    }
}

