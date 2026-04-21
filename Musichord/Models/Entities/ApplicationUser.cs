using Microsoft.AspNetCore.Identity;

namespace Musichord.Models.Entities;

public class ApplicationUser : IdentityUser
{
    public string SpotifyToken { get; set; } = string.Empty;
}