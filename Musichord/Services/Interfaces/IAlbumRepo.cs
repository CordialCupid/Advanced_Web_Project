using Musichord.Models.Entities;

namespace Musichord.Services;

public interface IAlbumRepository
{
    Task<ICollection<Album>> GetRandomAlbumsAsync(int count);
}