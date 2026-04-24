using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Musichord.Models.Entities;

namespace Muschord.Services;

public interface ISpotifyRepo
{
    Task<JsonDocument> GetRequest(string accessToken, string uri);
}