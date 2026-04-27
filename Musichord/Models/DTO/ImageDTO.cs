using System.Text.Json.Serialization;

namespace Musichord.Models.DTO;

public record ImageDTO
{
    [JsonPropertyName("url")]
    public required string Url { get; init; }
    [JsonPropertyName("height")]
    public int? Height { get; init; }
    [JsonPropertyName("width")]
    public int? Width { get; init; }
}