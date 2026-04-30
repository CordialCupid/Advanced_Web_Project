using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Musichord.Models.Entities;

public class ApplicationUser : IdentityUser
{
    public string SpotifyToken { get; set; } = String.Empty;
    // unique handle per user
    public string Handle { get; set; } = String.Empty;
    [JsonIgnore]
    public ICollection<FavoriteTrack> FavoriteTracks {get;set;} = new List<FavoriteTrack>();
    public string ProfilePicture { get; set; } = "";
    public string SpotEmail {get;set;} = String.Empty;
    public string SpotUser {get;set;} = String.Empty;
    [JsonIgnore]
    public ICollection<ListenRecord> Records {get;set;} = new List<ListenRecord>();
    [JsonIgnore]
    public ICollection<Review> Reviews {get;set;} = new List<Review>();
}