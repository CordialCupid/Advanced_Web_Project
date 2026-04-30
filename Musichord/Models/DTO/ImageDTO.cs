using System.Text.Json.Serialization;

namespace Musichord.Models.DTO;

public record ImageDTO
{
    [JsonPropertyName("url")]
    public required string Url { get; set; }
    [JsonPropertyName("height")]
    public int? Height { get; set; }
    [JsonPropertyName("width")]
    public int? Width { get; set; }
}