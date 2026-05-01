using Musichord.Models.Entities;

namespace Musichord.Services.Interfaces.TrackInterfaces;

public interface ITrackRepository
{
    Task<Track?> ReadTrackAsync(int id);
    Task CreateRecord(ListenRecord record);
    Task<Album?> ReadAlbum(string SpotifyId);
    Task<Artist?> ReadArtist(string SpotifyArtistId);
    Task<Track?> ReadTrackBySpotifyIdAsync(string id);
    Task CreateTrack(Track track);
    Task<ICollection<ListenRecord>> GetAllRecordExceptByUser(string handle);
    Task<ListenRecord?> ReadListenRecord(int trackId, string userId);
    Task DeleteFavorites(string userId);
    Task CreateFavoriteTracksAsync(ICollection<FavoriteTrack> favs);
}