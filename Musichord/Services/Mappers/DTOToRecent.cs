using Musichord.Models.DTO;
using Musichord.Models.Entities;

namespace Musichord.Services.Mappers;

public class DTOToRecent
{
    public static async Task<List<TrackDTO?>> MapToRecent(List<RecentTrackDTO> tracksdto)
    {
        List<TrackDTO?> ripped = tracksdto.Select(t => t.Track).ToList() ?? new();
        return ripped;
    }
}
