using Musichord.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using Musichord.Services.Interfaces.TrackInterfaces;

namespace Musichord.Services.Repositories;

public class TrackRepository : ITrackRepository
{
    private readonly ApplicationDbContext _db;

    public TrackRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    // STANDARD TRACK METHODS
    public async Task<Track?> ReadTrackBySpotifyIdAsync(string id)
    {
        return await _db.Tracks
                        .Include(t => t.Artist)
                        .FirstOrDefaultAsync(t => t.SpotifyId == id);
    }

    public async Task<Track?> ReadTrackAsync(int id)
    {
        return await _db.Tracks
                        .Include(t => t.Artist)
                        .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task CreateTrack(Track track)
    {
        await _db.Tracks.AddAsync(track);           
        await _db.SaveChangesAsync();
    }

    // LISTEN RECORD (RECENTLY PLAYED) METHODS

    public async Task CreateRecord(ListenRecord record)
    {
        await _db.ListenRecords.AddAsync(record);
        await _db.SaveChangesAsync();
    }

    public async Task<ICollection<ListenRecord>> GetAllRecordExceptByUser(string handle)
    {
        return await _db.ListenRecords.Where(l => l.UserHandle != handle).ToListAsync();
    }

    public async Task<ListenRecord?> ReadListenRecord(int trackId, string userId)
    {
        return await _db.ListenRecords.FirstOrDefaultAsync(r => r.TrackId == trackId && r.UserId == userId);
    }

    // FAVORITE TRACK
    public async Task CreateFavoriteTracksAsync(ICollection<FavoriteTrack> favs)
    {
        await _db.FavoriteTracks.AddRangeAsync(favs);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteFavorites(string userId)
    {
        await _db.FavoriteTracks.Where(ft => ft.UserId == userId).ExecuteDeleteAsync();
    }

    // ALBUM METHODS

    public async Task<Album?> ReadAlbum(string SpotifyId)
    {
        return await _db.Albums.FirstOrDefaultAsync(a => a.SpotifyId == SpotifyId);
    }

    // ARTIST METHODS

    public async Task<Artist?> ReadArtist(string SpotifyArtistId)
    {
        return await _db.Artists.FirstOrDefaultAsync(a => a.SpotifyArtistId == SpotifyArtistId);
    }
}