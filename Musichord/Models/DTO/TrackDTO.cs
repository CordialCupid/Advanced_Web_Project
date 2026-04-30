using System.Text.Json.Serialization;

namespace Musichord.Models.DTO;

public record TrackDTO
{
    [JsonPropertyName("id")]
    public required string TrackId {get; set;}
    [JsonPropertyName("name")]
    public required string TrackName {get;set;}
    [JsonPropertyName("artists")]
    public required List<ArtistDTO> Artists {get;set;}
    [JsonPropertyName("album")]
    public required AlbumDTO Album {get;set;}
}