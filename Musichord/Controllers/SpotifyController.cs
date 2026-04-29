using Microsoft.AspNetCore.Mvc;
using Muschord.Services;
using Musichord.Models.DTO;
using Musichord.Models.Entities;
using Musichord.Services;
using Musichord.Services.Mappers;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;


namespace Musichord.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpotifyController : ControllerBase
{
    private readonly ITrackRepository _trackRepo;
    private readonly IArtistRepository _artistRepo;
    private readonly ISpotifyRepo _spotRepo;
    private readonly IUserRepository _userRepo;
    private readonly ApplicationDbContext _db;

    public SpotifyController(ITrackRepository trackRepo, IArtistRepository artistRepo, ISpotifyRepo spotRepo, IUserRepository userRepo, ApplicationDbContext db)
    {
        _trackRepo = trackRepo;
        _artistRepo = artistRepo;
        _spotRepo = spotRepo;
        _userRepo = userRepo;
        _db = db;
    }

    [HttpGet("topfive/{accessToken}")]
    public async Task<IActionResult> GetFive(string accessToken)
    {   
        List<FavoriteTrack> favs = new List<FavoriteTrack>();
        ApplicationUser? currentUser = await _userRepo.ReadByUsernameAsync(User!.Identity!.Name!);
        string response = await _spotRepo.GetRequest(accessToken, "https://api.spotify.com/v1/me/top/tracks?limit=25&offset=0&time_range=short_term");
        TopFiveDTO? topFives = JsonSerializer.Deserialize<TopFiveDTO>(response);
        List<Artist> artists = await DTOToArtist.MapToArtist(topFives!.Tracks);
        List<Track> tracks = await DTOToTrack.MapToTrack(topFives!.Tracks);

        await _artistRepo.CreateArtistsAsync(artists);

        for (int i = 0; i < topFives.Tracks.Count(); i++)
        {
            tracks[i].ArtistId = artists[i].Id;
        }

        List<Track> newTracks = await _trackRepo.CreateTracksAsync(tracks);

        await _db.FavoriteTracks.Where(ft => ft.UserId == currentUser!.Id).ExecuteDeleteAsync();

        favs = newTracks.Select(t => new FavoriteTrack
        {
            Id = 0,
            TrackId = t.Id,
            UserId = currentUser!.Id
        }).ToList();

        await _db.FavoriteTracks.AddRangeAsync(favs);
        await _db.SaveChangesAsync();

        
        return Ok();
    }

    [HttpGet("recently-played/{accessToken}")]
    public async Task<IActionResult> GetRecent(string accessToken)
    {   
        List<ListenRecord> recents = new List<ListenRecord>();
        ApplicationUser? currentUser = await _userRepo.ReadByUsernameAsync(User!.Identity!.Name!);
        string response = await _spotRepo.GetRequest(accessToken, "https://api.spotify.com/v1/me/player/recently-played?limit=2");
        RecentDTO? recentDTo = JsonSerializer.Deserialize<RecentDTO>(response);
        List<TrackDTO?> rippedTracks = await DTOToRecent.MapToRecent(recentDTo.Tracks) ?? new();
        List<Artist> artists = await DTOToArtist.MapToArtist(rippedTracks);
        List<Track> tracks = await DTOToTrack.MapToTrack(rippedTracks);   
        

        await _artistRepo.CreateArtistsAsync(artists);

        for (int i = 0; i < rippedTracks.Count(); i++)
        {
            tracks[i].ArtistId = artists[i].Id;
        }

        List<Track> newTracks = await _trackRepo.CreateTracksAsync(tracks);

        await _db.ListenRecords.Where(ft => ft.UserId == currentUser!.Id).ExecuteDeleteAsync();

        recents = newTracks.Select(t => new ListenRecord
        {
            Id = 0,
            TrackId = t.Id,
            UserId = currentUser!.Id
        }).ToList();

        await _db.ListenRecords.AddRangeAsync(recents);
        await _db.SaveChangesAsync();

        
        return Ok();
    }
}