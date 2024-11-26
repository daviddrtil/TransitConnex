using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CollectionGenerator;

public class DateTimeToUTCConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Read the string and convert it back to DateTime
        var dateString = reader.GetString();

        // Parse the date string as UTC DateTime (assuming input is in ISO 8601 format)
        if (DateTime.TryParse(dateString, out var result))
        {
            return result.ToUniversalTime(); // Ensure UTC
        }

        throw new JsonException("Invalid date format.");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        // Convert the DateTime to the format "yyyy-MM-ddTHH:mm:ssZ"
        string datetime = value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss") + "Z";
        writer.WriteStringValue(datetime);
    }
}

