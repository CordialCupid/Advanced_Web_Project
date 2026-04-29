using Musichord.Models.DTO;
using Musichord.Models.Entities;

namespace Musichord.Services.Mappers;

public class DTOToAlbum
{
    public static List<Album> MapToAlbum(List<TrackDTO?> tracksdto)
    {
        List<Album> albumsToReturn = new List<Album>();
        foreach (TrackDTO? trackDTO in tracksdto)
        {
            if (trackDTO == null)
            {
                continue;
            }
            Album albumModel = new Album
            {
                Id = 0,
                SpotifyId = trackDTO.Album.AlbumId,
                Name = trackDTO.Album.AlbumName,
            };         
            albumsToReturn.Add(albumModel);
        }
        
        return albumsToReturn;
    }
}
