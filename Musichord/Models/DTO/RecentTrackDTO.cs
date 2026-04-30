using System.Text.Json.Serialization;

namespace Musichord.Models.DTO;

public record RecentTrackDTO
{
    [JsonPropertyName("track")]
    public TrackDTO? Track {get;set;} 
    [JsonPropertyName("played_at")]
    public DateTime Played {get;set;}
}