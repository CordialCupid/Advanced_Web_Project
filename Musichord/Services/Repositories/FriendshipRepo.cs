using Humanizer;
using Microsoft.EntityFrameworkCore;
using Musichord.Services;
using Microsoft.AspNetCore.Authorization;
using Musichord.Services.Interfaces.Friends;

namespace Musichord.Models.Entities;

public class FriendshipRepo : IFriendshipRepo
{
    private readonly ApplicationDbContext _db;

    public FriendshipRepo(ApplicationDbContext db, IUserRepository userRepo)
    {
        _db = db;
    }

    public async Task<Friendship?> ReadAsync(string sender, string receiver)
    {
        return await _db.Friendships
                        .Include(f => f.Sender)
                        .Include(f => f.Receiver)
                        .FirstOrDefaultAsync(f => f.SenderHandle == sender && f.ReceiverHandle == receiver);
    }

    public async Task<ICollection<Friendship>> GetAllFriendshipsAsync()
    {
        return await _db.Friendships
                        .Include(f => f.Sender)
                        .Include(f => f.Receiver)
                        .ToListAsync();
    }

    public async Task<Friendship> CreateFriendship(Friendship FriendshipToAdd)
    {
        await _db.Friendships.AddAsync(FriendshipToAdd);
        await _db.SaveChangesAsync();
        return FriendshipToAdd; 
    }

    public async Task DeleteFriendship(Friendship toDelete)
    {
        _db.Friendships.Remove(toDelete);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateFriendshipStatus(Friendship toUpdate)
    {
        toUpdate.Status = "Accepted";
        await _db.SaveChangesAsync();
    }
}