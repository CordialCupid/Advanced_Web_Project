using System.Text.Json.Serialization;

namespace Musichord.Models.DTO;

public record RecentDTO
{
    [JsonPropertyName("items")]
    public required List<RecentTrackDTO> Tracks {get; set;}
}