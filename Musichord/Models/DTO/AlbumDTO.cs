using System.Text.Json.Serialization;

namespace Musichord.Models.DTO;

public record AlbumDTO
{
    [JsonPropertyName("id")]
    public required string AlbumId {get; set;}
    [JsonPropertyName("name")]
    public required string AlbumName {get;set;}
    [JsonPropertyName("images")]
    public required List<ImageDTO> Images {get;set;}
}