using System.Text.Json.Serialization;

namespace Musichord.Models.DTO;

public record TopFiveDTO
{
    [JsonPropertyName("items")]
    public required List<TrackDTO> Tracks {get; set;}
}