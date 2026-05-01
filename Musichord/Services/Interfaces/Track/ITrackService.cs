using Musichord.Models.Entities;

namespace Musichord.Services.Interfaces.TrackInterfaces;

public interface ITrackService
{
    Task<List<Track>> CreateTracksAsync(List<Track> tracks);

    Task<Track?> ReadTrackBySpotifyIdAsync(string id);

    Task CreateTrack(Track track);

    // LISTEN RECORD (RECENTLY PLAYED) METHODS
    Task CreateRecord(ListenRecord record);

    Task<ICollection<ListenRecord>> GetAllRecordExceptByUser(string handle);

    Task<List<ListenRecord>> CreateListenRecords(ApplicationUser user, List<Track> tracks);

    Task<ListenRecord?> ReadListenRecord(int trackId, string userId);

    // FAVORITE TRACK
    Task<List<FavoriteTrack>> CreateTopFive(string userId, List<Track> tracks);

    // ALBUM
    Task<Album?> ReadAlbum(string SpotifyId);

    // ARTIST
    Task<Artist?> ReadArtist(string SpotifyArtistId);
}