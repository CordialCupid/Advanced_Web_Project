using Musichord.Models.Entities;
using System.Text.Json;
namespace Muschord.Services;

public class SpotifyRepo : ISpotifyRepo
{
    private readonly HttpClient _httpClient;

    public SpotifyRepo(HttpClient client)
    {
        _httpClient = client;
    }
    public async Task<JsonDocument> GetRequest(string accessToken, string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        request.Headers.Add("Authorization", $"Bearer {accessToken}");

        using var response = await _httpClient.SendAsync(request);
        string JsonString = await response.Content.ReadAsStringAsync();
        return JsonDocument.Parse(JsonString);
    }
}