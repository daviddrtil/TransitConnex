using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using TransitConnex.Domain.Collections.Interfaces;

namespace TransitConnex.Domain.Collections;

public abstract class QueryModelBase<TKey> : IQueryModel<TKey> where TKey : IEquatable<TKey>
{
    [BsonId]
    [BsonElement("_id")]
    [JsonPropertyName("_id")]
    [JsonPropertyOrder(-1)]
    public TKey Id { get; set; } = default!;
}
