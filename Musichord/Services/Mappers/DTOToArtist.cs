using Musichord.Models.DTO;
using Musichord.Models.Entities;

namespace Musichord.Services.Mappers;

public class DTOToArtist
{
    public static async Task<List<Artist>> MapToArtist(List<TrackDTO> tracksdto)
    {
        List<Artist> artistsToReturn = new List<Artist>();
        foreach (TrackDTO trackDTO in tracksdto)
        {
            Artist artistModel = new Artist
            {
                Id = 0,
                SpotifyArtistId = trackDTO.Artists[0].ArtistId,
                Name = trackDTO.Artists[0].ArtistName,
            };         
            artistsToReturn.Add(artistModel);
        }
        
        return artistsToReturn;
    }
}
