using MongoDB.Driver.GeoJsonObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CollectionGenerator;

public class GeoJsonPointConverter : JsonConverter<GeoJsonPoint<GeoJson2DCoordinates>>
{
    public override GeoJsonPoint<GeoJson2DCoordinates> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;
            if (root.TryGetProperty("coordinates", out var coordinates))
            {
                var coordArray = coordinates.EnumerateArray();
                var longitude = coordArray.ElementAt(0).GetDouble();
                var latitude = coordArray.ElementAt(1).GetDouble();

                return new GeoJsonPoint<GeoJson2DCoordinates>(new GeoJson2DCoordinates(longitude, latitude));
            }
        }
        return null;
    }

    public override void Write(Utf8JsonWriter writer,
        GeoJsonPoint<GeoJson2DCoordinates> value,
        JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("type", "Point");
        writer.WriteStartArray("coordinates");
        writer.WriteNumberValue(value.Coordinates.X);
        writer.WriteNumberValue(value.Coordinates.Y);
        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}

