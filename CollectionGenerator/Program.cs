using Bogus;
using MongoDB.Driver.GeoJsonObjectModel;
using System.Text.Json;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.Enums;

namespace CollectionGenerator;

internal class JsonParser
{
    public static string ProjectPath { get; set; } = Path.Combine(
        Environment.CurrentDirectory, "..", "..", "..");
    public static string CollectionPath { get; set; } = Path.Combine(ProjectPath, "ParsedCollections");

    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true, // For pretty-printing
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensures camelCase
        Converters = { new GeoJsonPointConverter(), new DateTimeToUTCConverter() },
    };

    public static void StoreAsJsonFile<T>(List<T> collection)
    {
        string path = Path.Combine(CollectionPath, $"{typeof(T).Name}.json");
        string jsonOutput = JsonSerializer.Serialize(collection, Options);
        File.WriteAllText(path, jsonOutput);
    }
}
