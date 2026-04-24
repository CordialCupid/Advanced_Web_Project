using Musichord.Models.DTO;
using Musichord.Models.Entities;

namespace Musichord.Services.Mappers;

public class DTOToTrack
{
    public static async Task<List<Track>> MapToTrack(List<TrackDTO> trackdto)
    {
        List<Track> tracks = new List<Track>();
        foreach (TrackDTO tdto in trackdto)
        {
            Track track = new Track
            {
                Id = 0,
                SpotifyId = tdto.TrackId,
                Name = tdto.TrackName,
            };
            tracks.Add(track);
        }
        return tracks;
    }
}