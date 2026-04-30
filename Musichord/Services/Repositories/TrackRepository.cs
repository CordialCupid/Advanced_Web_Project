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

    // STANDARD TRACK METHODS
    public async Task<List<Track>> CreateTracksAsync(List<Track> tracks)
    {

        foreach(Track track in tracks)
        {
            if (track.Artist != null)
            {
                var artist = await _db.Artists.FirstOrDefaultAsync( a => a.SpotifyArtistId == track.Artist.SpotifyArtistId);
                if (artist != null)
                {
                    track.Artist = artist;
                }          
            }

            if (track.Album != null)
            {
                var album = await _db.Albums.FirstOrDefaultAsync(a => a.SpotifyId == track.Album.SpotifyId);
                if (album != null)
                {

                    track.Album = album;
                }            
            }

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
        return await _db.Tracks
                        .Include(t => t.Artist)
                        .FirstOrDefaultAsync(t => t.Id == id);
    }

    // LISTEN RECORD (RECENTLY PLAYED) METHODS

    public async Task CreateRecord(ListenRecord record)
    {
        await _db.ListenRecords.AddAsync(record);
        await _db.SaveChangesAsync();
    }

    public async Task<List<ListenRecord>> CreateListenRecords(string userId, List<Track> tracks)
    {
        List<ListenRecord> records = new List<ListenRecord>();
        var newTracks = await CreateTracksAsync(tracks);
        records = newTracks.Select(t => new ListenRecord
        {
            Id = 0,
            TrackId = t.Id,
            UserId = userId
        }).ToList();

        
        if (records.Count() > 0 )
        {
            foreach (var record in records)
            {
                var rec = await _db.ListenRecords.FirstOrDefaultAsync(r => r.TrackId == record.TrackId && r.UserId == record.UserId);

                if (rec == null)
                {
                    await CreateRecord(record);
                }
                record.Id = rec.Id;
            }
        }
        return records;
    }


    // FAVORITE TRACK
    public async Task<List<FavoriteTrack>> CreateTopFive(string userId, List<Track> tracks)
    {
        List<FavoriteTrack> favs = new List<FavoriteTrack>();
        await _db.FavoriteTracks.Where(ft => ft.UserId == userId).ExecuteDeleteAsync();
        var newTracks = await CreateTracksAsync(tracks);
        favs = newTracks.Select(t => new FavoriteTrack
        {
            Id = 0,
            TrackId = t.Id,
            UserId = userId
        }).ToList();

        if (favs.Count() > 0)
        {
            await _db.FavoriteTracks.AddRangeAsync(favs);
            await _db.SaveChangesAsync();
        }
        return favs;
    }
}