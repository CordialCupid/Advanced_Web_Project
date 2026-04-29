using Microsoft.EntityFrameworkCore;
namespace Musichord.Models.Entities;

[Index(nameof(SpotifyId), IsUnique = true)]
public class Track
{
    public int Id {get;set;}
    public string Name {get;set;} = String.Empty;
    public string SpotifyId {get;set;} = String.Empty;
    public int ArtistId {get;set;}
    public Artist? Artist {get;set;}
    public string ImageUrl {get;set;} = String.Empty;
    public ICollection<ListenRecord> Records {get;set;} = new List<ListenRecord>();
}