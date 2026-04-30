using Musichord.Models.Entities;

namespace Musichord.Services;

public interface ITrackRepository
{
    Task<List<Track>> CreateTracksAsync(List<Track> tracks);

    Task<Track?> ReadTrackAsync(int id);
    Task<List<FavoriteTrack>> CreateTopFive(string userId, List<Track> newTracks);
    Task<List<ListenRecord>> CreateListenRecords(ApplicationUser user, List<Track> newTracks);
    Task CreateRecord(ListenRecord record);
    Task<List<ListenRecord>> GetAllRecordExceptByUser(string handle);
}