using Musichord.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Musichord.Services;

public class DbAlbumRepository : IAlbumRepository
{
    private readonly ApplicationDbContext _db;

    public DbAlbumRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ICollection<Album>> GetRandomAlbumsAsync(int count)
    {
        var albums = await _db.Albums.ToListAsync();
        
        return albums
            .OrderBy(a => Guid.NewGuid())
            .Take(count)
            .ToList();
    }
}