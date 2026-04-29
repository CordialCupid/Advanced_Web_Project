using Microsoft.EntityFrameworkCore;

namespace Musichord.Models.Entities;

public class Review
{
    public int Id {get;set;}
    public string Message {get;set;} = String.Empty;
    public int Rating {get;set;}
    public int AlbumId {get;set;}
    public Album? AlbumReview {get;set;}
    public string UserId {get;set;} = String.Empty;
    public ApplicationUser? User {get;set;}
}