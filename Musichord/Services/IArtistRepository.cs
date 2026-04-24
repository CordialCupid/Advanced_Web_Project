using Musichord.Models.Entities;

namespace Musichord.Services;

public interface IArtistRepository
{
    Task CreateArtistsAsync(List<Artist> artists);
    Task<Artist?> ReadArtistAsync(int id);
}