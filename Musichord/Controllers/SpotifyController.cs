using Microsoft.AspNetCore.Mvc;
using Musichord.Models.DTO;
using Musichord.Models.Entities;
using Musichord.Services;
using Musichord.Services.Mappers;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Musichord.Services.Interfaces.TrackInterfaces;


namespace Musichord.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpotifyController : ControllerBase
{
    private readonly ITrackService _trackService;
    private readonly IUserRepository _userRepo;
    private readonly ApplicationDbContext _db;
    private readonly HttpClient _httpClient;

    public SpotifyController(ITrackService trackService, IUserRepository userRepo, ApplicationDbContext db, HttpClient client)
    {
        _trackService = trackService;
        _userRepo = userRepo;
        _db = db;
        _httpClient = client;
    }

    [HttpGet("topfive/{accessToken}")]
    public async Task<IActionResult> GetFive(string accessToken)
    {   
        List<Track> tracks = new();
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }   

        ApplicationUser? currentUser = await _userRepo.ReadByUsernameAsync(User.Identity.Name);
        string response = await GetRequest(accessToken, "https://api.spotify.com/v1/me/top/tracks?limit=25&offset=0&time_range=short_term");
        TopFiveDTO? topFives = JsonSerializer.Deserialize<TopFiveDTO>(response);

        if (topFives != null)
        {
            tracks = await SpotifyApiMapper.Map(topFives);         
        }
        
        if (currentUser != null)
        {
            return Ok(await _trackService.CreateTopFive(currentUser.Id, tracks));
            
        }
        return Unauthorized();
    }

    [HttpGet("recently-played/{accessToken}")]
    public async Task<IActionResult> GetRecent(string accessToken)
    {   
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        } 

        List<Track> tracks = new();
        ApplicationUser? currentUser = await _userRepo.ReadByUsernameAsync(User.Identity.Name);
        string response = await GetRequest(accessToken, "https://api.spotify.com/v1/me/player/recently-played?limit=10");
        RecentDTO? recentDTo = JsonSerializer.Deserialize<RecentDTO>(response);

        if (recentDTo != null)
        {
            tracks = await SpotifyApiMapper.MapFromRecent(recentDTo);     
        }

        if (currentUser == null)
        {
            return Unauthorized();
        }
        return Ok(await _trackService.CreateListenRecords(currentUser, tracks));
    }

    public async Task<string> GetRequest(string accessToken, string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {accessToken}");

        using var response = await _httpClient.SendAsync(request);
        string JsonString = await response.Content.ReadAsStringAsync();
        return JsonString;
    }
}