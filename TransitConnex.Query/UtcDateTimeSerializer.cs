using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace TransitConnex.Query;

public class UtcDateTimeSerializer : SerializerBase<DateTime>
{
    public override DateTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var bsonDateTime = context.Reader.ReadDateTime(); // Read milliseconds since Unix epoch
        return DateTime.SpecifyKind(DateTimeOffset.FromUnixTimeMilliseconds(bsonDateTime).UtcDateTime, DateTimeKind.Utc);
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTime value)
    {
        var utcValue = value.Kind == DateTimeKind.Utc ? value : value.ToUniversalTime();
        var milliseconds = new DateTimeOffset(utcValue).ToUnixTimeMilliseconds(); // Convert to milliseconds since Unix epoch
        context.Writer.WriteDateTime(milliseconds);
    }
}
