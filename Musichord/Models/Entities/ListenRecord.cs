using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Musichord.Models.Entities;

[Index(nameof(TrackId), nameof(UserId), IsUnique = true)]
public class ListenRecord
{
    public int Id {get;set;}
    public int TrackId {get;set;}
    [JsonIgnore]
    public Track? Track {get;set;}
    public string UserId {get;set;} = String.Empty;
    
    [JsonIgnore]
    public ApplicationUser? User {get;set;}
}