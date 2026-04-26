using Musichord.Models.Entities;

namespace Musichord.Services;

public interface ITrackRepository
{
    Task<List<Track>> CreateTracksAsync(List<Track> tracks);

    Task<Track?> ReadTrackAsync(int id);
}