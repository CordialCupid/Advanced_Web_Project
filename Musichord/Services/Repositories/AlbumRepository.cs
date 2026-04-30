// using Musichord.Models.Entities;
// using Microsoft.EntityFrameworkCore;
// namespace Musichord.Services;

// public class AlbumRepository : IAlbumRepository
// {
//     private readonly ApplicationDbContext _db;

//     public AlbumRepository(ApplicationDbContext db)
//     {
//         _db = db;
//     }

//     public async Task CreateAlbumsAsync(List<Album> albums)
//     {

//         foreach(Album album in albums)
//         {
//             var existing = await _db.Albums.FirstOrDefaultAsync(a => a.SpotifyId == album.SpotifyId);
//             if (existing == null)
//             {
//                 _db.Albums.Add(album);
//                 await _db.SaveChangesAsync();
//             }
//             else
//             {
//                 album.Id = existing.Id;
//             }
//         }
//     }

//     public async Task<Album> CreateAlbumAsync(Album album)
//     {
//         var existing = await _db.Albums.FirstOrDefaultAsync(a => a.SpotifyId == album.SpotifyId);

//         if (existing != null)
//         {
//             return existing;
//         }

//         await _db.Albums.AddAsync(album);
//         await _db.SaveChangesAsync();
//         return album;
//     }
// }