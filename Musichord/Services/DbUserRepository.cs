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
        .FirstOrDefaultAsync(u => u.UserName == username);
    }
}