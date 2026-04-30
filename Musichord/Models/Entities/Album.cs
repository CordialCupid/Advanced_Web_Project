using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Musichord.Models.Entities;

public class Album
{
    public int Id {get;set;}
    public string Name {get;set;} = String.Empty;
    public string SpotifyId {get;set;} = String.Empty;
    public string ImageUrl {get;set;} = String.Empty;
    public int? Height { get; set; }
    public int? Width { get; set; }
}