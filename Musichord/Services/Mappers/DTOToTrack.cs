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
            // TODO: Extract the image URL from the album images array
            // Step 1: Check if tdto.Album and tdto.Album.Images exist (they might be null)
            // Step 2: The Images list contains multiple sizes - find the smallest one (best for performance)
            //         Use OrderBy(img => img.Height ?? 0) and then FirstOrDefault()
            // Step 3: Extract the Url property from the selected image
            // Step 4: Assign it to the imageUrl variable below
            // tdto.Album. = tdto.Album.Images ?? new List<ImageDTO>(); // Ensure Images is not null
            
            // string? imageUrl = null; // TODO: Replace null with your image extraction logic
            
            string imageUrl = string.Empty; // Default to empty string if no images are available


            if (tdto.Album?.Images != null && tdto.Album.Images.Count > 0)
            {
                // Find the smallest image (best for performance)
                var smallestImage = tdto.Album.Images.OrderBy(img => img.Height ?? int.MaxValue).FirstOrDefault();
                imageUrl = smallestImage?.Url; // Extract the URL from the selected image
            }


            Track track = new Track
            {
                Id = 0,
                SpotifyId = tdto.TrackId,
                Name = tdto.TrackName,
                ImageUrl = imageUrl
            };
            tracks.Add(track);
        }
        return tracks;
    }
}