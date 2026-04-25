using System.Text.Json.Serialization;

namespace Musichord.Models.DTO;

public record ArtistDTO
{
    [JsonPropertyName("id")]
    public required string ArtistId {get; init;}
    [JsonPropertyName("name")]
    public required string ArtistName {get;init;}
}