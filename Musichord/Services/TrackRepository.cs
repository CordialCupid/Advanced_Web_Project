using Musichord.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Musichord.Services;

public class TrackRepository : ITrackRepository
{
    private readonly ApplicationDbContext _db;

    public TrackRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<List<Track>> CreateTracksAsync(List<Track> tracks)
    {
        foreach(Track track in tracks)
        {
            var existing = await _db.Tracks.FirstOrDefaultAsync(a => a.SpotifyId == track.SpotifyId);
            if (existing == null)
            {
                await _db.Tracks.AddAsync(track);           
                await _db.SaveChangesAsync();
                existing = await _db.Tracks.FirstOrDefaultAsync(a => a.SpotifyId == track.SpotifyId);
            }
            track.Id = existing!.Id; 
        }
        return tracks;
    }

    public async Task<Track?> ReadTrackAsync(int id)
    {
        return await _db.Tracks.FindAsync(id);
    }

    public async Task<Track?> ReadTrackBySpotifyIdAsync(string spotId)
    {
        return await _db.Tracks.FirstOrDefaultAsync(t => t.SpotifyId == spotId);
    }
}