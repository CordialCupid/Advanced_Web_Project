using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
namespace Musichord.Models.Entities;

[Index(nameof(SpotifyId), IsUnique = true)]
public class Track
{
    public int Id {get;set;}
    public string Name {get;set;} = String.Empty;
    public string SpotifyId {get;set;} = String.Empty;
    public int ArtistId {get;set;}
    [JsonIgnore]
    public Artist? Artist {get;set;}
    [JsonIgnore]
    public Album? Album {get;set;}
    public int AlbumId {get;set;}
    [JsonIgnore]
    public ICollection<ListenRecord> Records {get;set;} = new List<ListenRecord>();
}