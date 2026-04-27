using System.Text.Json.Serialization;

namespace Musichord.Models.DTO;

public record AlbumDTO
{
    [JsonPropertyName("id")]
    public required string AlbumId {get; init;}
    [JsonPropertyName("name")]
    public required string AlbumName {get;init;}
    [JsonPropertyName("images")]
    public required List<ImageDTO> Images {get;init;}
}