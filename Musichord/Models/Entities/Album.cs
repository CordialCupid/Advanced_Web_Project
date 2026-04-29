using Microsoft.EntityFrameworkCore;

namespace Musichord.Models.Entities;

public class Album
{
    public int Id {get;set;}
    public string Name {get;set;} = String.Empty;
    public int ArtistId {get;set;}
    public Artist? Creator {get;set;}
    public ICollection<Review> Reviews {get;set;} = new List<Review>();
}