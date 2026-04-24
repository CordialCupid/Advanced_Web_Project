using System.Text.Json.Serialization;

namespace Musichord.Models.DTO;

public record TrackDTO
{
    [JsonPropertyName("id")]
    public required string TrackId {get; init;}
    [JsonPropertyName("name")]
    public required string TrackName {get;init;}
    [JsonPropertyName("artists")]
    public required List<ArtistDTO> Artists {get;init;}
}