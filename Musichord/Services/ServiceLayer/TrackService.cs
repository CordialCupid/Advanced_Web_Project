using Musichord.Services.Interfaces.TrackInterfaces;
using Musichord.Models.Entities;
using Musichord.Services.Repositories;

namespace Musichord.Services.ServiceLayer;

public class TrackService : ITrackService
{
    private readonly ITrackRepository _trackRepo;

    public TrackService(ITrackRepository trackRepo)
    {
        _trackRepo = trackRepo;
    }

     // STANDARD TRACK METHODS
    public async Task<List<Track>> CreateTracksAsync(List<Track> tracks)
    {

        foreach(Track track in tracks)
        {
            if (track.Artist != null)
            {
                var artist = await ReadArtist(track.Artist.SpotifyArtistId);
                if (artist != null)
                {
                    track.Artist = artist;
                }          
            }

            if (track.Album != null)
            {
                var album = await ReadAlbum(track.Album.SpotifyId);
                if (album != null)
                {

                    track.Album = album;
                }            
            }

            var existing = await ReadTrackBySpotifyIdAsync(track.SpotifyId);
            if (existing == null)
            {
                await CreateTrack(track);
                existing = await ReadTrackBySpotifyIdAsync(track.SpotifyId);
            }
            track.Id = existing!.Id; 
        }
        return tracks; 
    }

    public async Task<Track?> ReadTrackBySpotifyIdAsync(string id)
    {
        return await _trackRepo.ReadTrackBySpotifyIdAsync(id);
    }

    public async Task CreateTrack(Track track)
    {
        await _trackRepo.CreateTrack(track);
    }

    // LISTEN RECORD (RECENTLY PLAYED) METHODS

    public async Task CreateRecord(ListenRecord record)
    {
        await _trackRepo.CreateRecord(record);
    }

    public async Task<ICollection<ListenRecord>> GetAllRecordExceptByUser(string handle)
    {
        return await _trackRepo.GetAllRecordExceptByUser(handle);
    }

    public async Task<List<ListenRecord>> CreateListenRecords(ApplicationUser user, List<Track> tracks)
    {
        List<ListenRecord> records = new List<ListenRecord>();
        var newTracks = await CreateTracksAsync(tracks);
        records = newTracks.Select(t => new ListenRecord
        {
            Id = 0,
            TrackId = t.Id,
            UserId = user.Id,
            UserHandle = user.Handle,
            TrackName = t.Name,
            ProfilePicture = user.ProfilePicture
        }).ToList();

        
        if (records.Count() > 0 )
        {
            foreach (var record in records)
            {
                var rec = await ReadListenRecord(record.TrackId, record.UserId);

                if (rec == null)
                {
                    await CreateRecord(record);
                }
            }
        }
        return records;
    }

    public async Task<ListenRecord?> ReadListenRecord(int trackId, string userId)
    {
        return await _trackRepo.ReadListenRecord(trackId, userId);
    }

    // FAVORITE TRACK
    public async Task<List<FavoriteTrack>> CreateTopFive(string userId, List<Track> tracks)
    {
        List<FavoriteTrack> favs = new List<FavoriteTrack>();
        await _trackRepo.DeleteFavorites(userId);
        var newTracks = await CreateTracksAsync(tracks);
        favs = newTracks.Select(t => new FavoriteTrack
        {
            Id = 0,
            TrackId = t.Id,
            UserId = userId
        }).ToList();

        if (favs.Count() > 0)
        {
            await _trackRepo.CreateFavoriteTracksAsync(favs);
        }
        return favs;
    }

    // ALBUM
    public async Task<Album?> ReadAlbum(string SpotifyId)
    {
        return await _trackRepo.ReadAlbum(SpotifyId);
    }

    // ARTIST
    public async Task<Artist?> ReadArtist(string SpotifyArtistId)
    {
        return await _trackRepo.ReadArtist(SpotifyArtistId);
    }
}