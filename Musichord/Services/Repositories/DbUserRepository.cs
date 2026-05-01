using Musichord.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Musichord.Services;

public class DbUserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public DbUserRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public async Task<ApplicationUser?> ReadByUsernameAsync(string username)
    {
        return await _db.Users
            .Include(u => u.FavoriteTracks)
                .ThenInclude(ft => ft.Track)
                    .ThenInclude(t => t.Artist)
            .Include(u => u.FavoriteTracks)
                .ThenInclude(ft => ft.Track)
                    .ThenInclude(t => t.Album)
            .Include(r => r.Records)
            .Include(r => r.Reviews)
            .FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task<ApplicationUser?> ReadByHandleAsync(string handle)
    {
        return await _db.Users
        .Include(u => u.FavoriteTracks)
            .ThenInclude(ft => ft.Track)
                    .ThenInclude(a => a.Artist)
        .Include(t => t.FavoriteTracks)
            .ThenInclude(ft => ft.Track)
                .ThenInclude(a => a.Album)
        .Include(r => r.Records)
        .Include(r => r.Reviews)
        .FirstOrDefaultAsync(u => u.Handle == handle);
    }

    public async Task<ICollection<ApplicationUser>> ReadAllAsync()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task<ICollection<ApplicationUser>> ReadAllExceptAsync(string username)
    {
        return await _db.Users.Where(u => u.UserName != username).ToListAsync();
    }

    public async Task<ICollection<string>>ReadAllHandlesExceptUserAsync(string user)
    {
        return await _db.Users.Where(u => u.Handle != user).Select(u => u.Handle).ToListAsync();
    }

    public async Task<ApplicationUser?> ReadByUsernameWithTracksAsync(string username)
    {
        return await _db.Users
            .Where(u => u.UserName == username)
            .Include(u => u.FavoriteTracks)
                .ThenInclude(ft => ft.Track)
                    .ThenInclude(t => t.Artist)  // ✅ Add this
            .Include(u => u.FavoriteTracks)
                .ThenInclude(ft => ft.Track)
                    .ThenInclude(t => t.Album)
            .FirstOrDefaultAsync();
    }
}
