using Microsoft.EntityFrameworkCore;
using Musichord.Services;

namespace Musichord.Models.Entities;

public class FriendshipRepo : IFriendshipRepo
{
    private readonly ApplicationDbContext _db;

    public FriendshipRepo(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<ICollection<Friendship>> GetAcceptedFriendshipsAsync(string userId)
    {
        return await _db.Friendships.Where(f => (f.SenderId == userId || f.ReceiverId == userId) && f.Status == "Accepted").ToListAsync();        
    }

    public async Task<ICollection<Friendship>> GetUnacceptedFriendshipsAsync(string userId)
    {
        return await _db.Friendships.Where(f => (f.SenderId == userId || f.ReceiverId == userId) && f.Status == "Pending").ToListAsync();
    }

    public async Task<ICollection<Friendship>> GetAllFriendshipsAsync()
    {
        return await _db.Friendships.ToListAsync();
    }

    public async Task<Friendship> CreateFriendship(ApplicationUser sender, ApplicationUser receiver)
    {
        Friendship FriendshipToAdd = new Friendship();

        FriendshipToAdd.Sender = sender;
        FriendshipToAdd.SenderId = sender.Id;
        FriendshipToAdd.Receiver = receiver;
        FriendshipToAdd.ReceiverId = receiver.Id;
        FriendshipToAdd.Status = "Pending";

        await _db.Friendships.AddAsync(FriendshipToAdd);
        await _db.SaveChangesAsync();
        return FriendshipToAdd;
    }
}