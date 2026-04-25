using Musichord.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace Musichord.Services;

public class ArtistRepository : IArtistRepository
{
    private readonly ApplicationDbContext _db;

    public ArtistRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task CreateArtistsAsync(List<Artist> artists)
    {

        foreach(Artist artist in artists)
        {    
            var existing = await _db.Artists.FirstOrDefaultAsync(a => a.SpotifyArtistId == artist.SpotifyArtistId);
            if (existing == null)
            {
                _db.Artists.Add(artist);  
                await _db.SaveChangesAsync();
                existing = await _db.Artists.FirstOrDefaultAsync(a => a.SpotifyArtistId == artist.SpotifyArtistId);
            }
            artist.Id = existing!.Id;        
        }
    }

    public async Task<Artist?> ReadArtistAsync(int id)
    {
        return await _db.Artists.FindAsync(id);
    }

    public async Task<Artist?> ReadArtistByNameAsync(string name)
    {
        return await _db.Artists.FirstOrDefaultAsync(a => a.Name == name);
    }
}