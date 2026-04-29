using Humanizer;
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

    public async Task<ICollection<string>> GetAllFriendsHandlesAsync(string user)
    {
        var relationships = await GetAllFriendshipsAsync();
        var usersRelationships = relationships.Where(f => f.ReceiverHandle == user || f.SenderHandle == user).ToList();
        var userSent = usersRelationships.Where(f => f.SenderHandle == user).Select(f => f.ReceiverHandle).ToList();
        var userReceived = usersRelationships.Where(f => f.ReceiverHandle == user).Select(f => f.SenderHandle).ToList();

        var friendsHandles = userSent.Concat(userReceived).ToList();

        return friendsHandles;
    }

    public async Task<Friendship> CreateFriendship(ApplicationUser sender, ApplicationUser receiver)
    {
        Friendship FriendshipToAdd = new Friendship();

        FriendshipToAdd.Sender = sender;
        FriendshipToAdd.SenderHandle = sender.Handle;
        FriendshipToAdd.SenderId = sender.Id;
        FriendshipToAdd.Receiver = receiver;
        FriendshipToAdd.ReceiverHandle = receiver.Handle;
        FriendshipToAdd.ReceiverId = receiver.Id;
        FriendshipToAdd.Status = "Pending";

        await _db.Friendships.AddAsync(FriendshipToAdd);
        await _db.SaveChangesAsync();
        return FriendshipToAdd; 
    }

    public async Task DeleteFriendship(string sender, string receiver)
    {
        if (sender != null && receiver != null)
        {
            Friendship? toDelete = await ReadAsync(sender, receiver);
            if (toDelete != null)
            { 
                _db.Friendships.Remove(toDelete);
                await _db.SaveChangesAsync();
            }
        }
    }

    public async Task UpdateFriendshipStatus(string senderHandle, string receiverHandle)
    {
        Friendship? toUpdate = await ReadAsync(senderHandle, receiverHandle);
        if (toUpdate != null)
        {
            toUpdate.Status = "Accepted";
            await _db.SaveChangesAsync();
        }
    }
}