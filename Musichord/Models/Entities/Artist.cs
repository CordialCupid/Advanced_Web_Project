using Microsoft.EntityFrameworkCore;

namespace Musichord.Models.Entities;

[Index(nameof(SpotifyArtistId), IsUnique = true)]
public class Artist
{
    public int Id {get;set;}
    public string Name {get;set;} = String.Empty;
    public string SpotifyArtistId {get;set;} = String.Empty;
}