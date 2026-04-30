using Musichord.Models.Entities;
using Musichord.Models.DTO;
using System.Text.Json;

namespace Musichord.Services.Mappers;

public static class SpotifyApiMapper
{
    public static Track ToTrack(TrackDTO dto) => new Track
    {
        Id = 0,
        Name       = dto.TrackName,
        SpotifyId = dto.TrackId,
        Artist = ToArtist(dto.Artists.FirstOrDefault()),
        Album      = ToAlbum(dto.Album)
    };

    public static Artist ToArtist(ArtistDTO dto) => new Artist
    {
        Id = 0,
        Name = dto.ArtistName,
        SpotifyArtistId = dto.ArtistId
    };

    public static Album ToAlbum(AlbumDTO dto) => new Album
    {
        Id = 0,
        SpotifyId     = dto.AlbumId,
        Name          = dto.AlbumName,
        ImageUrl = dto.Images.FirstOrDefault()?.Url,
        Height = dto.Images.FirstOrDefault().Height,
        Width = dto.Images.FirstOrDefault().Width
    };

    public static async Task<List<Track>> Map(TopFiveDTO dto)
    {
        List<Track> newTracks = new();
        foreach (var t in dto.Tracks)
        {
            newTracks.Add(ToTrack(t));
        }

        return newTracks;
    }

    public static async Task<List<Track>> MapFromRecent(RecentDTO dto)
    {
        List<Track> newTracks = new();
        foreach (var r in dto.Tracks)
        {
            if (r.Track == null)
            {
                continue;
            }
            newTracks.Add(ToTrack(r.Track));
        }

        return newTracks;
    }
}