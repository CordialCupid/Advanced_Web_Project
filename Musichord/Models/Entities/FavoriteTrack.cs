using Microsoft.EntityFrameworkCore;

namespace Musichord.Models.Entities;

[Index(nameof(TrackId), nameof(UserId), IsUnique = true)]
public class FavoriteTrack
{
    public int Id {get;set;}
    public int TrackId {get;set;}
    public Track? Track {get;set;}
    public string UserId {get;set;} = String.Empty;
    public ApplicationUser? User {get;set;}
}