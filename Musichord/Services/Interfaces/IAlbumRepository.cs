using Musichord.Models.Entities;

namespace Musichord.Services;

public interface IAlbumRepository
{
    Task CreateAlbumsAsync(List<Album> albums);
    Task<Album> CreateAlbumAsync(Album album);
}